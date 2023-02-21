using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace varTCPServer
{
    class Program
    {
        public static int sendVarData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataLeft = size;
            int sent;

            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = s.Send(datasize);

            while (total < size)
            {
                sent = s.Send(data, total, dataLeft, SocketFlags.None);
                total += sent;
                dataLeft -= sent;
            }

            return total;
        }


        public static byte[] receiveVarData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];

            recv = s.Receive(datasize, 0);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataLeft = size;
            byte[] data = new byte[size];

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
            Console.WriteLine("Ket noi thanh cong.");

            string welcome = "Welome to my Server.";
            data = Encoding.ASCII.GetBytes(welcome);
            int sent = sendVarData(client, data);

            // Nhận được 5 tin nhắn và hiển thị.
            //for(int i = 0; i < 5; i++)
            //{

            //    data = receiveVarData(client);
            //    Console.WriteLine(Encoding.ASCII.GetString(data));
            //}


            // Nhận tới đâu thì hiển thị tin nhắn tới đó.
            while (true)
            {
                data = receiveVarData(client);
                Console.WriteLine(Encoding.ASCII.GetString(data));
            }


            client.Close();
            newsock.Close();

            Console.ReadLine();




        }
    }
}
