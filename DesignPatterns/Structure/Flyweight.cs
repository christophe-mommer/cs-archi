using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structure
{
    interface IShape
    {
        void Draw();
        int X { get; }
        int Y { get; }
        int Height { get; }
        int Width { get; }
    }

    class SquareShape : IShape
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public void Draw()
        {

        }
    }
    //class TriangleShape : IShape
    //{
    //    public void Draw()
    //    {
    //    }
    //}

    class Picture
    {

    }
}
