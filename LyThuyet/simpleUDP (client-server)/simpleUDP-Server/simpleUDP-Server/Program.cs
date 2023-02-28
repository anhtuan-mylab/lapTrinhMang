using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace simpleUDP_Server
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
            EndPoint remote = (EndPoint)(sender);

            recv = newSock.ReceiveFrom(data, ref remote);

            Console.WriteLine("Loi nhan nhan tu {0} :", remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            string welcome = "Chao mung den voi Server.";
            data = Encoding.ASCII.GetBytes(welcome);
            newSock.SendTo(data, data.Length, SocketFlags.None, remote);

            while(true)
            {
                data = new byte[1024];
                recv = newSock.ReceiveFrom(data, ref remote);

                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                newSock.SendTo(data, recv, SocketFlags.None, remote);
            }


        }
    }
}
