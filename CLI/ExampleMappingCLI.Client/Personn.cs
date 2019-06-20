using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMappingCLI.Client
{
    public enum Status
    {
        Standard,
        Silver,
        Gold,
        Platinum
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Status Status { get; set; }
    }
}
