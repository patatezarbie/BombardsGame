using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkBombards_Player
{
    class Program
    {
        static void Main(string[] args)
        {

            BG_Player player = new BG_Player("Player", new BG_Cannon(Color.Black, new BG_Location(10, 10)));
            player.MoveFromString("(Player;20;20)(Player2;20;20)(Player3;20;20)");

        }
    }
}
