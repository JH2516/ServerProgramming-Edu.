using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace console_tcp_00
{
    internal class Program
    {
        //private socket = Socket.socket;
        static void Main(string[] args)
        {
            var hostEntry = Dns.GetHostEntry("www.google.com");
            var remoteEP = new IPEndPoint(hostEntry.AddressList[0], 80);
            sendTcpWsg(remoteEP, "GET/HTTP/1.1\r\n\r\n");
            var remoteIP = new IPEndPoint(IPAddress.Parse("172.18.27.34"), 25000);
            sendTcpWsg(remoteIP, "helloFirst app\r\n");
        }
        private static void sendTcpWsg(IPEndPoint remoteEP, string v)
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                Console.WriteLine($"[info] -- connection to Server" + 
                    $"{remoteEP.Address}{remoteEP.Port}");
                byte[] buffer = Encoding.UTF8.GetBytes(v);
                int size = buffer.Length;

                //데이터 송신
                Console.WriteLine(v);
                int retval = socket.Send(buffer, 0,size,SocketFlags.None);
                Console.WriteLine("[info] -- send finish -- !");


                //데이터 수신
                buffer = new byte[1500];
                retval = socket.Receive(buffer,0,buffer.Length, SocketFlags.None);
                Console.WriteLine($"[info] -- recvbytes[{retval}]");
                Console.WriteLine($"[data] ==> [{buffer.ToString()}]");
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == 0x0d || buffer[i] == 0x0a)
                    {
                        buffer[i] = 0x21;
                    }
                }
                Console.WriteLine($"[data] ==> [{Encoding.UTF8.GetString(buffer)}]");
                Console.WriteLine("The End");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }

        }

    }
}
