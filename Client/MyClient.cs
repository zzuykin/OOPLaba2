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

        public MyClient(IPAddress ip,int Port)
        {
            Ip = ip;
            EndPoint = new IPEndPoint(ip, Port);
            TcpSocket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connecting(int port)
        {
            EndPoint = new IPEndPoint(Ip, port);
            TcpSocket = new Socket(Ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                TcpSocket.Connect(EndPoint);
                Console.WriteLine("Succes conect");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        async public Task ConnectingAsync(int port)
        {
            EndPoint = new IPEndPoint(Ip, port);
            TcpSocket = new Socket(Ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                await TcpSocket.ConnectAsync(EndPoint);
                Console.WriteLine("Succes conect");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendMess(string mess)
        {
            try
            {
                Console.WriteLine("Succses sent message");
                byte[] data = Encoding.UTF8.GetBytes(mess);
                TcpSocket.Send(data);
                var buffer = new byte[1024];
                var answer = new StringBuilder();
                var size = TcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                Console.WriteLine("Получен ответ с сервера:");
                Console.WriteLine(answer.ToString());
                //return answer.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        async public Task SendMessAsync(string mess)
        {
            try
            {
                Console.WriteLine("Succses sent message");
                byte[] data = Encoding.UTF8.GetBytes(mess);
                await TcpSocket.SendAsync(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void ConnectClose()
        {
            TcpSocket.Dispose();
        }
    }
}