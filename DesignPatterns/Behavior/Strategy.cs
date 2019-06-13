using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DesignPatterns.Behavior
{
    interface IEntity
    {
        Guid Id { get; }
        interface IEqualityStrategy
        {
            bool CheckEquality(object first, object second);
        }
        class EntityEqualityStrategy : IEqualityStrategy
        {
            public bool CheckEquality(object first, object second) => (first as IEntity)?.Id == (second as IEntity)?.Id;
        }
        class ValueObjectEqualityStrategy : IEqualityStrategy
        {
            public bool CheckEquality(object first, object second)
                => first
                    .GetType()
                    .GetRuntimeProperties()
                    .All(p => p.GetValue(first) == second.GetType().GetRuntimeProperty(p.Name).GetValue(second));
        }
        class BusinessModel
        {
            public void ExecuteBusinessRule(object obj, object obj2)
            {
                IEqualityStrategy strategy;
                if (obj is IEntity && obj2 is IEntity)
                {
                    strategy = new EntityEqualityStrategy();
                }
                else
                {
                    strategy = new ValueObjectEqualityStrategy();
                }
                if(strategy.CheckEquality(obj, obj2))
                {
                    Console.WriteLine("Objects are equals");
                }
                else
                {
                    Console.WriteLine("Objects are differents");
                }
            }

        }
    }
}
