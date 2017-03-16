/* 
 * Robin Plojoux, Dylan Wacker - CFPT-I  
 * 16.03.2017
 * POO 
 * POO tech project - BG_Field.cs
 * 
 * Description : Generate field points for the bombard game
 *               Code based on the Midpoint Displacement in One Dimension algorithm
 *               Explanation of the algorithm at http://www.gameprogrammer.com/fractal.html
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Field_Location_SampleProject
{
    class BG_Field
    {
        // Constants
        // The number of pixel between each points (50 or 100)
        private const int POINT_INTERVAL = 60; // <- /!\ Please note that this value must be adapted manualy depending on the screen size for it to properly work
        // The starting displacement value used for the midpoint displacement algorithm
        private const int DEFAULT_DISPLACE = 35;
        // The displacement reduce value used for the midpoint displacement algorithm
        // *If you change this value between 0.4 and 0.9, the field rougthness will variate
        private const double DISPLACEMENT_REDUCE = 0.6;
        // The moutain value is a interval used to change terrain height variations
        // *Changing the number between 2 and 8 will change the terrain height variations. The lower the number , the more mountains*
        private const int MOUNTAINS_VARIATION = 4;

        // Variables
        private Random _rnd;
        private int _screenCenter;

        // Properties
        public List<BG_Location> Locations { get; set; } // List of field points
        public int Width { get; set; } // Game screen width
        public int Height { get; set; } // Game screen height


        // Constructors
        public BG_Field(int clientWidth, int clientHeight, int seed)
        {
            this.Width = clientWidth;
            this.Height = clientHeight;
            this.Locations = new List<BG_Location>();
            this._rnd = new Random(seed); // Apply seed to random object, therefor the generated map can be recreated identicaly

            this._screenCenter = this.Height / 2; // Screen center
            
            GenerateField();
        }

        // Methods
        /// <summary>
        /// Generate locations used to set the 2D field boundaries
        /// </summary>
        private void GenerateField()
        {
            // Variables
            // Point 1
            int oldPosX;
            int oldPosY;
            // Point 2
            int posX;
            int posY;
            // Center values between point 1 and 2
            int centerY;
            int centerX;

            // Y axis diseplacement on midpoints
            double displace = DEFAULT_DISPLACE;
            int y = 0;

            // Set initial points
            for (int i = 0; i <= this.Width; i++)
            {
                if (i % POINT_INTERVAL == 0)
                {
                    // Dislace initial point up or down (one on two)
                    if ((i / POINT_INTERVAL) % MOUNTAINS_VARIATION == 0)
                    {
                        y = this._rnd.Next(this._screenCenter - (int)displace, this._screenCenter); // The point is place below mid screen
                    }
                    else
                    {
                        y = this._rnd.Next(this._screenCenter, this._screenCenter + (int)displace); // The point is placed over mid screen
                    }
                    
                    // Add point to locations list
                    this.Locations.Add(new BG_Location(i, y));
                }
            }

            // Repetions of the midpoint displacement algorithm on each segment until there is no more midpoint
            // Each run, the number of points is doubled
            while ( (this.Locations[1].PosX - this.Locations[0].PosX) != 0)
            {
                // Start for last point and finishes at first point (right side of the screen to the left)
                for (int i = Locations.Count() - 1; i > 0; i--)
                {
                    // Point 1
                    oldPosX = Locations[i].PosX;
                    oldPosY = Locations[i].PosY;

                    // Point 2
                    posX = Locations[i - 1].PosX;
                    posY = Locations[i - 1].PosY;

                    // Center values between point 1 and 2
                    centerX = (oldPosX + posX) / 2;
                    centerY = (oldPosY > posY) ? oldPosY - (oldPosY - posY) / 2 : posY - (posY - oldPosY) / 2;

                    // Generate random Y axis value 
                    y = this._rnd.Next(centerY - (int)displace, centerY + (int)displace);
                    // Reduce displacement
                    displace *= DISPLACEMENT_REDUCE;
                    // Add new midpoint in locations list
                    this.Locations.Insert(i, new BG_Location(centerX, y));
                }
            }

        }

        /// <summary>
        /// Detection if bullet colides with field
        /// </summary>
        /// <param name="bulletX"></param>
        /// <param name="bulletY"></param>
        /// <returns></returns>
        public bool IsFieldTouched(BG_Location bulletLocation)
        {
            bool isHit = false;
            foreach (BG_Location fieldLocation in this.Locations)
            {
                if (bulletLocation.PosX == fieldLocation.PosX && bulletLocation.PosY >= fieldLocation.PosY)
                {
                    isHit = true;
                }
            }
            return isHit;
        }

    }
}