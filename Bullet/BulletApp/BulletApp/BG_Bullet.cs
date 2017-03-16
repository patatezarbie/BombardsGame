/* Author : Kevin Amado & Lucien Camuglia
 * Class  : T.IS-E2A
 * Date   : 16.03.17
 * Version : 2.0
 * Description : Create a bullet and display position of it.
 */ 


using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BulletApp
{
    class BG_Bullet
    {
        #region constants
        const double GRAVITY = 9; // m/s²
        const int RADIUS = 10; //px
        const int DISPLAY_TIME = 250; //ms
        #endregion
        

        #region fields
        //actual postionts of bullet
        private int _x, _y;
        //initial position of bullet
        private int _xInit, _yInit;  
        //angle of shoot
        private int _angle;
        //bullet's velocity
        private int _velocity;
        private Stopwatch _stp;            
        #endregion

        #region construcotrs
        /// <summary>
        /// Create a new bullet
        /// </summary>
        /// <param name="x">location x</param>
        /// <param name="y">location y</param>
        /// <param name="angle">angle of the bullet</param>
        /// <param name="velocity">velocity of the bullet</param>
        public BG_Bullet(int x, int y, int angle, int velocity)
        {
            this._x = this._xInit = x;
            this._y = this._yInit = y;
            this._angle = angle;
            this._velocity = velocity;            
            _stp = new Stopwatch();
            _stp.Start();         
        }
        #endregion

        #region methods

        /// <summary>
        /// Calculate the position of the bullet and draw the bullet
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        public void Draw(PaintEventArgs e)
        {
            //transfome angle form degres to radians
            double angle_rad = this._angle * Math.PI / 180;

            // time of the MRUA of the bullet
            double t = (double)_stp.ElapsedMilliseconds / DISPLAY_TIME;

            // http://www.sem-experimentation.ch/~math/spip.php?article415
            // MRU
            _x = _yInit - Convert.ToInt32(Convert.ToDouble(_velocity) * Math.Cos(angle_rad) * t);
            // MRUA - 0.5 is the 1/2 for MRUA formule
            _y = _yInit - Convert.ToInt32(Convert.ToDouble(_velocity) * Math.Sin(angle_rad) * t + 0.5d * -GRAVITY * Math.Pow(t, 2));   

            // draw bullet
            e.Graphics.FillEllipse(Brushes.Red, _x, _y, RADIUS, RADIUS);
            e.Graphics.DrawEllipse(Pens.Black, _x , _y, RADIUS, RADIUS);
        }
        #endregion


    }
}
