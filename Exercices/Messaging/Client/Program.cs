using Common;
using MicroORM.Common;
using RabbitMQ.Client;
using System;
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
                    string serializedMessage =
                        Newtonsoft.Json.JsonConvert.SerializeObject(new Message<T>(data, operation));
                    var message = Encoding.UTF8.GetBytes(serializedMessage);
                    channel.BasicPublish(exchange: "MessageQueuing",
                                         routingKey: "",
                                         basicProperties: null,
                                         body: message);
                }
            }
        }
    }
}
