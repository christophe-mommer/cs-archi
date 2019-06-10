using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControl
{

    class Program
    {
        static void Main(string[] args)
        {
            #region Without IoC

            if (DateTime.Now.TimeOfDay.TotalHours < 12)
            {
                new ServiceCaller(new ServiceOne()).DoSomething();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                new ServiceCaller(new ServiceTwo()).DoSomething();
            }
            else
            {
                new ServiceCaller(new ServiceThree()).DoSomething();
            }

            Console.ReadLine();

            #endregion

            #region With If

            IService service = null;
            if (DateTime.Now.TimeOfDay.TotalHours < 12)
            {
                service = new ServiceOne();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                service = new ServiceTwo();
            }
            else
            {
                service = new ServiceThree();
            }
            new ServiceCaller(service).DoSomething();

            Console.ReadLine();

            #endregion

            #region With IoC

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ServiceCaller>().AsSelf();
            containerBuilder.Register<IService>(_ =>
            {
                if (DateTime.Now.TimeOfDay.TotalHours < 12)
                {
                    return new ServiceOne();
                }
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    return new ServiceTwo();
                }
                else
                {
                    return new ServiceThree();
                }
            }).As<IService>();

            var container = containerBuilder.Build();

            container.Resolve<ServiceCaller>().DoSomething();

            Console.ReadLine();

            #endregion
        }
    }
}
