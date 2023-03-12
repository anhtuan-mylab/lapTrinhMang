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

            //while(true)
            //{
            //    data = new byte[1024];
            //    recv = newSock.ReceiveFrom(data, ref remote);

            //    Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            //    newSock.SendTo(data, recv, SocketFlags.None, remote);
            //}

            // Gửi chương trình 
            string chuong_trinh = "Chon phep toan: a.PhepCong    b.PhepTru    c.PhepNhan    d.PhepChia";
            data = Encoding.ASCII.GetBytes(chuong_trinh);
            newSock.SendTo(data, data.Length, SocketFlags.None, remote);

            // Nhận phép toán :
            int phep_chon;
            phep_chon = newSock.ReceiveFrom(data, ref remote);
            string stringData_phepToan = Encoding.ASCII.GetString(data, 0, phep_chon);
            Console.WriteLine(stringData_phepToan);



            // Nhận số a và b
            int so_a, so_b;

            so_a = newSock.ReceiveFrom(data, ref remote);
            string stringData_a = Encoding.ASCII.GetString(data, 0, so_a);
            Console.WriteLine(stringData_a);

            so_b = newSock.ReceiveFrom(data, ref remote);
            string stringData_b = Encoding.ASCII.GetString(data, 0, so_b);
            Console.WriteLine(stringData_b);


            // Thực hiện phép toán

            switch(stringData_phepToan)
            {
                case "a":
                    int tong = Convert.ToInt16(stringData_a) + Convert.ToInt16(stringData_b);
                    Console.WriteLine(tong);
                    data = Encoding.ASCII.GetBytes(Convert.ToString(tong));
                    newSock.SendTo(data, data.Length, SocketFlags.None, remote);



                    break;

                case "b":
                    int hieu = Convert.ToInt16(stringData_a) - Convert.ToInt16(stringData_b);
                    Console.WriteLine(hieu);
                    data = Encoding.ASCII.GetBytes(Convert.ToString(hieu));
                    newSock.SendTo(data, data.Length, SocketFlags.None, remote);
                    break;

                case "c":
                    int nhan = Convert.ToInt16(stringData_a) * Convert.ToInt16(stringData_b);
                    Console.WriteLine(nhan);
                    data = Encoding.ASCII.GetBytes(Convert.ToString(nhan));
                    newSock.SendTo(data, data.Length, SocketFlags.None, remote);
                    break;

                case "d":
                    int chia = Convert.ToInt16(stringData_a) / Convert.ToInt16(stringData_b);
                    Console.WriteLine(chia);
                    data = Encoding.ASCII.GetBytes(Convert.ToString(chia));
                    newSock.SendTo(data, data.Length, SocketFlags.None, remote);
                    break;
            }


            newSock.Close();

            Console.ReadLine();

        }
    }
}
