using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace tinhToan_Server
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

        public static void phepCong(Socket s, byte[] data)
        {
            sentVarData(s, Encoding.ASCII.GetBytes("Nhap so a = "));

            data = receiveVarData(s);
            string so_a_chon = Encoding.ASCII.GetString(data);
            Console.Write("So a la : " + so_a_chon + "\n");

            sentVarData(s, Encoding.ASCII.GetBytes("Nhap so b = "));

            data = receiveVarData(s);
            string so_b_chon = Encoding.ASCII.GetString(data);
            Console.Write("So b la : " + so_b_chon + "\n");

            int ketQua = Convert.ToInt16(so_a_chon) + Convert.ToInt16(so_b_chon);

            sentVarData(s, Encoding.ASCII.GetBytes(Convert.ToString(ketQua)));           
        }

        public static void phepTru(Socket s, byte[] data)
        {
            sentVarData(s, Encoding.ASCII.GetBytes("Nhap so a = "));

            data = receiveVarData(s);
            string so_a_chon = Encoding.ASCII.GetString(data);
            Console.Write("So a la : " + so_a_chon + "\n");

            sentVarData(s, Encoding.ASCII.GetBytes("Nhap so b = "));

            data = receiveVarData(s);
            string so_b_chon = Encoding.ASCII.GetString(data);
            Console.Write("So b la : " + so_b_chon + "\n");

            int ketQua = Convert.ToInt16(so_a_chon) - Convert.ToInt16(so_b_chon);

            sentVarData(s, Encoding.ASCII.GetBytes(Convert.ToString(ketQua)));
        }

        public static void phepNhan(Socket s, byte[] data)
        {
            sentVarData(s, Encoding.ASCII.GetBytes("Nhap so a = "));

            data = receiveVarData(s);
            string so_a_chon = Encoding.ASCII.GetString(data);
            Console.Write("So a la : " + so_a_chon + "\n");

            sentVarData(s, Encoding.ASCII.GetBytes("Nhap so b = "));

            data = receiveVarData(s);
            string so_b_chon = Encoding.ASCII.GetString(data);
            Console.Write("So b la : " + so_b_chon + "\n");

            int ketQua = Convert.ToInt16(so_a_chon) * Convert.ToInt16(so_b_chon);

            sentVarData(s, Encoding.ASCII.GetBytes(Convert.ToString(ketQua)));
        }

        public static void phepChia(Socket s, byte[] data)
        {
            sentVarData(s, Encoding.ASCII.GetBytes("Nhap so a = "));

            data = receiveVarData(s);
            string so_a_chon = Encoding.ASCII.GetString(data);
            Console.Write("So a la : " + so_a_chon + "\n");

            sentVarData(s, Encoding.ASCII.GetBytes("Nhap so b = "));

            data = receiveVarData(s);
            string so_b_chon = Encoding.ASCII.GetString(data);
            Console.Write("So b la : " + so_b_chon + "\n");

            int ketQua = Convert.ToInt16(so_a_chon) / Convert.ToInt16(so_b_chon);

            sentVarData(s, Encoding.ASCII.GetBytes(Convert.ToString(ketQua)));
        }

        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];

            // Quá trình kết nối , server trong trạng thái chờ.
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9056);

            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Dang cho ket noi ...");

            Socket client = newSock.Accept();
            Console.WriteLine("Ket noi thanh cong.");

            // Quá trình trao đổi dữ liệu.

            sentVarData(client, Encoding.ASCII.GetBytes("Hello Client (server->client)"));

            // gửi menu chọn phương pháp.
            sentVarData(client, Encoding.ASCII.GetBytes("Chon phuong thuc thuc hien phep toan : "));
            sentVarData(client, Encoding.ASCII.GetBytes("a. Phep cong."));
            sentVarData(client, Encoding.ASCII.GetBytes("b. Phep tru."));
            sentVarData(client, Encoding.ASCII.GetBytes("c. Phep nhan."));
            sentVarData(client, Encoding.ASCII.GetBytes("d. Phep chia."));
            
            // thực hiện tính toán 
            data = receiveVarData(client);
            string chonPhepToan = Encoding.ASCII.GetString(data);


            switch(chonPhepToan)
            {
                case "a":
                    Console.WriteLine("Chon phep toan cong.");
                    phepCong(client, data);
                    break;


                case "b":
                    Console.WriteLine("Chon phep toan tru.");
                    phepTru(client, data);
                    break;

                case "c":
                    Console.WriteLine("Chon phep toan nhan.");
                    phepNhan(client, data);
                    break;

                case "d":
                    Console.WriteLine("Chon phep toan chia.");
                    phepChia(client, data);
                    break;

                default:
                    break;
            }


            client.Close();
            newSock.Close();

            Console.ReadLine();

        }
    }
}
