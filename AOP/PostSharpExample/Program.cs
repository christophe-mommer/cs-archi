using System;

namespace PostSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello PostSharp!");
            MyMethod();
            try
            {
                MyFailureMethod();
            }
            catch { }
            Console.ReadLine();

        }

        [LogMethodBoundary]
        static void MyMethod()
        {
            Console.WriteLine("This is just a cool method that does cool stuff");
        }

        [LogMethodBoundary]
        static void MyFailureMethod()
        {
            Console.WriteLine("This method is sooo bad, it fails everytime");
            throw new Exception();
        }
    }
}
