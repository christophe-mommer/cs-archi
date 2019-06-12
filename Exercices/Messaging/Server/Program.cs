using Common;
using MicroORM.Common;
using MicroORMSample;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server is running");
            var factory = new RabbitMQ.Client.ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("MessageQueuing", ExchangeType.Fanout);
                    var queue = channel.QueueDeclare(queue: "server_queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    channel.QueueBind(queue: "server_queue",
                                      exchange: "MessageQueuing",
                                      routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);
                    var orm = new MicroORMContext();
                    orm.OnSaveChanged += ((int NbAdd, int NbUpdates, int NbDeletes) data) =>
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"A saved has beend performed with {data.NbAdd} add(s), {data.NbUpdates} update(s) and {data.NbDeletes} deletion(s)");
                        Console.ResetColor();
                    };

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(body));

                        switch (message.Operation)
                        {
                            case "Add":
                                orm.Add(message.Data);
                                break;
                            case "Update":
                                orm.Update(message.Data);
                                break;
                            case "Delete":
                                orm.Delete(message.Data);
                                break;
                        }
                        orm.Save();
                    };

                    channel.BasicConsume(queue: queue,
                                         autoAck: true,
                                         consumer: consumer);
                    Console.ReadLine();
                }
            }
        }
    }
}
