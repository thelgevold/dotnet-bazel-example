using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "demo-greeting",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                while (true)
                {
                    string message = $"Hello World! {DateTime.Now.ToString()}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                     routingKey: "demo-greeting",
                                     basicProperties: null,
                                     body: body);

                    Console.WriteLine(" [x] Sent {0}", message);

                    Thread.Sleep(1000);
                }
            }
        }
    }
}