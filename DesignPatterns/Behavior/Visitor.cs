using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Behavior
{
    class BankClient : IAsset
    {
        public List<IAsset> Assets { get; set; } = new List<IAsset>();

        public void Accept(IBankVisitor visitor) => Assets.ForEach(a => a.Accept(visitor));
    }
    class Loan : IAsset
    {
        public decimal Owed { get; set; }
        public decimal MonthlyPayment { get; set; }

        public void Accept(IBankVisitor visitor) => visitor.Visit(this);
    }
    class BankAccount : IAsset
    {
        public decimal Amount { get; set; }
        public decimal MonthlyInterest { get; set; }

        public void Accept(IBankVisitor visitor) => visitor.Visit(this);
    }
    class RealEstate : IAsset
    {
        public decimal EstimatedValue { get; set; }
        public decimal MonthlyRent { get; set; }
        public void Accept(IBankVisitor visitor) => visitor.Visit(this);
    }

    interface IBankVisitor
    {
        void Visit(RealEstate realEstate);
        void Visit(BankAccount bankAccount);
        void Visit(Loan loan);
    }

    interface IAsset
    {
        void Accept(IBankVisitor visitor);
    }

    class NetWorthVisitor : IBankVisitor
    {
        public decimal Total { get; private set; }
        public void Visit(RealEstate realEstate) => Total += realEstate.EstimatedValue;

        public void Visit(BankAccount bankAccount) => Total += bankAccount.Amount;

        public void Visit(Loan loan) => Total -= loan.Owed;
    }

    class BankManager
    {
        public void ComputeNetWorth(BankClient client)
        {
            var visitor = new NetWorthVisitor();
            client.Accept(visitor);
            Console.WriteLine($"The client worth {visitor.Total}");
        }
    }

}
