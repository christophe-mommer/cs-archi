using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creation
{
    interface IAuto
    {
        string Name { get; }
        void TurnOn();
        void TurnOff();
    }
    class Peugeot : IAuto
    {
        public string Name => "Peugeot";
        public void TurnOff() => Console.WriteLine("Peugeot Off");
        public void TurnOn() => Console.WriteLine("Peugeot On");
    }
    class Renault : IAuto
    {
        public string Name => "Renault";
        public void TurnOff() => Console.WriteLine("Renault Off");
        public void TurnOn() => Console.WriteLine("Renault On");
    }

    class Factory
    {

        public static IAuto CreateAuto(string name)
        {
            switch(name)
            {
                case "Peugeot": return new Peugeot();
                case "Renault": return new Renault();
                default: throw new InvalidOperationException($"This factory cannot handle {name}");
            }
        }

    }
}
