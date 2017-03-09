using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombardsServer
{
    class Program
    {
        public static Server bombardsRoom;

        protected static void InterruptHandler(object sender, ConsoleCancelEventArgs args)
        {
            bombardsRoom.Shutdown();
            args.Cancel = true;
        }

        public static void Main(string[] args)
        {
            // Create the server
            string name = "Bombards CFPT";
            int port = 8000;
            bombardsRoom = new Server(name, port);

            // Add a handler for a Ctrl-C press
            Console.CancelKeyPress += InterruptHandler;

            // run the chat server
            bombardsRoom.Run();
        }
    }
}
