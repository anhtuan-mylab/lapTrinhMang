using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace theTestUDPServ
{
    class Program
    {
        public static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            newSock.Bind(ipep);
            Console.WriteLine("Dang cho ket noi ...");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)(sender);

            recv = newSock.ReceiveFrom(data, ref tmpRemote);

            Console.WriteLine("Loi nhan nhan tu {0}", tmpRemote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            string welcome = "Chao mung den voi server cua minh (gui tu server).";
            data = Encoding.ASCII.GetBytes(welcome);
            newSock.SendTo(data, data.Length, SocketFlags.None, tmpRemote);

            for(int i = 0; i < 5; i++)
            {
                data = new byte[1024];
                recv = newSock.ReceiveFrom(data, ref tmpRemote);
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            }

            newSock.Close();

            Console.ReadLine();


        }
    }
}
