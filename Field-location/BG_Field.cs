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
        private const int FIELD_HEIGHT_RANGE = 190;

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
            int quarter = FIELD_HEIGHT_RANGE;
            this._maxFieldHeight = quarter;
            this._minFieldHeight = this.Height - quarter;
        }

        private void GenerateField()
        {
            int oldPosX;
            int posX;
            int centerPoint;

            int mid = (this._maxFieldHeight + this._minFieldHeight) / 2;
            int y = 0;
            for (int i = 0; i < this.Width; i++)
            {
                if (i % POINT_INTERVAL == 0)
                {
                    y = this._rnd.Next(mid - 10, mid + 10);
                    this.Locations.Add(new BG_Location(i, y));
                } 
            }

            for (int i = Locations.Count() - 1; i > 0; i--)
            {
                oldPosX = Locations[i].PosX;
                posX = Locations[i - 1].PosX;

                centerPoint = (oldPosX + posX) / 2;
                y = this._rnd.Next(this._maxFieldHeight, this._minFieldHeight);

                this.Locations.Insert(i, new BG_Location(centerPoint, y));
            }
        }

    }
}
