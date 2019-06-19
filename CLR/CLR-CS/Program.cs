using System;

namespace CLR_CS
{
    class UserInterface
    {
        public string Value { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a value");
            var userInterface = new UserInterface();
            userInterface.Value = Console.ReadLine();
            Console.WriteLine($"You enter {userInterface.Value}");
        }
    }
}
