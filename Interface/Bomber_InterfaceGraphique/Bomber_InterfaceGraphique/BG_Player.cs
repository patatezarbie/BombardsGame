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
using System.Threading;

namespace Bomber_InterfaceGraphique
{
    public class BG_Player
    {
        #region Consts
        #endregion

        #region Fields
        private BG_Cannon _cannon;
        /// <summary>
        /// Represent an history an all the player touched as Hit object
        /// </summary>
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
            string regex = "\\(([^,;.]+);([0-9]{1,3});([0-9]{1,3})\\)";

            MatchCollection matches = Regex.Matches(input, regex);

            foreach (Match match in matches)
            {
                string[] values = Regex.Split(match.Value, regex);

                string name = values[1];
                int x = int.Parse(values[2]);
                int y = int.Parse(values[3]);

                Console.WriteLine("name : {0}{3}x : {1}{3}y : {2}", name, x, y, Environment.NewLine);
            }
        }

        public void MoveCannon(BG_Location location)
        {
            this.Cannon.Move(location);
        }

        public void RotateCannon(float angle)
        {
            this.Cannon.AdjustAngle(angle);
        }
        #endregion
    }
}
