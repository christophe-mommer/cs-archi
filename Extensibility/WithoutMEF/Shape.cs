using System;
using System.Collections.Generic;
using System.Text;

namespace Extensibility
{
    class Shape
    {
        public IEnumerable<Side> Sides { get; private set; }

        public Shape(params Side[] sides)
        {
            if (sides.Length <= 3) throw new ArgumentException("A shape must have a least three sides");
            Sides = sides;
        }
    }

    class Side
    {
        public decimal Length { get; set; }

        public Side(decimal length)
        {
            if (length <= 0) throw new ArgumentException("Length must be greater of equal than 0");
            Length = length;
        }
    }
}
