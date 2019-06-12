using System;
using System.Collections.Generic;
using System.Text;

namespace MicroORM.Common
{
    public class Person : DataModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
