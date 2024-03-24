using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MyClient
    {
        public IPAddress Ip { get; set; }

        private IPEndPoint EndPoint { get; set; }

        private Socket TcpSocket { get; set; }

        public MyClient(IPAddress ip)
        {
            Ip = ip;
        }

        public void Connecting(int port)
        {
            EndPoint = new IPEndPoint(Ip, port);
            TcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TcpSocket.Connect(EndPoint);
        }

        async public Task ConnectingAsync(int port)
        {
            EndPoint = new IPEndPoint(Ip, port);
            TcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await TcpSocket.ConnectAsync(EndPoint);
        }

        public string SendMess(string mess)
        {
            var data = Encoding.UTF8.GetBytes(mess);
            var buffer = new byte[1024];
            TcpSocket.Send(data);
            var answer = new StringBuilder();
            var size = TcpSocket.Receive(buffer);
            answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            Console.WriteLine(answer.ToString());
            return answer.ToString();
        }

        async public Task<string> SendMessAsync(string mess)
        {
            var data = Encoding.UTF8.GetBytes(mess);
            var buffer = new byte[1024];
            await TcpSocket.SendAsync(data);
            var answer = new StringBuilder();
            var size = TcpSocket.Receive(buffer);
            answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            Console.WriteLine(answer.ToString());
            return answer.ToString();
        }

        public void ConnectClose()
        {
            TcpSocket.Shutdown(SocketShutdown.Both);
            TcpSocket.Close();
        }
    }
}
