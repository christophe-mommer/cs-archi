using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Behavior
{
    interface IPaymentState
    {
        void Pay(decimal amount);
    }
    class CreditCardPaymentState : IPaymentState
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Credit card debited ({amount}) !");
        }
    }
    class PaypalPaymentState : IPaymentState
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paypal account debited ({amount}) !");
        }
    }

    class ShoppingCart // Context
    {
        public IPaymentState State { get; set; }
        public IEnumerable<string> ArticleReferences { get; set; }

        public void Checkout()
        {
            decimal amount = 0;
            foreach (var item in ArticleReferences)
            {
                amount += 10;
            }
            if (amount > 100)
            {
                State = new CreditCardPaymentState();
            }
            else
            {
                State = new PaypalPaymentState();
            }
            State.Pay(amount);
        }
    }
}
