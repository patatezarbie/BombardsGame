using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombardsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get a name
            Console.Write("Enter a name to use: ");
            string name = Console.ReadLine();

            // Setup the Messenger
            string host = "127.0.0.1";//args[0].Trim();
            int port = 8000;//int.Parse(args[1].Trim());
            Player user = new Player(host, port, name);

            // connect and send messages
            user.Connect();
            user.SendMessages();
        }
    }
}
