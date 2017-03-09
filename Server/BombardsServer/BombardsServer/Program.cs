using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombardsServer
{
    class Program
    {
        public static BG_GameManager bombardsGame;

        protected static void InterruptHandler(object sender, ConsoleCancelEventArgs args)
        {
            bombardsGame.StopGame();
            args.Cancel = true;
        }

        public static void Main(string[] args)
        {
            // Create the game manager
            

            string name = "Bombards CFPT";
            int port = 8000;
            bombardsGame = new BG_GameManager(name, port, 42);

            // Add a handler for a Ctrl-C press
            Console.CancelKeyPress += InterruptHandler;

            // run the server
            bombardsGame.GameLoop();
        }
    }
}
