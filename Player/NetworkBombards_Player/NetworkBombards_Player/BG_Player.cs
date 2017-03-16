using System;
/*
 * - Network Bombard Game -
 * V.HAURY & C.DOS REIS
 * 02.03.2017
 * V1.0
 * Player Class
 */
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetworkBombards_Player
{
    public class BG_Player
    {
        #region Consts
        #endregion

        #region Fields
        private BG_Cannon _cannon;
        // Represent an history an all the player touched as Hit object
        private List<BG_Hit> _history;
        private string _name;
        private bool _isPlaying;
        //private bool _isDead;
        #endregion

        #region Propperties
        public BG_Cannon Cannon
        {
            get { return _cannon; }
            set { _cannon = value; }
        }

        public int Score
        {
            get { return this.History.Count; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set { _isPlaying = value; }
        }

        public List<BG_Hit> History
        {
            get { return _history; }
            set { _history = value; }
        }

        /*
        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }
        */
        #endregion

        #region Constructor
        /// <summary>
        /// Create a Player [Designated constructor]
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="cannon">Represent the canon of the player</param>
        public BG_Player(string name, BG_Cannon cannon)
        {
            //this.Cannon = new BG_Cannon();
            this.History = new List<BG_Hit>();
            this.Name = name;
            this.IsPlaying = false;
            this.Cannon = cannon;
        }

        /// <summary>
        /// Create a Player
        /// </summary>
        /// <param name="cannon">Represent the canon of the player</param>
        public BG_Player(BG_Cannon cannon)
            : this("noname", cannon)
        {
            // No code
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tels the cannon to shoot
        /// </summary>
        public void shoot()
        {
            if (IsPlaying)
                this._cannon.Shoot();
        }

        /// <summary>
        /// Reset characteristics 
        /// </summary>
        public void Reset()
        {
            //this.IsDead = false;
            this.IsPlaying = false;
            this.History.Clear();
        }

        /// <summary>
        /// Connect to the server
        /// </summary>
        public void Connect()
        {
            // Waiting for Server/Client codes...
        }

        /// <summary>
        /// Disconnect from the server
        /// </summary>
        public void Disconnect()
        {
            // Waiting for Server/Client codes...
        }

        /// <summary>
        /// At each tick we send info about the player and canon to the server/game master
        /// </summary>
        public void Update()
        {
            // Waiting for Server/Client codes...
        }

        /// <summary>
        /// Add an element in the history
        /// </summary>
        /// <param name="hit">Represent the hit that touched a player</param>
        public void AddHistory(BG_Hit hit)
        {
            this.History.Add(hit);
        }

        /// <summary>
        /// Define the cannon movement 
        /// </summary>
        /// <param name="str"></param>
        public void MoveFromString(string input)
        {
            string regex = "\\([^,;.]+;[0-9]{0,3};[0-9]{0,3}\\)";
            string[] substrings = Regex.Split(input, regex);
            foreach (string match in substrings)
            {
                Console.WriteLine("'{0}'", match);
            }
        }
        #endregion
    }
}
