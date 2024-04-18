using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace ConsoleApp2
{

    internal class Class1
    {
        private static Socket sock;
        static void main(string[] args)
        {
            var localEPIP = new IPEndPoint(IPAddress.Parse("172.18.27.34"), 25000);
            var localEPLoopback = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25000);
            localEPLoopback = new IPEndPoint(IPAddress.Loopback, 25000);
            var localEPAll = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 25000);
            localEPAll = new IPEndPoint(IPAddress.Any, 25000);

            StartServer(localEPAll);
        }
        private static void StartServer(IPEndPoint localEPAll)
        {
            try
            {
                sock = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                //EndPoint is abstract class
                
                sock.Bind(localEPAll);
                Console.WriteLine($"[info] -- Server Bind to [{sock.LocalEndPoint}]");
                Console.WriteLine($"[info] -- Server Bind to [{localEPAll.Address}]:[{localEPAll.Port}]");

                Console.WriteLine("[info] -- Server Listenning with limit 10");
                sock.Listen(10);
                Console.WriteLine("[info] -- Server Listen started");

                //접속자가 나타날때까지 동기식 대기상태로 대기
                Console.WriteLine("[info] -- Server blocking in Accept()");
                var clientSock = sock.Accept();
                Console.WriteLine("[info] -- Client connected");
                IPEndPoint clientEp = (IPEndPoint)clientSock.RemoteEndPoint;
                Console.WriteLine($"[Client] -- from [{clientEp.Address}]:[{clientEp.Port}]");

                //데이터 송수신
                Console.WriteLine("[info] -- Transfer data from client to server");
                //Thread.Sleep(1000);
                Console.ReadLine();
                //접속 종료(아무나)
                clientSock.Close();
                Console.WriteLine("[info] -- Client Closed by Server");
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
            }
            sock.Close();
        }
    }
}
