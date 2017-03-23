using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BombardsServer
{
    public class BG_GameManager
    {
        #region fields
        private const int FINAL_ROUND = 10;
        private const int PLAYER_CAP = 2;
        private const int SLEEP_TIME = 50; // Used to avoid server interpreting two messages as one
        private const string SEPARATOR = ";";
        private enum ServerState { StartServer, InitializeRound, PlayerTurn, GameEnd };

        private ServerState _state;
        private int _totalRounds;
        private int _currentRound;
        private string _roomName;
        private int _terrainSeed;
        private int _port;


        private BG_Server _server;
        #endregion

        #region properties
        public int TotalRounds
        {
            get
            {
                return _totalRounds;
            }

            set
            {
                _totalRounds = value;
            }
        }

        public int CurrentRound
        {
            get
            {
                return _currentRound;
            }

            set
            {
                _currentRound = value;
            }
        }

        public string RoomName
        {
            get
            {
                return _roomName;
            }

            set
            {
                _roomName = value;
            }
        }

        public int TerrainSeed
        {
            get
            {
                return _terrainSeed;
            }

            set
            {
                _terrainSeed = value;
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }

            set
            {
                _port = value;
            }
        }

        public BG_Server Server
        {
            get
            {
                return _server;
            }

            set
            {
                _server = value;
            }
        }

        private ServerState State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = value;
            }
        }
        #endregion

        #region constructors
        public BG_GameManager(string roomName, int port, int terrainSeed)
        {
            this.RoomName = roomName;
            this.Port = port;
            this.Server = new BG_Server(this.RoomName, this.Port);
            this.TerrainSeed = terrainSeed;

            // Initialize other components
            this.TotalRounds = 0;
            this.CurrentRound = 0;
            this.State = ServerState.StartServer;
        }
        #endregion

        #region methods
        /// <summary>
        /// Main loop and state machine
        /// </summary>
        public void GameLoop()
        {
            while (true)
            {
                switch (this.State)
                {
                    case ServerState.StartServer:
                        // Starts the server
                        this.RunServer();
                        this.State = ServerState.InitializeRound;
                        break;
                    case ServerState.InitializeRound:
                        // Check if we have enough players before proceeding
                        if (this.Server.Names.Count >= PLAYER_CAP)
                        {
                            // Set the terrain seed for all players
                            this.InitializeGame();
                            this.State = ServerState.PlayerTurn;
                        }
                        break;
                    case ServerState.PlayerTurn:
                        bool isPlayerTurn = true;

                        // Choose a player to play
                        this.NewTurn();

                        // Await player response
                        while (isPlayerTurn)
                        {
                            // Avoid mixing messages
                            Thread.Sleep(SLEEP_TIME * 2);

                            // If we receive the endturn message, we break
                            foreach (var message in this.Server.MessageQueue)
                            {
                                if (message == "endturn")
                                {
                                    isPlayerTurn = false;
                                }
                            }
                        }

                        // Make sure the client update
                        this.Server.SendMessages();

                        // If we reach the final turn, we end the game
                        if (TotalRounds == FINAL_ROUND)
                        {
                            this.State = ServerState.GameEnd;
                        }

                        // If we didn't switch state, go back to another player's turn
                        break;
                    case ServerState.GameEnd:
                        // Send messages for the end of the game
                        // TODO:

                        // Start another game
                        this.State = ServerState.InitializeRound;
                        break;
                }

                // Sleep again when restarting the game
                Thread.Sleep(SLEEP_TIME);
            }
        }

        /// <summary>
        /// Run the server on a thread
        /// </summary>
        private void RunServer()
        {
            Thread thread = new Thread(new ThreadStart(this.Server.Run));
            thread.Start();
            Thread.Sleep(SLEEP_TIME);
        }

        /// <summary>
        /// Initialize the game
        /// </summary>
        private void InitializeGame()
        {
            // Send the seed to generate terrain
            this.Server.MessageQueue.Enqueue("seed:" + this.TerrainSeed);
            Thread.Sleep(SLEEP_TIME);

        }

        /// <summary>
        /// Starts a new turn
        /// </summary>
        private void NewTurn()
        {
            // Send every player's position
            string playerPos = this.GetPlayerPos();
            this.Server.MessageQueue.Enqueue(playerPos);
            Thread.Sleep(SLEEP_TIME);

            // Choose a player and tell him that it's his turn
            string currentPlayerName = this.Server.Names.Values.ToArray()[this.CurrentRound];
            this.Server.MessageQueue.Enqueue(currentPlayerName + SEPARATOR + "newturn");
            Thread.Sleep(SLEEP_TIME);

            // Increment the turn information
            this.TotalRounds++;
            // Indicates which player will play the next turn, resets to the first one if we reach the last player
            this.CurrentRound = (this.CurrentRound + 1) % this.Server.Names.Count();
        }

        /// <summary>
        /// Returns all the players location as string
        /// </summary>
        /// <returns></returns>
        private string GetPlayerPos()
        {
            string str = "";
            Random rnd = new Random();

            foreach (var player in this.Server.Names)
            {
                int x = rnd.Next(100, 300); // TO CHANGE ACCORDING TO BOARD SIZE
                int y = rnd.Next(100, 300); // TO CHANGE ACCORDING TO BOARD SIZE

                // format the string
                // (player1;x;y)(player2;x;y) etc..
                str += "(" + player.Value + SEPARATOR + x + SEPARATOR + y + ")";
            }

            return str;
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        public void StopGame()
        {
            // Interrupt the server
            this.Server.Shutdown();
        }
        #endregion
    }
}
