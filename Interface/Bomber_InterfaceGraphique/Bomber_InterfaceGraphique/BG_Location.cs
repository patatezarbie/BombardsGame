/* 
 * Robin Plojoux, Dylan Wacker - CFPT-I  
 * 09.03.2017
 * POO 
 * POO tech project - BG_Location.cs
 * 
 * Description : Location class for the bombard game
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber_InterfaceGraphique
{
    public class BG_Location
    {
        // Properties
        public int PosX { get; set; }
        public int PosY { get; set; }

        // Constructors
        public BG_Location(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        } 
    }
}
