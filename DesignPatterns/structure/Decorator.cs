using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.structure
{
    class DataModel
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }

    class TrackedDataModel
    {
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }

        public TrackedDataModel(DataModel model)
        {
            Model = model;
        }

        public DataModel Model { get; }
    }
}
