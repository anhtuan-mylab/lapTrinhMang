using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace simpleTCPClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Khong the ket noi den Server.");
                Console.WriteLine(se.ToString());
                return;
            }

            int recv = server.Receive(data);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);


            string welcome = "Hello Friend (client -> server)";
            data = Encoding.ASCII.GetBytes(welcome);
            server.Send(data, data.Length, SocketFlags.None);

            server.Close();

            Console.ReadLine();


        }
    }
}
