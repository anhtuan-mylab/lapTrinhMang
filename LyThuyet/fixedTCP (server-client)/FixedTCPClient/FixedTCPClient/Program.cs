using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace FixedTCPClient
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
            int sent;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Connect(ipep);

            int recv = server.Receive(data);

            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);

            sent = sentData(server, Encoding.ASCII.GetBytes("message 1"));
            sent = sentData(server, Encoding.ASCII.GetBytes("message 2"));
            sent = sentData(server, Encoding.ASCII.GetBytes("message 3"));
            sent = sentData(server, Encoding.ASCII.GetBytes("message 4"));
            sent = sentData(server, Encoding.ASCII.GetBytes("message 5"));

            server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();

            

        }
    }
}
