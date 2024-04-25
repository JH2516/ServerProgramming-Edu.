using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcpClient_fixed_model
{
    internal class Program
    {
        static readonly int FIXED_BUFSIZE = 40;
        static void Main(string[] args)
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.LoopBack, 25000);
            SendFixedData(remoteEP);
        }

        private static void SendFixedData(IPEndPoint remoteEP)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(remoteEP);

            string commandString = "GET Weather/Temp\r\n";
            byte[] temp = Encoding.UTF8.GetBytes(commandString);
            byte[] buffer = new byte[40];
            //생성후 문자열 넣기
            Array.Copy(temp, buffer, temp.Length);
            for (int cnt = temp.Length; cnt < buffer.Length; cnt++)
            {
                buffer[cnt] = (Byte)'#';
            }
            sock.Connect(remoteEP);
            sock.Send(buffer);
            sock.Close();
        }
    }
}
