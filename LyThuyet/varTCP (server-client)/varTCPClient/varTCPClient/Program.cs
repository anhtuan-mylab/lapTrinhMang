using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace varTCPClient
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
            int sent;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);
            }
            catch (Exception se)
            {
                Console.WriteLine("Khong the ket noi den may chu.");
                return;
            }

            data = receiveVarData(server);
            string stringData = Encoding.ASCII.GetString(data);
            Console.WriteLine(stringData);

            // Gắn chuỗi sắn và gửi đi.
            //string messaga1 = "This is a first test";
            //string messaga2 = "This is a first test and a second test.";
            //string messaga3 = "This is a thirt test";

            //sent = sendVarData(server, Encoding.ASCII.GetBytes(messaga1));
            //sent = sendVarData(server, Encoding.ASCII.GetBytes(messaga2));
            //sent = sendVarData(server, Encoding.ASCII.GetBytes(messaga3));


            // Nhập chuỗi từ bàn phím và gửi đi.
            while (true)
            {
                string input = Console.ReadLine();
                sent = sendVarData(server, Encoding.ASCII.GetBytes(input));
            }
            



            server.Close();

            Console.ReadLine();



        }
    }
}
