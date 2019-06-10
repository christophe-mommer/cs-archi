using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControl
{
    public class ServiceCaller
    {
        private IService _service;
        public ServiceCaller(IService service)
        {
            _service = service;
        }

        public void DoSomething() => _service.DoSomething();
    }
}
