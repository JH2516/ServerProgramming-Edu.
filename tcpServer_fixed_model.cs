using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcpClient_fixed_model
{
    internal class Program
    {
        //실제 시뮬하려면 소켓 수신 버퍼를 40 이하로 줄여놓고 실행
        //발송하는 쪽에서 40 미만 1~2개로 100msec 인터벌
        //프로토콜 사이즈가 동일하다
        static readonly int FIXED_BUFSIZE = 100;
        static void Main(string[] args)
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 25000);
            StartServerWithFixedType(localEP);
        }
        private static void StartServerWithFixedType(IPEndPoint localEP)
        {
            //소켓
            Socket listensock = new Socket(AddressFamily.InterNetwork, SocketType,Stream, Program.FIXED_BUFSIZE);
            //바인드,listen
            listensock.Bind(localEP);
            listensock.Listen(10);
            //accept
            while (listensock.Connected)
            {
                //accept
                Socket clientSock = listensock.Accept();
                Console.WriteLine("[info] -- waiting Connection with New client");
                IPEndPoint clientEP = (IPEndPoint)clientSock.RemoteEndPoint;
                Console.WriteLine($"[info] -- connection With [{clientEP.Address}]:[{clientEP.ToString()}]");
                byte[] buffer = new byte [1500];

                //send,recv
                Console.WriteLine("[info] -- RECV waiting");
                byte[] size = new byte[sizeof(int)];
                int retSizeval = clientSock.Receive(size,0,size.Length,SocketFlags.None);
                Console.Writeline($"[RECV] --> recvBytes : retSizeValue [{retsizeval}]");
                int realDataSize = BitConverter.ToInt32(size);
                byte[] buffer = new byte[realDataSize];
                int retval = clientEP.Receive(buffer, 0,buffer.Length,SocketFlags.None);
                Console.WriteLine($"[RECV] --> recvBytes[{retval}]");
                Console.WriteLine($"[RECV Raw Data] --> [{Encoding.UTF8.GetString(buffer)}]");

                //send,rev 2
                byte[] size = new byte[sizeof(int)];
                int retSizeval = clientSock.Receive(size, 0, size.Length, SocketFlags.None);
                Console.Writeline($"[RECV2] --> recvBytes : retSizeValue [{retsizeval}]");
                int realDataSize = BitConverter.ToInt32(size);
                byte[] buffer = new byte[realDataSize];
                int retval = clientEP.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                Console.WriteLine($"[RECV2] --> recvBytes[{retval}]");
                Console.WriteLine($"[RECV2 Raw Data] --> [{Encoding.UTF8.GetString(buffer)}]");
                //구문 분석 및 처리
                //찾기
                string recvRawData = Encoding.UTF8.GetString(buffer);
                if(recvRawData.Contains(terminalStr);)
                {
                    int idx = recvRawData.IndexOf(terminalStr);
                    //if(recvRawData.Length == idx)
                    //{
                        byte[] data = new byte[recvRawData.Length - idx];
                        Array.Copy(buffer, 0, data, 0, idx-1);
                        Console.WriteLine($"[RECV Data] --> [{Encoding.UTF8.GetString(data)}]");
                    //}
                }
                //응답 전송 및 처리
                //연결 끊음
                Console.WriteLine("[info] -- client closed");
                clientSock.Close();
            }
            //서버 죽음
            listensock.Close();
        }
    }
}
