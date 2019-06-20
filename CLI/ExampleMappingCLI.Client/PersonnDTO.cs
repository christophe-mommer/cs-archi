using ExampleMappingCLI.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMappingCLI.Client
{
    [MapFromClass(typeof(Person))]
    public class PersonDTO
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public Status Status { get; set; }
    }
}
