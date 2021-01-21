using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Serialization;

namespace Client
{
    public class Client
    {
        IPHostEntry host;
        IPAddress ipAdress;
        IPEndPoint endPoint;

        Socket s_Client;

        public Client(string ip,int port)
        {
            host = Dns.GetHostEntry(ip);
            ipAdress = host.AddressList[0];
            endPoint = new IPEndPoint(ipAdress, port);
            //Init Client
            s_Client = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Start()
        {
            s_Client.Connect(endPoint);
        }
        public void Send(string msg)
        {
            byte[] byteMsg = Encoding.ASCII.GetBytes(msg);
            s_Client.Send(byteMsg);
            Console.WriteLine("Message Sended.");
        }
        public string Received()
        {
            byte[] buffer = new byte[1024];
            s_Client.Receive(buffer);
            return byte2String(buffer);
        }
        public void SendObject(object toSend)
        {
            s_Client.Send(BinarySerialization.Serializate(toSend));
        }
        public string byte2String(byte[] buffer)
        {
            string message;
            int endIndex;
            message = Encoding.ASCII.GetString(buffer);
            endIndex = message.IndexOf('\0');
            if (endIndex > 0)
                message = message.Substring(0, endIndex);
            return message;
        }
    }
}
