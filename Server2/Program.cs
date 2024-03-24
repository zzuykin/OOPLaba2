
using System.Net;
using System.Net.Sockets;
using System.Text;


const string IP = "127.0.0.1";
const int Port = 8080;
var EndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);

var TcpSoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    TcpSoket.Bind(EndPoint);
    TcpSoket.Listen(5);

    while (true)
    {
        Console.WriteLine($"Ожидвем сооедениение порта {EndPoint}");

        var data = new StringBuilder();
        var listener = await TcpSoket.AcceptAsync(); // новый сокет для подключения конкретного клиента
        var buffer = new byte[1024];
        var size = await listener.ReceiveAsync(buffer);
        data.Append(Encoding.UTF8.GetString(buffer, 0, size));

        Console.WriteLine("Сервер получил сообщение!");
        Console.WriteLine("Изменяю сообщение :)");

        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{10 * i}% ");
            data.Append("?");
            await Task.Delay(50);
        }
        Console.WriteLine("Успешно!");
        Console.WriteLine(data.ToString());
        await listener.SendAsync(Encoding.UTF8.GetBytes("Ответ с сервера 2:\n" + data.ToString()));
        listener.Shutdown(SocketShutdown.Both);
        listener.Close();
    }

}
catch (Exception exception)
{
    Console.WriteLine(exception.ToString());
}
finally
{
    Console.ReadLine();
}