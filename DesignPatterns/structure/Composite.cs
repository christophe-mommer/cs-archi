using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Structure
{
    interface IDrawable
    {
        void Draw();
    }
    class Square : IDrawable
    {
        public void Draw()
        {

        }
    }
    class Triange : IDrawable
    {
        public void Draw()
        {

        }
    }
    class Rectangle : IDrawable
    {
        public void Draw()
        {

        }
    }
    class Canvas : IDrawable
    {
        private readonly List<IDrawable> _shapes;

        public Canvas(params IDrawable[] shapes)
        {
            _shapes = shapes.ToList();
        }
        public void Draw()
             => _shapes.ForEach(s => s.Draw());
    }
}
