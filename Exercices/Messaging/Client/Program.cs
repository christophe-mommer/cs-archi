using Common;
using MicroORM.Common;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to send person");
            Console.ReadLine();
            SendData(new Person
            {
                Name = "Steve",
                LastName = "Jobs",
                Birthday = new DateTime(1955, 02, 24)
            }, "Add");
            Console.WriteLine("Press enter to send address");
            Console.ReadLine();
            SendData(new Address
            {
                Line1 = "Le Terra Mundi",
                Line2 = "Rue d'Aubigny",
                ZipCode = "69003",
                Town = "Lyon",
                Country = "France"
            }, "Add");
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
        static void SendData<T>(T data, string operation) where T : DataModel
        {
            var factory = new RabbitMQ.Client.ConnectionFactory {  HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("MessageQueuing", ExchangeType.Fanout);
                    using (var memStream = new MemoryStream())
                    {
                        var message = new Message(data, operation);
                        new BinaryFormatter().Serialize(memStream, message);
                        var body = memStream.ToArray();
                        channel.BasicPublish(exchange: "MessageQueuing",
                                             routingKey: "",
                                             basicProperties: null,
                                             body: body);
                    }
                }
            }
        }
    }
}
