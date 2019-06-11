using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structure
{
    class Amount
    {
        public decimal Value { get; private set; }
        public Amount(decimal value)
        {
            Value = value;
        }
    }
    class AccountNumber
    {
        public string Value { get; private set; }
        public AccountNumber(string value)
        {
            Value = value;
        }
    }
    class Transfer
    {
        public Amount Amount { get; set; }
        public DateTime Date { get; set; }
        public AccountNumber From { get; set; }
        public AccountNumber To { get; set; }
    }

    //this is the aggregate
    class Account 
    {
        public IEnumerable<Transfer> Transfers { get; set; }
        public AccountNumber Number { get; set; }
        public Amount Balance { get; set; }
    }
}
