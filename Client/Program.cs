﻿

using Client;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;


const string IP = "127.0.0.1";
const int Port1 = 8000;
const int Port2 = 8080;


try
{
    Stopwatch timer = new Stopwatch();
    MyClient client = new MyClient(IPAddress.Parse(IP),Port1);
    //1 сервер
    timer.Start();
    client.Connecting(Port1);
    client.SendMess("СOW");
    client.SendMess("Mouse");
    client.ConnectClose();
    //2 сервер
    client = new MyClient(IPAddress.Parse(IP), Port2);
    client.Connecting(Port2);
    client.SendMess("Fox");
    client.SendMess("Snake");
    client.ConnectClose();
    timer.Stop();

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