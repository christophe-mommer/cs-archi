using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InversionOfControl
{
    public interface IService
    {
        void DoSomething();
    }

    public class ServiceOne : IService
    {
        public void DoSomething() => Console.WriteLine("S1 : Do something");
    }

    public class ServiceTwo : IService
    {
        public void DoSomething() => Console.WriteLine("S2 : Do something");
    }

    public class ServiceThree : IService
    {
        public void DoSomething() => Console.WriteLine("S3 : Do something");
    }
}
