﻿using System.Drawing;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    class BG_PowerBarProgress
    {
        #region Constants
        private static readonly Color DEFAULT_COLOR = Color.FromArgb(49, 140, 231);
        private const float DEFAULT_WIDTH = 100;
        private const float DEFAULT_HEIGHT = 15;
        private const float DEFAULT_X = 0;
        private const float DEFAULT_Y = 100;
        private const float DEFAULT_MAX_VALUE = 100;
        private const float DEFAULT_VALUE = 0;
        #endregion

        #region Properties
        private float _value;
        public float X { get; set; }
        public float Y { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float MaxValue { get; set; }
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }

        public float Size
        {
            get
            {
                return (this.Value * this.Width) / this.MaxValue;
            }
        }

        public float Value
        {
            get { return this._value; }
            set
            {
                this._value = (value > this.MaxValue) ? DEFAULT_VALUE : value;
            }
        }
        #endregion

        #region Constructor
        public BG_PowerBarProgress(float pValue, float pMaxValue, float pX, float pY, float pWidth, float pHeight, Color pColor)
        {
            this.Value = pValue;
            this.MaxValue = pMaxValue;
            this.X = pX;
            this.Y = pY;
            this.Width = pWidth;
            this.Height = pHeight;
            this.Pen = new Pen(pColor);
            this.Brush = new SolidBrush(pColor);
        }

        public BG_PowerBarProgress(float pValue, float pMaxValue, float pX, float pY, float pWidth, float pHeight)
            : this(pValue, pMaxValue, pX, pY, pWidth, pHeight, DEFAULT_COLOR)
        {

        }

        public BG_PowerBarProgress(float pValue, float pMaxValue, float pX, float pY)
            : this(pValue, pMaxValue, pX, pY, DEFAULT_WIDTH, DEFAULT_HEIGHT)
        {

        }

        public BG_PowerBarProgress(float pValue, float pMaxValue)
            : this(pValue, pMaxValue, DEFAULT_X, DEFAULT_Y)
        {

        }

        public BG_PowerBarProgress(float pValue)
            : this(pValue, DEFAULT_MAX_VALUE)
        {

        }

        public BG_PowerBarProgress()
            : this(DEFAULT_VALUE)
        {

        }
        #endregion

        #region Methods
        public void Draw(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(this.Brush, this.X, this.Y, this.Size, this.Height);
        }
        #endregion
    }
}
