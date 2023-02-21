using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace simpleTCPServer
{
    class Program
    {
        public static void Main(string[] args)
        {

            byte[] data = new byte[1024];

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("Dang cho ket noi ...");


            Socket client = newsock.Accept();
            Console.WriteLine("Da ket noi thanh cong den Client.\n");

            // Thực hiện trao đổi dữ liệu
            string welcome = "Hello Friend (client -> server)";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);

            int recv = client.Receive(data);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);
            



            client.Close();
            newsock.Close();



            Console.ReadLine();

        }
    }
}
