using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace streamTCP_Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            string data;
            string so_a;
            string so_b;
            string phep_toan_chon;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Dang cho ket noi ...");

            Socket client = newSock.Accept();

            IPEndPoint newClient = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Ket noi voi {0} tai cong {1}", newClient.Address, newClient.Port);

            NetworkStream ns = new NetworkStream(client);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            string welcome = "Chao mung den voi Server (gui tu Server)";
            sw.WriteLine(welcome);
            sw.Flush();

            string nhap_chon = "Nhap chon phep toan: a.PhepCong    b.PhepTru    c.PhepNhan    d.PhepChia";
            sw.WriteLine(nhap_chon);
            sw.Flush();
            
            // Nhận phép toán chọn.
            phep_toan_chon = sr.ReadLine();


            // Nhận số a và b.
            so_a = sr.ReadLine();
            Console.WriteLine(so_a);

            so_b = sr.ReadLine();
            Console.WriteLine(so_b);

            // Thực hiện tính toán.
            switch (phep_toan_chon)
            {
                case "a":
                    int a = Convert.ToInt16(so_a);
                    int b = Convert.ToInt16(so_b);
                    int tong = a + b;
                    
                    sw.WriteLine(Convert.ToString(tong));
                    sw.Flush();
                    break;

                case "b":
                    int a_b = Convert.ToInt16(so_a);
                    int b_b = Convert.ToInt16(so_b);
                    int hieu = a_b - b_b;

                    sw.WriteLine(Convert.ToString(hieu));
                    sw.Flush();
                    break;

                case "c":
                    int c_a = Convert.ToInt16(so_a);
                    int c_b= Convert.ToInt16(so_b);
                    int nhan = c_a * c_b;

                    sw.WriteLine(Convert.ToString(nhan));
                    sw.Flush();
                    break;

                case "d":
                    int d_a = Convert.ToInt16(so_a);
                    int d_b = Convert.ToInt16(so_b);
                    int chia = d_a + d_b;

                    sw.WriteLine(Convert.ToString(chia));
                    sw.Flush();
                    break;
            }
            

            //while (true)
            //{
            //    try
            //    {
            //        data = sr.ReadLine();

            //    }
            //    catch (IOException)
            //    {
            //        break;
            //    }

            //    Console.WriteLine(data);
            //    sw.WriteLine(data);
            //    sw.Flush();
            //}

            Console.WriteLine("Dung ket noi tu {0}", newClient.Address);

            sw.Close();
            sr.Close();
            ns.Close();

            newSock.Close();

            Console.ReadLine();



        }
    }
}
