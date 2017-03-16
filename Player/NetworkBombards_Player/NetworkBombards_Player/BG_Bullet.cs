using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkBombards_Player
{
    public class BG_Bullet
    {
        #region constants
        const int GRAVITY = 9;
        const int RADIUS = 10; //px
        const int TIMETOTARGET = 3000; //ms
        #endregion

        #region properties
      /*  public float VelocityX { get; set; }
        public float VelocityY { get; set; }*/
        public double _dx { get; set; }
        #endregion

        #region fields
        private int _x, _y;
        private int _toX, _toY;
        private int _xInit, _yInit;
      
        private int _angle;
        private int _velocity;
        private Stopwatch _stp; 

        public int Test = 0;
        
        #endregion

        #region construcotrs
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

        public void Draw(PaintEventArgs e)
        {
            if (_stp.ElapsedMilliseconds >= 1)
            {
                _dx++;
                _stp.Restart();
            }

            double angle_rad = _angle * Math.PI / 180;

            // https://fr.wikipedia.org/wiki/Trajectoire_d'un_projectile
            //              Y0            +                dx  *      tan(angle)  - (    g    *          dx * dx  ) / (2 *         (                     v      *    cos(angle)) carré)   
            _y = _yInit - Convert.ToInt32(Convert.ToDouble(0) + _dx * Math.Tan(angle_rad) - ((GRAVITY * Math.Pow(_dx, 2)) / (2d * Math.Pow(Convert.ToDouble(_velocity) * Math.Cos(angle_rad), 2))));
             
            // draw bullet
            e.Graphics.FillEllipse(Brushes.Red, _x + Convert.ToInt32(_dx), _y, RADIUS, RADIUS);
            e.Graphics.DrawEllipse(Pens.Black, _x + Convert.ToInt32(_dx), _y, RADIUS, RADIUS);
        }
        #endregion


    }
}
