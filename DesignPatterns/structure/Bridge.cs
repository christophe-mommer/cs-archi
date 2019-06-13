using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structure
{
    class CustomerData
    {
        public string Source { get; set; }
    }
    interface ICustomerDataProvider
    {
        CustomerData RetrieveData();
    }
    class SQLCustomerDataProvider : ICustomerDataProvider
    {
        public CustomerData RetrieveData() => new CustomerData { Source = "SQL" };
    }
    class CSVCustomerDataProvider : ICustomerDataProvider
    {
        public CustomerData RetrieveData() => new CustomerData { Source = "CSV" };
    }
    interface ICustomerService
    {
        void GetCustomerData(ICustomerDataProvider dataProvider);
    }
    class BaseCustomerService : ICustomerService
    {
        public void GetCustomerData(ICustomerDataProvider dataProvider)
        {
            var data = dataProvider.RetrieveData();
            //Do something with data
        }
    }
}
