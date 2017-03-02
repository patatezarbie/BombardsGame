using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Field_Location_SampleProject
{
    public class BG_Location
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public BG_Location(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        } 
    }
}
