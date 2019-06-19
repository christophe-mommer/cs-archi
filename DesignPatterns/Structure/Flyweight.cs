using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Structure
{
    interface IShape
    {
        void Draw();
        int X { get; }
        int Y { get; }
    }

    class SquareShape : IShape
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int SideLength { get; set; }

        public void Draw()
        {
        }
        public override bool Equals(object obj)
            => (obj as SquareShape)?.SideLength == SideLength;
        public override int GetHashCode()
            => (typeof(SquareShape).AssemblyQualifiedName + SideLength).GetHashCode();
    }
    class TriangleShape : IShape
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int FirstSizeLength { get; set; }
        public int SecondSizeLength { get; set; }
        public int ThirdSizeLength { get; set; }

        public void Draw()
        {
        }
        public override bool Equals(object obj)
        {
            var otherTriangle = (obj as TriangleShape);
            return otherTriangle?.FirstSizeLength == FirstSizeLength
                && otherTriangle?.SecondSizeLength == SecondSizeLength
                && otherTriangle?.ThirdSizeLength == ThirdSizeLength;
        }
        public override int GetHashCode()
            => (typeof(TriangleShape).AssemblyQualifiedName + FirstSizeLength + SecondSizeLength.ToString() + ThirdSizeLength).GetHashCode();
    }

    class ShapeFactory
    {
        private List<IShape> _cache = new List<IShape>();

        public IShape GetShape()
        {
            (int X, int Y) GetXY()
            {
                int x, y;
                Console.WriteLine("Set the X value");
                x = int.Parse(Console.ReadLine());
                Console.WriteLine("Set the Y value");
                y = int.Parse(Console.ReadLine());
                return (x, y);
            }
            (int x, int y) = GetXY();
            Console.WriteLine("Select the shape you want to draw");
            Console.WriteLine("1. Square");
            Console.WriteLine("2. Triangle");
            var choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Enter the side length");
                var side = int.Parse(Console.ReadLine());


                var existingSquare =
                    _cache
                        .Where(s => s is SquareShape)
                        .Cast<SquareShape>()
                        .FirstOrDefault(s => s.SideLength == side);
                if (existingSquare != null)
                {
                    existingSquare.Draw();
                }
                else
                {
                    existingSquare = new SquareShape { SideLength = side };
                    _cache.Add(existingSquare);
                }
                return existingSquare;
            }
            else
            {
                int GetSide(int sideNumber)
                {
                    Console.WriteLine($"Enter the lenght of the side n°{sideNumber}");
                    return int.Parse(Console.ReadLine());
                }
                int[] sides = new int[3];
                for (int i = 1; i <= 3; i++)
                {
                    sides[i - 1] = GetSide(i);
                }

                var existingTriangle =
                   _cache
                       .Where(s => s is TriangleShape)
                       .Cast<TriangleShape>()
                       .FirstOrDefault(s => s.FirstSizeLength == sides[0] && s.SecondSizeLength == sides[1] && s.ThirdSizeLength == sides[2]);
                if (existingTriangle != null)
                {
                    existingTriangle.Draw();
                }
                else
                {
                    existingTriangle = new TriangleShape { FirstSizeLength = sides[0], SecondSizeLength = sides[1], ThirdSizeLength = sides[2] };
                    _cache.Add(existingTriangle);
                }

                return existingTriangle;
            }
        }
    }

    class Picture
    {
        public void DrawPicture()
        {
            var factory = new ShapeFactory();
            var shape = factory.GetShape();
            shape.Draw();
        }
    }
}
