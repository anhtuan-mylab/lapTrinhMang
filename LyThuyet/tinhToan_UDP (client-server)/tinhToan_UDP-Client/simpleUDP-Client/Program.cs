using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace simpleUDP_Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            string welcome = "xin chao, ban co o do khong ?";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)sender;

            data = new byte[1024];
            int recv = server.ReceiveFrom(data, ref remote);

            Console.WriteLine("Tin nhan duoc nhan tu {0} :", remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            //while (true)
            //{
            //    input = Console.ReadLine();
            //    if (input == "exit")
            //    {
            //        break;
            //    }

            //    server.SendTo(Encoding.ASCII.GetBytes(input), remote);
            //    data = new byte[1024];
            //    recv = server.ReceiveFrom(data, ref remote);
            //    stringData = Encoding.ASCII.GetString(data, 0, recv);
            //    Console.WriteLine(stringData);
            //}

            // Nhận chương trình 

            recv = server.ReceiveFrom(data, ref remote);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);

            // Chọn phép toán :
            Console.Write("Chon phep toan: ");
            input = Console.ReadLine();
            server.SendTo(Encoding.ASCII.GetBytes(input), remote);


            // Gửi số a và b
            Console.Write("Nhap so a : ");
            input = Console.ReadLine();
            server.SendTo(Encoding.ASCII.GetBytes(input), remote);

            Console.Write("Nhap so b : ");
            input = Console.ReadLine();
            server.SendTo(Encoding.ASCII.GetBytes(input), remote);

            // Nhận kết quả:
            int ketQua;
            ketQua = server.ReceiveFrom(data, ref remote);
            string stringData_ketQua = Encoding.ASCII.GetString(data, 0, ketQua);
            Console.WriteLine("Ket qua la: " + stringData_ketQua);


            Console.WriteLine("Dung ket noi ...");
            server.Close();

            Console.ReadLine();

        }
    }
}
