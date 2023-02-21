using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace FixedTCPServer
{
    class Program
    {

        public static int sentData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataLeft = size;
            int sent;

            while (total < size)
            {
                sent = s.Send(data, total, dataLeft, SocketFlags.None);
                total += sent;
                dataLeft -= sent;
            }

            return total;
        }

        public static byte[] receiveData(Socket s, int size)
        {
            int total = 0;
            int dataLeft = size;
            byte[] data = new byte[size];
            int recv;

            while (total < size)
            {
                recv = s.Receive(data, total, dataLeft, 0);
                if (recv == 0)
                {
                    data = Encoding.ASCII.GetBytes("exit");
                    break;
                }

                total += recv;
                dataLeft -= recv;
            }

            return data;
        }


        public static void Main(string[] args)
        {

            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("Dang cho ket noi ...");

            Socket client = newsock.Accept();

            string welcome = "Welcome to my test server";
            data = Encoding.ASCII.GetBytes(welcome);

            int sent = sentData(client, data);

            for(int i=0; i<5; i++)
            {
                data = receiveData(client, 9);
                Console.WriteLine(Encoding.ASCII.GetString(data));
            }

            client.Close();
            newsock.Close();

            Console.ReadLine();



        }
    }
}
