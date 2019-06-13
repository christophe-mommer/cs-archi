using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Behavior
{
    abstract class BaseCalculator
    {
        public void PerformOperation()
        {
            (int first, int second) = GetData();
            if (!CheckPositivity(first, second))
            {
                Console.WriteLine("Both number should be positive");
                return;
            }
            var operation = GetOperation();
            switch (operation)
            {
                case "Add":
                    Console.WriteLine($"Add : {first} + {second} = { first + second}");
                    break;
                case "Substract":
                    Console.WriteLine($"Add : {first} + {second} = { first + second}");
                    break;
                default:
                    Console.WriteLine($"Operation {operation} not supported (yet ? )");
                    break;
            }
        }

        public virtual (int first, int second) GetData()
        {
            return (0, 0);
        }
        public virtual string GetOperation()
        {
            return "";
        }

        private bool CheckPositivity(int int1, int int2)
        {
            if (int1 < 0 || int2 < 0)
            {
                return false;
            }
            return true;
        }
    }

    class Add : BaseCalculator
    {
        public override string GetOperation()
             => "Add";
        public override (int first, int second) GetData()
        {
            Console.WriteLine("Enter two number to add them");
            var int1 = int.Parse(Console.ReadLine());
            var int2 = int.Parse(Console.ReadLine());
            return (int1, int2);
        }
    }

    class Substract : BaseCalculator
    {
        public override string GetOperation()
             => "Substract";
        public override (int first, int second) GetData()
        {
            Console.WriteLine("Enter two number to substract them");
            var int1 = int.Parse(Console.ReadLine());
            var int2 = int.Parse(Console.ReadLine());
            return (int1, int2);
        }

    }

}
