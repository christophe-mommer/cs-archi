using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Behavior
{
    public abstract class BusinessRule
    {
        protected BusinessRule Next { get; set; }

        public virtual void Evaluate()
        {
            Next.Evaluate();
        }
    }

    public class RuleOne : BusinessRule
    {
        public override void Evaluate()
        {
            Console.WriteLine("First business rule");
            Next = new RuleTwo();
            base.Evaluate();
        }
    }
    public class RuleTwo : BusinessRule
    {
        public override void Evaluate()
        {
            Console.WriteLine("Second business rule");
            base.Evaluate();
        }
    }
}
