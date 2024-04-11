using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace ConsoleApp1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var remoteAddress = IPAddress.Parse("172.18.27.34"); //이 구조를 잘 기억할 것
            var remoteAddress2 = new IPEndPoint(remoteAddress,25000);//추후 변환되어 헥사값으로 변환된다
            var remoteForSend = new IPEndPoint(IPAddress.Parse("172.18.27.34"), 26000);//받는 포트번호
            SendUp(remoteForSend);
            //string MULTICASTING "204.0.0.10";
        }
        private static void SendUp(IPEndPoint remoteForSend)
        {
            Socket remoteSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var receiver = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            remoteSocket.Bind(new IPEndPoint(IPAddress.Any, 25000));//보내는 포트 번호
            EndPoint remoteForReceiver = (EndPoint)receiver;
            while (true)
            {
                string data = Console.ReadLine();
                if (data == "send") break;
                else
                {
                    //보내기
                    //byte[] buffer = Encoding.UTF8.GetBytes(data);
                    //remoteSocket.SendTo(buffer, buffer.Length, SocketFlags.None, remoteForSend);//보낼곳을 바로 기입

                    //받기
                    byte[] buffer2 = new byte[1024];
                    int recvbyte = remoteSocket.ReceiveFrom(buffer2, ref remoteForReceiver);

                    if (recvbyte >= 0) break;
                    remoteSocket.ReceiveFrom(buffer2, ref remoteForReceiver);

                }
            }
            string firstMessage = "Hello World";

            byte[] buffer = Encoding.UTF8.GetBytes(firstMessage);
            //send는 tcp용
            remoteSocket.SendTo(buffer, buffer.Length, SocketFlags.None, remoteForSend);//보낼곳을 바로 기입


            //byte[] buffer2 = new byte[1024];
            //remoteSocket.ReceiveFrom(buffer2, ref remoteForReceiver);
            //Console.WriteLine($"First rev from{remoteForReceiver.ToString()}");
            //Console.WriteLine($"{Encoding.UTF8.GetString(buffer2)}");

        }
    }
}
