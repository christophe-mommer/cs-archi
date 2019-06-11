using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structure
{
    class DataModel
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }

    class TrackedModel<T> where T : class
    {
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }

        public TrackedModel(T model)
        {
            Model = model;
        }

        public T Model { get; }
    }
}
