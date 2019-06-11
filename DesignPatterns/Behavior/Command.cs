using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Behavior
{
    interface ICommand { }
    interface ICommandHandler<T> where T : ICommand
    {
        void Handle(T command);
    }
    class ComputeCommand : ICommand
    {
        public int FirtsValue { get; set; }
        public int SecondValue { get; set; }
    }

    class ComputeCommandHandler : ICommandHandler<ComputeCommand>
    {
        public void Handle(ComputeCommand command)
        {
            Console.WriteLine($"Result = {command.FirtsValue + command.SecondValue}");
        }
    }
}
