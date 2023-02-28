using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace tinhToan_Client
{
    class Program
    {
        public static int sentVarData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataLeft = size;
            int sent;

            byte[] dataSize = new byte[4];
            dataSize = BitConverter.GetBytes(size);
            sent = s.Send(dataSize);

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
            byte[] dataSize = new byte[4];

            recv = s.Receive(dataSize, 0);
            int size = BitConverter.ToInt32(dataSize, 0);
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

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9056);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            int flag = 0;

            while (flag==0)
            {
                try
                {
                    server.Connect(ipep);
                    flag = 1;
                    Console.Clear();
                }
                catch (Exception se)
                {
                    Console.WriteLine("Khong the ket noi den may chu.");
                }
            }

            

            // Nhận câu Hello client
            data = receiveVarData(server);
            string stringData = Encoding.ASCII.GetString(data);
            Console.WriteLine(stringData);


            // Nhận câu hỏi chọn phương thức 
            for (int i = 0; i < 5; i++)
            {
                data = receiveVarData(server);
                string chonPhuongThuc = Encoding.ASCII.GetString(data);
                Console.WriteLine(chonPhuongThuc);
            }




            bool stop = false;
            while (!stop)
            {
                Console.Write("Chon phuong thuc: ");
                string nhapPT = Console.ReadLine();

                if (nhapPT=="a" || nhapPT == "b" || nhapPT == "c" || nhapPT == "d")
                {
                    Console.WriteLine("Da chon phuong thuc : " + nhapPT);
                    sentVarData(server, Encoding.ASCII.GetBytes(nhapPT));

                    stop = true;
                }
                else
                {
                    Console.WriteLine("Phuong thuc khong ton tai.");
                }
            }



            






            // Nhập số và hiển thị kết quả

            data = receiveVarData(server);
            string so_a = Encoding.ASCII.GetString(data);
            Console.Write(so_a);

            sentVarData(server, Encoding.ASCII.GetBytes(Console.ReadLine()));

            data = receiveVarData(server);
            string so_b = Encoding.ASCII.GetString(data);
            Console.Write(so_b);

            sentVarData(server, Encoding.ASCII.GetBytes(Console.ReadLine()));

            

            data = receiveVarData(server);
            string dapAn = Encoding.ASCII.GetString(data);
            Console.Write("Dap an la: " + dapAn);










            server.Close();

            Console.ReadLine();


        }
    }
}
