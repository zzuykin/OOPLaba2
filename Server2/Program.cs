
using System.Net;
using System.Net.Sockets;
using System.Text;


const string IP = "127.0.0.1";
const int Port = 8080;
var EndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);

var TcpSoket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
try
{
    TcpSoket.Bind(EndPoint);
    Console.WriteLine("Сервер 2 включён");
    TcpSoket.Listen();
    Console.WriteLine($"Ожидаем сооедениение порта {EndPoint}");
    while (true)
    {
        var listener = await TcpSoket.AcceptAsync();
        var data = new StringBuilder();
        var buffer = new byte[1024];
        int size;
        do
        {
            size = await listener.ReceiveAsync(buffer);
            data.Append(Encoding.UTF8.GetString(buffer, 0, size));
        }
        while (size > 0);
        Console.WriteLine("Сервер получил сообщение!");
        Console.WriteLine("Изменяю сообщение :)");
        for (int i = 0; i <= 10; i++)
        {
            Console.Write($"{10 * i}% ");
            await Task.Delay(50);
        }
        data.Append(" - Колличество слов в тексте: " + Convert.ToString(data.Length));
        Console.WriteLine("Успешно!");
        Console.WriteLine(data.ToString());
        listener.Send(Encoding.UTF8.GetBytes("Ответ с сервера 2:\n" + data.ToString()));
        listener.Shutdown(SocketShutdown.Both);
        listener.Close();
    }
    TcpSoket.Dispose();
    Console.WriteLine("Socket closed");

}
catch (Exception exception)
{
    Console.WriteLine(exception.ToString());
}
finally
{
    Console.ReadLine();
}