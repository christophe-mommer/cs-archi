using Common;
using MicroORM.Common;
using MicroORMSample;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
                    #region ORM

                    var orm = new MicroORMContext(InMemoryDataAdapter.Instance);
                    orm.OnSaveChanged += ((int NbAdd, int NbUpdates, int NbDeletes) data) =>
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"A saved has beend performed with {data.NbAdd} add(s), {data.NbUpdates} update(s) and {data.NbDeletes} deletion(s)");
                        Console.ResetColor();
                    };

                    #endregion
                    consumer.Received += (model, ea) =>
                    {
                        Console.WriteLine("Entering message reception");
                        #region ORM treatment

                        using (var memStream = new MemoryStream(ea.Body, 0, ea.Body.Length))
                        {
                            var message = new BinaryFormatter().Deserialize(memStream) as Message;
                            //var modelType = Type.GetType(message.Type);
                            //var modelData = Newtonsoft.Json.JsonConvert.DeserializeObject(message.Data, modelType) as DataModel;
                            var modelData = message.Data;
                            switch (message.Operation)
                            {
                                case "Add":
                                    orm.Add(modelData);
                                    break;
                                case "Update":
                                    orm.Update(modelData);
                                    break;
                                case "Delete":
                                    orm.Delete(modelData);
                                    break;
                            }
                            orm.Save();
                        }

                        #endregion
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
