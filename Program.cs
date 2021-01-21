using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("127.0.0.1",5050);
            client.Start();
            User u;
            string user;
            string password;
            while (true)
            {
                Console.WriteLine("Add User:");
                user = Console.ReadLine();
                Console.WriteLine("Add Password:");
                password = Console.ReadLine();
                u = new User(user,password);
                client.SendObject(u);
                //Received from server
                Console.WriteLine("Message from Server: " + client.Received());
            }
        }
    }
}
