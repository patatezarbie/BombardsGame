using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BombardsClient
{
    class BG_Client
    {
        #region fields
        // Connection objects
        public readonly string ServerAddress;
        public readonly int Port;

        private TcpClient _tcpClient;
        public bool Running { get; private set; }

        // Buffer & messaging
        public readonly int BufferSize = 2 * 1024;  // 2KB
        private NetworkStream _msgStream = null;

        // Personal data
        public readonly string Name;
        #endregion

        #region properties
        public TcpClient TcpClient
        {
            get
            {
                return _tcpClient;
            }

            set
            {
                _tcpClient = value;
            }
        }

        public NetworkStream MsgStream
        {
            get
            {
                return _msgStream;
            }

            set
            {
                _msgStream = value;
            }
        }
        #endregion

        #region constructors
        public BG_Client(string serverAddress, int port, string name)
        {
            // Create a non-connected TcpClient
            this.TcpClient = new TcpClient();
            this.TcpClient.SendBufferSize = this.BufferSize;
            this.TcpClient.ReceiveBufferSize = this.BufferSize;
            this.Running = false;

            // Set the other things
            this.ServerAddress = serverAddress;
            this.Port = port;
            this.Name = name;
        }
        #endregion

        #region methods

        public void Connect()
        {
            // Try to connect
            this.TcpClient.Connect(ServerAddress, Port);
            EndPoint endPoint = TcpClient.Client.RemoteEndPoint;

            // Make sure we're connected
            if (this.TcpClient.Connected)
            {
                // Got in!
                Console.WriteLine("Connected to the server at {0}.", endPoint);

                // Tell them that we're a player
                this.MsgStream = TcpClient.GetStream();
                byte[] msgBuffer = Encoding.UTF8.GetBytes(String.Format("player:{0}", Name));
                this.MsgStream.Write(msgBuffer, 0, msgBuffer.Length);   // Blocks

                // If we're still connected after sending our name, that means the server accepts us
                if (!this.IsDisconnected(this.TcpClient))
                {
                    this.Running = true;
                    Thread.Sleep(20);
                }
                else
                {
                    // Name was probably taken...
                    this.CleanupNetworkResources();
                    Console.WriteLine("The server rejected the connection; name \"{0}\" is probably in use.", this.Name);
                }
            }
            else
            {
                this.CleanupNetworkResources();
                Console.WriteLine("Wasn't able to connect to the server at {0}.", endPoint);
            }
        }

        /// <summary>
        /// "Chat mode" poll the user for a message and send it
        /// </summary>
        public void SendMessages()
        {
            bool wasRunning = this.Running;

            while (this.Running)
            {
                // Poll for user input
                Console.Write("{0}> ", Name);
                string msg = Console.ReadLine();

                // Quit or send a message
                if ((msg.ToLower() == "quit") || (msg.ToLower() == "exit"))
                {
                    // User wants to quit
                    Console.WriteLine("Disconnecting...");
                    this.Running = false;
                }
                else if (msg != string.Empty)
                {
                    // Send the message
                    byte[] msgBuffer = Encoding.UTF8.GetBytes(msg);
                    this.MsgStream.Write(msgBuffer, 0, msgBuffer.Length);
                }

                // Use less CPU
                Thread.Sleep(10);

                // Check the server didn't disconnect us
                if (this.IsDisconnected(TcpClient))
                {
                    this.Running = false;
                    Console.WriteLine("Server has disconnected from us.\n:[");
                }
            }

            if (wasRunning)
            {
                this.CleanupNetworkResources();
                Console.WriteLine("Disconnected.");
            }
        }


        /// <summary>
        /// Send a single message passed as parameter
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessages(string msg)
        {
            bool wasRunning = this.Running;

            // Quit or send a message
            if ((msg.ToLower() == "quit") || (msg.ToLower() == "exit"))
            {
                // User wants to quit
                Console.WriteLine("Disconnecting...");
                this.Running = false;
            }
            else if (msg != string.Empty)
            {
                // Send the message
                byte[] msgBuffer = Encoding.UTF8.GetBytes(msg);
                this.MsgStream.Write(msgBuffer, 0, msgBuffer.Length);
            }

            // Use less CPU
            Thread.Sleep(20);

            // Check that we are still connected to the server
            if (this.IsDisconnected(this.TcpClient))
            {
                Running = false;
                this.CleanupNetworkResources();
                Console.WriteLine("Server has disconnected from us.\n:[");
            }
        }

        // See if the server
        public void CheckForNewMessages()
        {
            bool wasRunning = Running;

            // Listen for messages
            while (Running)
            {
                // Do we have a new message?
                int messageLength = this.TcpClient.Available;
                if (messageLength > 0)
                {
                    // Read the whole message
                    byte[] msgBuffer = new byte[messageLength];
                    this.MsgStream.Read(msgBuffer, 0, messageLength);   // Blocks

                    string msg = Encoding.UTF8.GetString(msgBuffer);

                    // Check that we don't recieve our own messages
                    string nameFromMsg = msg.Split(':')[0];

                    if (nameFromMsg != this.Name)
                    {
                        // TODO : Fetch the messages in a array/list/queue ??
                        Console.WriteLine(Environment.NewLine + msg);
                    }
                }

                // Use less CPU
                Thread.Sleep(10);

                // Check that we are still connected to the server
                if (this.IsDisconnected(this.TcpClient))
                {
                    Running = false;
                    this.CleanupNetworkResources();
                    Console.WriteLine("Server has disconnected from us.\n:[");
                }
            }
        }

        // Cleans any leftover network resources
        private void CleanupNetworkResources()
        {
            this.MsgStream.Close();
            this.MsgStream = null;
            this.TcpClient.Close();
        }

        // Checks if a socket has disconnected
        private bool IsDisconnected(TcpClient client)
        {
            try
            {
                Socket clientSocket = client.Client;
                return clientSocket.Poll(10 * 1000, SelectMode.SelectRead) && (clientSocket.Available == 0);
            }
            catch (SocketException socketError)
            {
                // We got a socket error, assume it's disconnected
                return true;
            }
        }

        /// <summary>
        /// Disconnect the client when called
        /// </summary>
        public void Disconnect()
        {
            if (!this.IsDisconnected(this.TcpClient))
            {
                this.SendMessages("quit");
            }
        }
        #endregion
    }
}

