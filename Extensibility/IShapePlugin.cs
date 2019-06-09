using System;
using System.Collections.Generic;
using System.Text;

namespace Extensibility
{
    interface IShapePlugin
    {
        string GetName();

        void DoSomethingWithShape(Shape s);
    }
}
