using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    class BG_PowerBar
    {
        #region Constants
        private const float START_VALUE = 0;
        private const float MAX_VALUE = 100;
        private const int ADJUSTER = 5;
        private const bool DEFAULT_VISIBLE = true;
        private const bool STARTED_STATE = false;
        private const float DEFAULT_WIDTH = 200;
        private const float DEFAULT_HEIGHT = 30;
        #endregion

        #region Properties
        private bool _isStarted;

        public bool IsVisible { get; set; }
        public BG_PowerBarBackground Background { get; set; }
        public BG_PowerBarProgress Progress { get; set; }
        public Timer TimePorgress { get; set; }
        
        /// <summary>
        /// Get or set if the progress bar change the value
        /// </summary>
        private bool IsStarted
        {
            get
            {
                return this._isStarted;
            }
            set
            {
                if (value) // Enable or disable the timer
                    this.TimePorgress.Start();
                else
                    this.TimePorgress.Stop();

                this._isStarted = value;
            }
        }

        /// <summary>
        /// Get the normalized power
        /// </summary>
        public float NormalizedPower
        {
            get
            {
                return this.Progress.Value / this.Progress.MaxValue;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Create new Powerbar
        /// </summary>
        /// <param name="pX">X position</param>
        /// <param name="pY">Y position</param>
        /// <param name="pWidth">Width</param>
        /// <param name="pHeight">Height</param>
        public BG_PowerBar(float pX, float pY, float pWidth, float pHeight)
        {
            this.Background = new BG_PowerBarBackground(pX, pY, pHeight, pWidth);
            this.Progress = new BG_PowerBarProgress(START_VALUE, MAX_VALUE, this.Background.X + ADJUSTER, this.Background.Y + ADJUSTER, this.Background.Width - (ADJUSTER * 2), this.Background.Height - (ADJUSTER * 2));

            this.TimePorgress = new Timer();
            this.TimePorgress.Interval = 1;
            this.TimePorgress.Tick += TimePorgress_Tick;
            this.TimePorgress.Stop();

            this.IsStarted = STARTED_STATE;
            this.IsVisible = DEFAULT_VISIBLE;
        }

        /// <summary>
        /// Create new Powerbar
        /// </summary>
        /// <param name="pX">X position</param>
        /// <param name="pY">Y position</param>
        public BG_PowerBar(float pX, float pY)
            : this(pX, pY, DEFAULT_WIDTH, DEFAULT_HEIGHT)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Event to each timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimePorgress_Tick(object sender, System.EventArgs e)
        {
            if (this.IsVisible)
                this.Progress.Value++; // Increment the value
        }

        /// <summary>
        /// Enable or disable the progress event
        /// </summary>
        private void StartStopProgress()
        {
            if (this.IsVisible)
                this.IsStarted = !this.IsStarted;
        }

        public void StartProgress()
        {
            if (!this.IsStarted)
                this.StartStopProgress();
        }

        public void StopProgress()
        {
            if (this.IsStarted)
                this.StartStopProgress();
        }



        /// <summary>
        /// Draw element
        /// </summary>
        /// <param name="pe"></param>
        public void Draw(PaintEventArgs pe)
        {
            if (this.IsVisible)
            {
                this.Background.Draw(pe);
                this.Progress.Draw(pe);
            }
        }
        #endregion
    }
}
