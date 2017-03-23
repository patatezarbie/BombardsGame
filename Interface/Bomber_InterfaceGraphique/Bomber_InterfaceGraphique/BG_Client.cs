using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Bomber_InterfaceGraphique
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

        public List<BG_Player> listPlayers;
        public BG_Field field;

        // QUICK AND DIRTY WARNING !!!!!
        FrmView MainForm;
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
        public BG_Client(string serverAddress, int port, string name, FrmView view)
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
            this.MainForm = view;

            this.listPlayers = new List<BG_Player>();
            this.field = new BG_Field(MainForm.Width, MainForm.Height, 0);
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

                    // Update the client's values depending on the message
                    this.UpdateValues(msg);

                    if (nameFromMsg != this.Name)
                    {
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

        public void UpdateValues(string msg)
        {
            // Check for seed
            string SeedRegex = "seed:([0-9]{1,})";
            Match seedMatch = Regex.Match(msg, SeedRegex);

            // Check if we have a seed
            if (seedMatch.Value != String.Empty)
            {
                int levelSeed = int.Parse(seedMatch.Groups[1].Value);
                this.field = new BG_Field(MainForm.Width - 180, MainForm.Height - 24, levelSeed);
            }


            // Check for moving players
            string MoveRegex = "\\(([^,;.]+);([0-9]{1,3});([0-9]{1,3})\\)";
            MatchCollection moveMatches = Regex.Matches(msg, MoveRegex);

            foreach (Match match in moveMatches)
            {
                string[] MoveValues = Regex.Split(match.Value, MoveRegex);

                string name = MoveValues[1];
                int x = int.Parse(MoveValues[2]);
                int y = int.Parse(MoveValues[3]);

                BG_Cannon currentPlayerCannon = new BG_Cannon(Color.Red, new BG_Location(x, y));
                BG_Player currentPlayer = new BG_Player(name, currentPlayerCannon);

                this.listPlayers.Add(currentPlayer);
            }


            // Check for new turn
            string newTurnRegex = "[^,.-]{1,};newturn";
            Match newTurnMatch = Regex.Match(msg, newTurnRegex);

            // Check if we have to start a new turn
            if (newTurnMatch.Value == (this.Name + ";" + "newturn"))
            {
                // Start the turn
                
            }


            // Check for end turn
            string testStr = "endturn";
            string endTurnRegex = "^endturn$";
            Match endTurnMatch = Regex.Match(testStr, endTurnRegex);

            // Check if we have to start a new turn
            if (endTurnMatch.Value == "endturn")
            {
                // End the turn

            }
            
        }

        #endregion
    }
}

