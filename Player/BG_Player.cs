﻿/*
 * - Network Bombard Game -
 * V.HAURY & C.DOS REIS
 * 02.03.2017
 * V1.0
 * Player Class
 */
using System.Collections.Generic;

namespace NetworkBombards_Player
{
    public class BG_Player
    {
        #region Consts
        #endregion

        #region Fields
        private BG_Cannon _cannon;
        private List<BG_Hit> _history;
        private int _score;
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
            get { return _score; }
            set { _score = value; }
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

        // Designated constructor
        public BG_Player(string name)
        {
            //this.Cannon = new BG_Cannon();
            this.History = new List<BG_Hit>();
            this.Name = name;
            this.Score = this.History.Count;
            this.IsPlaying = false;
        }

        public BG_Player()
            : this("DefaultName")
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
            this._cannon.Shoot();
        }

        /// <summary>
        /// Reset characteristics 
        /// </summary>
        public void Reset()
        {
            //this.IsDead = false;
            this.IsPlaying = false;
            this.Score = 0;
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
        public void Update() {

            this.Score = this.History.Count;
            // Waiting for Server/Client codes...
        }
        #endregion
    }
}
