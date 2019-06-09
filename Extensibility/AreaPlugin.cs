using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensibility
{
    class AreaPlugin : IShapePlugin
    {
        public void DoSomethingWithShape(Shape sh)
        {
            if (sh.Sides.Count() == 4)
            {
                if(sh.Sides.All(s => s.Length == sh.Sides.First().Length))
                {
                    var oneSide = sh.Sides.First().Length;
                    Console.WriteLine($"This is a square, it's area is {oneSide * oneSide}");
                }
                else
                {
                    Console.WriteLine("Will be implemented in v2");
                }
            }
            else
            {
                Console.WriteLine("Sorry, we can't handle this kind of shape for now");
            }
        }

        public string GetName()
             => "Compute Area";
    }
}
