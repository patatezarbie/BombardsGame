using System;
/*
    Authors             : Julien Faeh, Jordan Dacuna Rodriguez 
    Description         : This class represent a cannon control by a player and he can adjust the angle and move it to another position.
                          The cannon can shoot and be drawed.
    Version             : v1 - 09/03/17
    Class               : T.IS.E2B
 */
using System.Drawing;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    public class BG_Cannon
    {
        #region Constantes
        const float DEFAULT_ROTATION = 0.0f;
        const int DEFAULT_BODY_SIZE_X = 50;
        const int DEFAULT_BODY_SIZE_Y = 50;
        const int DEFAULT_CANNON_SIZE_X = 50;
        const int DEFAULT_CANNON_SIZE_Y = 20;
        const int DEFAULT_ANGLE_MAX = 360;
        const int DEFAULT_ANGLE_MIN = 0;
        #endregion

        #region fields
        private Brush defaultBulletBrush;
        private Brush defaultCannonBrush;
        private Rectangle RecBody;
        private Rectangle RecCannon;       
        #endregion

        #region Properties
        public float Rotation { get; set; }
        public Brush BobyBrush { get; set; }
        public Size BodySize { get; set; }
        public Size CannonSize { get; set; }
        public BG_Location Location { get; set; }
        public int Force { get; set; }
        public BG_Bullet Bullet { get; set; }
        public bool PermissionToFire { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Designated constructor 
        /// </summary>
        /// <param name="rotation">Angle setter</param>
        /// <param name="bColor">Color of body canon</param>
        /// <param name="location">Location of the canon</param>
        public BG_Cannon(float rotation, Color bColor, BG_Location location)
        {
            this.Rotation = rotation;
            this.Location = location;

            this.BobyBrush = new SolidBrush(bColor);
            this.defaultCannonBrush = new SolidBrush(Color.Black);
            this.defaultBulletBrush = new SolidBrush(Color.Gray);

            this.BodySize = new Size(DEFAULT_BODY_SIZE_X, DEFAULT_BODY_SIZE_Y);
            this.CannonSize = new Size(DEFAULT_CANNON_SIZE_X, DEFAULT_CANNON_SIZE_Y);
            
            this.RecBody = new Rectangle(Location.PosX, Location.PosY, this.BodySize.Width, this.BodySize.Height);
            this.RecCannon = new Rectangle(new Point(0, 0), this.CannonSize);
            this.PermissionToFire = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bColor">Color of body canon</param>
        /// <param name="location">Location of the canon</param>
        public BG_Cannon(Color bColor, BG_Location location)
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
        public void Move(BG_Location location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Turn the canon with an angle
        /// </summary>
        /// <param name="value">New angle</param>
        public void AdjustAngle(float value)
        {
            this.Rotation += value;

            if (this.Rotation >= DEFAULT_ANGLE_MAX)
            {
                
                this.Rotation -= DEFAULT_ANGLE_MAX; //This set the angle to a number lower than 359 
            }    
            if(this.Rotation < DEFAULT_ANGLE_MIN)
            {
                
                this.Rotation += DEFAULT_ANGLE_MAX;
            }
           
        }

        /// <summary>
        /// Create a new bullet object and shoot
        /// </summary>
        public void Shoot()
        {
            PermissionToFire = true;
        }

        /// <summary>
        /// Drawing the all parts of the canon
        /// </summary>
        /// <param name="e">Drawing zone</param>
        public void Draw(PaintEventArgs e)
        {
            float ShootAngle = 0;

            //BULLET
            e.Graphics.TranslateTransform(Location.PosX, Location.PosY);
            if (PermissionToFire)
            {
                //Calcule the angle to have the cannon look up when it's a 90°
                ShootAngle = this.Rotation + DEFAULT_ANGLE_MAX;
                ShootAngle = ShootAngle - (ShootAngle - (DEFAULT_ANGLE_MAX - this.Rotation));

                Bullet = new BG_Bullet(RecCannon.Location.X, RecCannon.Location.Y - 5, (int)ShootAngle, 99);

                Bullet.Draw(e);
                PermissionToFire = false;
            }

            if (Bullet != null)
            {
                Bullet.Draw(e);
            }
           
            //CANNON
            e.Graphics.RotateTransform((int)this.Rotation);

            RecBody.Location = new Point(0 - this.RecBody.Width /2, 0 - this.RecBody.Height/2);
            e.Graphics.FillRectangle(defaultCannonBrush, this.RecCannon.Location.X, this.RecCannon.Y - this.RecCannon.Height / 2 , this.RecCannon.Width, this.RecCannon.Height);
            e.Graphics.FillEllipse(BobyBrush, this.RecBody);           

            //This represent the center of the rotation. Use this in case of test.
            e.Graphics.FillEllipse(Brushes.Blue, -5, -5, 10, 10);                
        }
        #endregion


    }
}
