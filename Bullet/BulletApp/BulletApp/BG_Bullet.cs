using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletApp
{
    class BG_Bullet
    {
        #region constants
        const int GRAVITY = 9;
        const int RADIUS = 10; //px
        #endregion

        #region properties
      /*  public float VelocityX { get; set; }
        public float VelocityY { get; set; }*/      
        #endregion

        #region fields
        private int _x, _y;
      
        private int _angle;
        private int _velocity;
        private Stopwatch _stp;

        #endregion

        #region construcotrs
        public BG_Bullet(int x, int y,int angle,int velocity)
        {
            this._x = x;
            this._y = y;
            this._angle = angle;
            this._velocity = velocity;
            _stp = new Stopwatch();
            _stp.Start();

        }
        #endregion

        #region methods
        public void Draw()
        {
            //TODO by 
        }
        #endregion


    }
}
