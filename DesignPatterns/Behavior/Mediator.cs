using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Behavior
{
    class Airplane
    {
        private readonly List<string> _acknowledgedAirplanes = new List<string>();
        public string Reference { get; private set; }
        public void Acknowledge(Airplane other)
        {
            _acknowledgedAirplanes.Add(other.Reference);
        }
        public Airplane(string reference)
        {
            Reference = reference;
        }
    }
    class Mediator
    {
        public void AcknoledgeAirplanes(params Airplane[] airplanes)
        {
            foreach (var item in airplanes)
            {
                airplanes.Where(a => a.Reference != item.Reference).ToList().ForEach(a => a.Acknowledge(item));
            }
        }
    }
}
