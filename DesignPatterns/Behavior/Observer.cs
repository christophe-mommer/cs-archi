using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Behavior
{
    class Observable
    {
        public event Action<string> OnValueChanged;

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged(value);
            }
        }
        private string _value;
    }
    class Observer
    {
        public void DoObservation()
        {
            var observable = new Observable();
            observable.OnValueChanged += (s) => Console.WriteLine($"New value = {s}");

            observable.Value = "new";
        }
    }
}
