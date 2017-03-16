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
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomColorName);

            BG_Player player = new BG_Player("Player", new BG_Cannon(Color.FromName(randomColorName.ToString()), new BG_Location(10, 10)),"127.0.0.1", 50);
            player.MoveFromString("(Player;20;20)(Player2;20;20)(Player3;20;20)");

        }
    }
}
