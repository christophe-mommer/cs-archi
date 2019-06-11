using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Behavior
{
    class State : ICloneable
    {
        public string Value { get; set; }

        public object Clone()
            => MemberwiseClone();
    }
    class StatefulElement
    {
        private readonly Stack<State> _states = new Stack<State>();
        private State _currentState = new State();

        public void UpdateValue(string value)
        {
            _states.Push(_currentState.Clone() as State);
            _currentState.Value = value;
        }
        public void Undo()
        {
            if(_states.Count > 0)
            {
                _currentState = _states.Pop();
            }
        }
    }
}
