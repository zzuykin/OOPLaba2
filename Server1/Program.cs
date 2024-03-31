using System.Net.Sockets;
using System.Net;
using System.Text;

const string IP = "127.0.0.1";
const int Port = 8000;
var EndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);

var TcpSoket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
try
{
    TcpSoket.Bind(EndPoint);
    Console.WriteLine("Сервер 1 включён");
    TcpSoket.Listen();
    Console.WriteLine($"Ожидаем соединение порта {EndPoint}");
    while (true)
    {
        var listener = await TcpSoket.AcceptAsync();
        Console.WriteLine("Клиент подключен");

        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = await listener.ReceiveAsync(buffer, SocketFlags.None);

            if (bytesRead > 0)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Сервер получил сообщение: {receivedMessage}");

                // Обработка полученного сообщения
                for (int i = 0; i <= 10; i++)
                {
                    Console.Write($"{10 * i}% ");
                    await Task.Delay(50);
                }
                var processedMessage = receivedMessage + " - " + Convert.ToString(receivedMessage.Length);
                Console.WriteLine(processedMessage);

                // Отправка ответа клиенту
                byte[] responseBytes = Encoding.UTF8.GetBytes(processedMessage);
                await listener.SendAsync(responseBytes, SocketFlags.None);
            }
            else
            {
                // Клиент закрыл соединение, выходим из внутреннего цикла
                Console.WriteLine("Клиент закрыл соединение");
                break;
            }
        }

        // Закрываем соединение с клиентом
        listener.Shutdown(SocketShutdown.Both);
        listener.Close();
        Console.WriteLine("Соединение с клиентом закрыто");
    }
}
catch (Exception exception)
{
    Console.WriteLine(exception.ToString());
}
finally
{
    TcpSoket.Dispose();
    Console.WriteLine("Socket closed");
}