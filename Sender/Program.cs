﻿using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

string message = "Hello World!";

for (int i = 0; i < 100; i++)
{
    Thread.Sleep(2000);

    var body = Encoding.UTF8.GetBytes(message + " - " + i);

    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

    Console.WriteLine(" [x] Sent {0}", message + " - " + i);

    Console.WriteLine(" Press [enter] to exit.");
}

Console.ReadLine();
