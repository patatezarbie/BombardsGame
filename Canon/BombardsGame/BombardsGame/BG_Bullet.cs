using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombardsGame
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
        #endregion

        #region fields
        public int _x, _y;
        private int _toX, _toY;

        private int _angle;
        private int _velocity;
        private Stopwatch _stp;


        #endregion

        #region construcotrs
        public BG_Bullet(int x, int y, int angle, int velocity)
        {
            this._x = x;
            this._y = y;
            this._angle = angle;
            this._velocity = velocity;
            _stp = new Stopwatch();
            _stp.Start();

            Destination();

        }
        #endregion

        #region methods

        public void Destination()
        {
            _toX = 500;
            /*_toY = 200;*/
        }

        public void Draw(PaintEventArgs e)
        {
            // Calcul du X en fonction du temps
            double factor = (Convert.ToDouble(this._stp.ElapsedMilliseconds) / Convert.ToDouble(TIMETOTARGET)) * 1000;

            int newX = this._x + Convert.ToInt32((this._toX - this._x) * factor);
            int newY = (int)(0.5 * -0.5 * Math.Pow(factor, 2) + _velocity * Math.Sin(_angle) * factor);


            e.Graphics.FillEllipse(Brushes.Red, newX, newY, RADIUS, RADIUS);

            if (_stp.ElapsedMilliseconds >= TIMETOTARGET)
            {
                _stp.Stop();
            }
            e.Graphics.DrawEllipse(Pens.Black, _x, _y, RADIUS, RADIUS);
        }
        #endregion


    }
}
