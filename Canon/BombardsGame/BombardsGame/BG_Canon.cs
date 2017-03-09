/*
    Authors             : Julien Faeh, Jordan Dacuna Rodriguez 
    Description         :
    Version             : v1 - 09/03/17
    Class               : T.IS.E2B
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombardsGame
{
    public class BG_Canon
    {
        #region Constantes
        const float DEFAULT_ROTATION = 0.0f;
        const int DEFAULT_BODY_SIZE_X = 50;
        const int DEFAULT_BODY_SIZE_Y = 50;
        const int DEFAULT_CANON_SIZE_X = 20;
        const int DEFAULT_CANON_SIZE_Y = 50;
        const int DEFAULT_ANGLE_MAX = 360;
        #endregion

        #region fields
        private Brush defaultBulletBrush;
        private Brush defaultCanonBrush;
        private Rectangle RecBody;
        private Rectangle RecCanon;
        private bool permissionToFire;//test
        #endregion

        #region Properties
        public float Rotation { get; set; }
        public Brush BobyBrush { get; set; }
        public Size BodySize { get; set; }
        public Size CanonSize { get; set; }
        public Point Location { get; set; }
        public int Force { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Designated constructor 
        /// </summary>
        /// <param name="rotation">Angle setter</param>
        /// <param name="bColor">Color of body canon</param>
        /// <param name="location">Location of the canon</param>
        public BG_Canon(float rotation, Color bColor, Point location)
        {
            this.Rotation = rotation;
            this.Location = location;

            this.BobyBrush = new SolidBrush(bColor);
            this.defaultCanonBrush = new SolidBrush(Color.Black);
            this.defaultBulletBrush = new SolidBrush(Color.Gray);

            this.BodySize = new Size(DEFAULT_BODY_SIZE_X, DEFAULT_BODY_SIZE_Y);
            this.CanonSize = new Size(DEFAULT_CANON_SIZE_X, DEFAULT_CANON_SIZE_Y);
            
            this.RecBody = new Rectangle(Location, this.BodySize);
            this.RecCanon = new Rectangle(new Point(0, 0), this.CanonSize);
            this.permissionToFire = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bColor">Color of body canon</param>
        /// <param name="location">Location of the canon</param>
        public BG_Canon(Color bColor, Point location)
            :this(DEFAULT_ROTATION, bColor, location)
        {
            //no code
        }
        #endregion

        #region Methods
        /// <summary>
        /// Move the canon to another position
        /// </summary>
        /// <param name="location">New Position</param>
        public void Move(Point location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Turn the canon with an angle
        /// </summary>
        /// <param name="value">New angle</param>
        public void ajustAngle(float value)
        {
            this.Rotation += value;

            if (this.Rotation >= DEFAULT_ANGLE_MAX)
            {
                this.Rotation -= DEFAULT_ANGLE_MAX; //This set the angle to a number lower than 360 
            }            
        }

        /// <summary>
        /// Create a new bullet object and shoot
        /// </summary>
        public void shoot()
        {
            permissionToFire = true;
        }

        /// <summary>
        /// Drawing the all parts of the canon
        /// </summary>
        /// <param name="e">Drawing zone</param>
        public void draw(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(Location.X, Location.Y); 
            e.Graphics.RotateTransform(this.Rotation);

            RecBody.Location = new Point(0 - this.RecBody.Width /2, 0 - this.RecBody.Height/2);

            e.Graphics.FillRectangle(defaultCanonBrush, this.RecCanon.Location.X - this.RecCanon.Width / 2, this.RecCanon.Y, this.RecCanon.Width, this.RecCanon.Height);

            e.Graphics.FillEllipse(BobyBrush, this.RecBody);

            //This represent the center of the rotation
            //e.Graphics.FillEllipse(Brushes.Red, -5, -5, 10, 10);
            
            if(permissionToFire)
            {
                e.Graphics.FillRectangle(defaultBulletBrush, RecCanon.Location.X - RecCanon.Width / 4, RecCanon.Location.Y + RecCanon.Height, 10, 10);
                permissionToFire = false;
            }           

            e.Graphics.ResetTransform();
        }
        #endregion
    }
}
