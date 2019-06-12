using System;
using System.Collections.Generic;
using System.Text;

namespace MicroORM.Common
{
    public class Address : DataModel
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
    }
}
