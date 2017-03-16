/* Author : Kevin Amado & Lucien Camuglia
 * Class  : T.IS-E2A
 * Date   : 16.03.17
 * Description : 
 * 
 * 
 */ 


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulletApp
{
    class BG_Bullet
    {
        #region constants
        const double GRAVITY = 9;
        const int RADIUS = 10; //px
        const int DISPLAY_TIME = 250; //ms
        #endregion

        #region properties
        public double _dx { get; set; }
        #endregion

        #region fields
        private int _x, _y;
        private int _xInit, _yInit;     
        private int _angle;
        private int _velocity;
        private Stopwatch _stp;     
        public int Test = 0;
        
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
            this._dx = 0;
                   
        }
        #endregion

        #region methods

        /// <summary>
        /// Calculate the position of the bullet and draw the bullet
        /// </summary>
        /// <param name="e"></param>
        public void Draw(PaintEventArgs e)
        {
            double angle_rad = this._angle * Math.PI / 180;
            // time of the MRUA of the bullet
            double t = (double)_stp.ElapsedMilliseconds / DISPLAY_TIME;

            // http://www.sem-experimentation.ch/~math/spip.php?article415
            // MRU
            _x = _yInit - Convert.ToInt32(Convert.ToDouble(_velocity) * Math.Cos(angle_rad) * t);
            // MRUA
            _y = _yInit - Convert.ToInt32(Convert.ToDouble(_velocity) * Math.Sin(angle_rad) * t + 0.5d * -GRAVITY * Math.Pow(t, 2));   

            // draw bullet
            e.Graphics.FillEllipse(Brushes.Red, _x + Convert.ToInt32(_dx), _y, RADIUS, RADIUS);
            e.Graphics.DrawEllipse(Pens.Black, _x + Convert.ToInt32(_dx), _y, RADIUS, RADIUS);
        }
        #endregion


    }
}
