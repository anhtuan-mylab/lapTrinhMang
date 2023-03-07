using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace theTestUDPClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            string welcome = "Xin chao, ban co o do khong ? (gui tu client).";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)sender;

            data = new byte[1024];
            int recv = server.ReceiveFrom(data, ref tmpRemote);

            Console.WriteLine("Loi nhan tu {0}", tmpRemote.ToString());

            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            server.SendTo(Encoding.ASCII.GetBytes("Loi nhan 01."), tmpRemote);
            server.SendTo(Encoding.ASCII.GetBytes("Loi nhan 02."), tmpRemote);
            server.SendTo(Encoding.ASCII.GetBytes("Loi nhan 03."), tmpRemote);
            server.SendTo(Encoding.ASCII.GetBytes("Loi nhan 04."), tmpRemote);
            server.SendTo(Encoding.ASCII.GetBytes("Loi nhan 05."), tmpRemote);

            Console.WriteLine("Dung ket noi ...");
            server.Close();


            Console.ReadLine();
        }
    }
}
