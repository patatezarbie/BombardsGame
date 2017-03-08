using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BombardsServer
{
    public class Server
    {
        #region fields
        // What listens in
        private TcpListener _listener;

        // types of clients connected
        private List<TcpClient> _clients = new List<TcpClient>();

        // Names that are taken by other players
        private Dictionary<TcpClient, string> _names = new Dictionary<TcpClient, string>();

        // Messages that need to be sent
        private Queue<string> _messageQueue = new Queue<string>();

        public readonly string RoomName;
        public readonly int Port;
        public bool Running { get; private set; }

        // Buffer
        public readonly int BufferSize = 2 * 1024;  // 2048 byte
        #endregion

        #region properties
        public TcpListener Listener
        {
            get
            {
                return _listener;
            }

            set
            {
                _listener = value;
            }
        }

        public List<TcpClient> Clients
        {
            get
            {
                return _clients;
            }

            set
            {
                _clients = value;
            }
        }

        public Dictionary<TcpClient, string> Names
        {
            get
            {
                return _names;
            }

            set
            {
                _names = value;
            }
        }

        public Queue<string> MessageQueue
        {
            get
            {
                return _messageQueue;
            }

            set
            {
                _messageQueue = value;
            }
        }
        #endregion

        #region constructors
        public Server(string roomName, int port)
        {
            // Set the room info
            this.RoomName = roomName;
            this.Port = port;
            this.Running = false;

            // Make the listener listen for connections on any network device
            this.Listener = new TcpListener(IPAddress.Any, Port);
        }
        #endregion

        #region methods
        public void Shutdown()
        {
            Running = false;
            Console.WriteLine("Shutting down server");
        }

        public void Run()
        {
            // Some info
            Console.WriteLine("Starting the \"{0}\" TCP Server on port {1}.", this.RoomName, this.Port);
            Console.WriteLine("Press Ctrl-C to shut down the server at any time.");

            // Make the server run
            this.Listener.Start();
            this.Running = true;

            // Main server loop
            while (this.Running)
            {
                // Check for new clients
                if (this.Listener.Pending())
                {
                    this.HandleNewConnection();
                }
                    
                // Do the rest
                this.CheckForDisconnects();
                this.CheckForNewMessages();
                this.SendMessages();

                // Update only every 10ms
                Thread.Sleep(10);
            }

            // Stop the server, and clean up any connected clients
            foreach (TcpClient v in this.Clients) {
                this.CleanupClient(v);
            }

            this.Listener.Stop();

        }

        // Sees if any of the clients have left the chat server
        private void CheckForDisconnects()
        {
            // For every client in the room
            foreach (TcpClient client in this.Clients.ToArray())
            {
                if (this.IsDisconnected(client))
                {
                    // Get info about the messenger
                    string name = _names[client];

                    // Tell the viewers someone has left
                    Console.WriteLine(Environment.NewLine, "Player {0} has left.", name);
                    this.MessageQueue.Enqueue(String.Format("{0}{1} has left the game", Environment.NewLine, name));

                    this.Clients.Remove(client);  // Remove from list
                    this.Names.Remove(client);    // Remove taken name
                    this.CleanupClient(client);   // Cleanup
                }
            }
        }

        // See if any of our messengers have sent us a new message, put it in the queue
        private void CheckForNewMessages()
        {
            foreach (TcpClient client in this.Clients)
            {
                int messageLength = client.Available;
                if (messageLength > 0)
                {
                    // Get the message if there is one
                    byte[] msgBuffer = new byte[messageLength];
                    client.GetStream().Read(msgBuffer, 0, msgBuffer.Length);

                    // Attach a name to it and shove it into the queue
                    string msg = String.Format("{0}: {1}", this.Names[client], Encoding.UTF8.GetString(msgBuffer));
                    this.MessageQueue.Enqueue(msg);
                }
            }
        }

        // Clears out the message queue (and sends it to all of the viewers
        private void SendMessages()
        {
            foreach (string message in this.MessageQueue)
            {
                // Encode the message
                byte[] msgBuffer = Encoding.UTF8.GetBytes(message);

                // Send the message to each client
                foreach (TcpClient client in this.Clients) {
                    client.GetStream().Write(msgBuffer, 0, msgBuffer.Length);
                }

                Console.WriteLine(message);
            }

            // clear out the queue
            this.MessageQueue.Clear();
        }

        // Checks if a socket has disconnected
        private bool IsDisconnected(TcpClient client)
        {
            try
            {
                Socket clientSocket = client.Client;
                return clientSocket.Poll(10 * 1000, SelectMode.SelectRead) && (clientSocket.Available == 0);
            }
            catch (SocketException socketExeption)
            {
                // We got a socket error, assume it's disconnected
                return true;
            }
        }

        private void HandleNewConnection()
        {
            bool clientIsAccepted = false;
            TcpClient newClient = _listener.AcceptTcpClient();
            NetworkStream netStream = newClient.GetStream();

            // Modify the default buffer sizes
            newClient.SendBufferSize = BufferSize;
            newClient.ReceiveBufferSize = BufferSize;

            // Print some info
            EndPoint endPoint = newClient.Client.RemoteEndPoint;
            Console.WriteLine("{0}Handling a new client from {1}...", Environment.NewLine, endPoint);

            // Let them identify themselves
            byte[] msgBuffer = new byte[BufferSize];
            int bytesRead = netStream.Read(msgBuffer, 0, msgBuffer.Length);
            //Console.WriteLine("Got {0} bytes.", bytesRead);
            if (bytesRead > 0)
            {
                string msg = Encoding.UTF8.GetString(msgBuffer, 0, bytesRead);

                // Check for the correct format
                if (msg.StartsWith("player:"))
                {
                    string name = msg.Substring(msg.IndexOf(':') + 1);

                    if ((name != string.Empty) && (!_names.ContainsValue(name)))
                    {
                        // Add the player
                        clientIsAccepted = true;
                        this.Names.Add(newClient, name);
                        this.Clients.Add(newClient);

                        // Show ip and name
                        Console.WriteLine("{0} is a player with the name {1}.", endPoint, name);

                        // Tell the current players we have a new player
                        this.MessageQueue.Enqueue(String.Format("{0}{1} has joined the game.", Environment.NewLine, name));
                    }
                }
                else
                {
                    // Wasn't either a viewer or messenger, clean up anyways.
                    Console.WriteLine("Client wasn't able to connect (check message format?).", endPoint);
                    CleanupClient(newClient);
                }
            }

            // Clear the client if he doesn't meet our requirements
            if (!clientIsAccepted)
                newClient.Close();
        }


        private void CleanupClient(TcpClient client)
        {
            // Clean the sent TcpClient
            client.GetStream().Close();
            client.Close();
        }
        #endregion
    }
}
