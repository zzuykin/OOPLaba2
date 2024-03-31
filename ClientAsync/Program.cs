using Client;
using System.Diagnostics;
using System.Net;

const string IP = "127.0.0.1";
const int Port1 = 8000;
const int Port2 = 8080;


try
{
    //Console.WriteLine("Введите сообщние для запроса:");
    //var mess = Console.ReadLine();
    Stopwatch timer = new Stopwatch();
    MyClient client = new MyClient(IPAddress.Parse(IP),Port2);

    //// 1 сервер
    timer.Start();
    await client.ConnectingAsync(Port1);
    await client.SendMessAsync("СOW");
    await client.SendMessAsync("Mouse");
    client.ConnectClose();
    

    // 2 сервер
    await client.ConnectingAsync(Port2);
    await client.SendMessAsync("DOG");
    await client.SendMessAsync("CAT");
    //var f = client.GetAnswer();
    //Console.WriteLine(f);
    client.ConnectClose();

    await client.ConnectingAsync(Port2);
    await client.SendMessAsync("DOG");
    client.ConnectClose();

    Console.WriteLine($"Время выполнения обработки запросов: {timer.Elapsed.TotalSeconds} секунд");
    Console.ReadLine();
}
catch (Exception exception)
{
    Console.WriteLine(exception.ToString());
}
finally
{
    Console.WriteLine();
}