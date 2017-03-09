using System.Drawing;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    class BG_PowerBarBackground
    {
        #region Constants
        private static readonly Color DEFAULT_COLOR = Color.FromArgb(174, 137, 100);
        private const float DEFAULT_WIDTH = 200;
        private const float DEFAULT_HEIGHT = 30;
        private const float DEFAULT_X = 0;
        private const float DEFAULT_Y = 100;
        #endregion

        #region Properties
        public float X { get; set; }
        public float Y { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }
        #endregion

        #region Constructor
        public BG_PowerBarBackground(float pX, float pY, float pHeight, float pWidth, Color pColor)
        {
            this.Height = pHeight;
            this.Width = pWidth;
            this.X = pX;
            this.Y = pY;
            this.Pen = new Pen(pColor);
            this.Brush = new SolidBrush(pColor);
        }

        public BG_PowerBarBackground(float pX, float pY, float pHeight, float pWidth)
            : this(pX, pY, pHeight, pWidth, DEFAULT_COLOR)
        {

        }

        public BG_PowerBarBackground(float pX, float pY)
            : this(pX, pY, DEFAULT_HEIGHT, DEFAULT_WIDTH)
        {

        }

        public BG_PowerBarBackground()
            : this(DEFAULT_X, DEFAULT_Y)
        {
        
        }
        #endregion

        #region Methods
        public void Draw(PaintEventArgs pe)
        {
            pe.Graphics.DrawRectangle(this.Pen, this.X, this.Y, this.Width, this.Height);
            pe.Graphics.FillRectangle(this.Brush, this.X, this.Y, this.Width, this.Height);
        }
        #endregion
    }
}
