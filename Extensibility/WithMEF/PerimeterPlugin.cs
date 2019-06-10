using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace Extensibility
{
    [Export(typeof(IShapePlugin))]
    class PerimeterPlugin : IShapePlugin
    {
        public void DoSomethingWithShape(Shape sh)
            => Console.WriteLine($"The perimeter of the shape is {sh.Sides.Sum(s => s.Length)}");

        public string GetName()
            => "Compute perimeter";
    }
}
