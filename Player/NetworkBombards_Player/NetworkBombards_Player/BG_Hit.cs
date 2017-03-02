using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkBombards_Player
{
    public class BG_Hit
    {
        #region Consts
        #endregion

        #region Fields
        #endregion

        #region Propperties
        public BG_Location Location{ get; set; }
        public BG_Cannon Cannon { get; set; }
        #endregion

        #region Constructor

        // Designated constructor
        public BG_Hit(BG_Location location, BG_Cannon cannon)
        {
            this.Location = location;
            this.Cannon = cannon;
        }
        #endregion

        #region Methods
       
        #endregion
    }
}
