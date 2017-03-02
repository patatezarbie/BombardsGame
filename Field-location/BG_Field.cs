using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Field_Location_SampleProject
{
    class BG_Field
    {
        // Constants
        private const int POINT_INTERVAL = 30;
        private const int MIN_FIELD_HEIGTH = 0;
        private const int MAX_FIELD_HEIGTH = 0;

        // Variables
        private Random _rnd;
        private int _minFieldHeight;
        private int _maxFieldHeight;

        // Properties
        public List<BG_Location> Locations { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        

        // Constructors
        public BG_Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Locations = new List<BG_Location>();
            this._rnd = new Random();

            SetBoundaries();

            GenerateField();
        }

        private void SetBoundaries()
        {
            int quarter = this.Height / 3;
            this._maxFieldHeight = quarter;
            this._minFieldHeight = this.Height - quarter;
        }

        private void GenerateField()
        {
            int y = this._maxFieldHeight;
            for (int i = 0; i < this.Width; i++)
            {
                if (i % POINT_INTERVAL == 0)
                {
                    //y = this._rnd.Next(this._minFieldHeight, this._maxFieldHeight);
                    this.Locations.Add(new BG_Location(i, y));
                } 
            }
        }

    }
}
