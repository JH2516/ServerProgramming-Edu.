using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcpClient_fixed_model
{
    internal class Program
    {
        //실제 시뮬하려면 소켓 수신 버퍼를 40 이하로 줄여놓고 실행
        //발송하는 쪽에서 40 미만 1~2개로 100msec 인터벌
        static readonly int FIXED_BUFSIZE = 100;
        static readonly string terminalStr = "\n";
        static readonly string terminalStr2 = "END";
        static readonly byte[] terminalBufferStatic = Encoding.UTF8.GetBytes(terminalStr);
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
            byte[] buffer = new byte[FIXED_BUFSIZE];
            byte[] terminalBuff = Encoding.UTF8.GetBytes(terminalStr);
            byte[] size = new byte[sizeof(int)];
            size = BitConverter.GetBytes(temp.Length);
            // 호스트 2 네트워크 바이트 오버 바이트 순서 변경
            //합배송
            byte[] fullBytes = new byte[temp.Length + size.Length];
            Array.copy(size, fullBytes, size.Length);
            Array.Copy(temp,0,fullBytes,size.Length,temp.Lengths)
            //문자열 녛기
            Array.Copy(temp, buffer, temp.Length);
            Array.Copy(terminalBuff, 0, buffer, temp.Length,terminalBuff);
            //생성후 문자열 넣기
            //배열을 삽입하거나 빼기 위한 과정
            //소스, 도착지, 길이
            Array.Copy(temp, buffer, temp.Length);
            //Array.Copy(terminalStr.ToCharArray(), 0, buffer, temp.Length + terminalStr.Length, terminalStr.Length);//이건 말도 안된다고!
            Array.Copy(terminalBuff, 0, buffer, temp.Length+terminalStr.Length, terminalStr.Length);
            int buffSize = temp.Length+ terminalStr.Length + terminalBuff.Length;
            for (int cnt = buff.Length; cnt < buffer.Length; cnt++)
            {
                buffer[cnt] = (Byte)'#';
            }
            sock.Connect(remoteEP);
            //보내기
            //분리 전송
            sock.Send(size);
            sock.Send(temp);
            //합전송
            sock.Send(fullbytes);
            //sock.Send(buffer);
            //받기
            int retval = sock.Receive(buffer,0,FIXED_BUFSIZE, SocketFlags.None);
            while(retval < 40)
            {
                retval += sock.Receive(buffer, 0, FIXED_BUFSIZE, SocketFlags.None);
                //메세지를 의미있는 단위로 재합성해서 큐 같은 것들에 저장한다
            }
            sock.Close();
        }
    }
}
