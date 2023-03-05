using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace streamTCP_Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            string data;
            string input;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Khong the ket noi den may chu.");
                Console.WriteLine(e.ToString());
                return;
            }

            NetworkStream ns = new NetworkStream(server);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            data = sr.ReadLine();
            Console.WriteLine(data);

            data = sr.ReadLine();
            Console.WriteLine(data);

            // Nhập phép toán (a,b,c,d)
            Console.Write("Nhap phép toán (chon a,b,c,d) : ");
            input = Console.ReadLine();
            sw.WriteLine(input);
            sw.Flush();

            // Nhập (gửi) số a và b
            Console.Write("Nhap so a = ");
            input = Console.ReadLine();
            sw.WriteLine(input);
            sw.Flush();

            Console.Write("Nhap so b = ");
            input = Console.ReadLine();
            sw.WriteLine(input);
            sw.Flush();

            //Nhận kết quả và hiển thị
            data = sr.ReadLine();
            Console.WriteLine("Ket qua la : " + data);

            //while (true)
            //{
            //    input = Console.ReadLine();
            //    if (input == "exit")
            //    {
            //        break;
            //    }

            //    sw.WriteLine(input);
            //    sw.Flush();

            //    data = sr.ReadLine();
            //    Console.WriteLine(data);
            //}

            Console.WriteLine("Dong ket noi tu Server.");

            sr.Close();
            sw.Close();
            ns.Close();

            //server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();

        }
    }
}
