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
            var remoteForSend = new IPEndPoint(IPAddress.Parse("127.18.70.34"), 25000);
            SendUp(remoteForSend);
        }
        private static void SendUp(IPEndPoint remoteForSend)
        {
            Socket remoteSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var receiver = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remoteForReceiver = (EndPoint)receiver;
            while (Console.ReadLine() != "q")
            {
                string data = Console.ReadLine();
                if (data == "q") break;
                else
                {
                    //보내기
                    //byte[] bytes = Encoding.UTF8.GetBytes(data);
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
            //remoteSocket.ReceiveFrom(buffer2, ref remoteForReceive);
            //Console.WriteLine($"First rev from{remoteForReceive.ToString()}");
            //Console.WriteLine($"{Encording.UTF8.GetString(buffer2)}");

        }
    }
}
