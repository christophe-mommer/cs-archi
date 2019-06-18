using MicroORM.Common;
using System;
using System.Linq;

namespace MicroORMSample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Models

            var steveJobs = new Person
            {
                Name = "Steve",
                LastName = "Jobs",
                Birthday = new DateTime(1955, 02, 24)
            };
            var billGates = new Person
            {
                Name = "Bill",
                LastName = "Gates",
                Birthday = new DateTime(1955, 10, 28)
            };

            var adress1 = new Address
            {
                Line1 = "Le Terra Mundi",
                Line2 = "Rue d'Aubigny",
                ZipCode = "69003",
                Town = "Lyon",
                Country = "France"
            };
            var adress2 = new Address
            {
                Line1 = "555 110th Ave NE",
                Line2 = "Bellevue",
                ZipCode = "WA 98004",
                Town = "Seattle",
                Country = "USA"
            };

            #endregion

            var orm = new MicroORMContext(InMemoryDataAdapter.Instance);

            orm.OnSaveChanged += ((int NbAdd, int NbUpdates, int NbDeletes) data) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"A saved has been performed with {data.NbAdd} add(s), " +
                    $"{data.NbUpdates} update(s) and {data.NbDeletes} deletion(s)");
                Console.ResetColor();
            };

            orm.Add(steveJobs);
            orm.Add(billGates);
            orm.Add(adress1);
            orm.Add(adress2);
            orm.Save();

            try
            {
                orm.Delete(new Person());
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("It fails on delete, as expected");
            }

            try
            {
                orm.Add(steveJobs);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("It fails on add, as expected");
            }

            var persons = orm.Get<Person>(m => true);
            void DisplayPersons()
            {
                persons = orm.Get<Person>(m => true);
                foreach (var item in persons)
                {
                    Console.WriteLine($"{item.Name} {item.LastName} is born the {item.Birthday}");
                }
            }

            DisplayPersons();

            orm.Delete(persons.First(m => m.LastName == "Jobs"));
            orm.Save();

            DisplayPersons();

            orm.Update(steveJobs);
            orm.Save();

            DisplayPersons();

            foreach (var item in orm.Get<Address>(m => m.Country == "USA"))
            {
                Console.WriteLine("Adress : ");
                Console.WriteLine($"\t {item.Line1}");
                Console.WriteLine($"\t {item.Line2}");
                Console.WriteLine($"\t {item.ZipCode}");
                Console.WriteLine($"\t {item.Town}");
                Console.WriteLine($"\t {item.Country}");
            }

            Console.ReadLine();
        }
    }
}
