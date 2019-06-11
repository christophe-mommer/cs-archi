using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structure
{
    class Proxy
    {

        SensibleDomainModel GetSensibleInfos(string userName, string password)
        {
            if (userName == "admin" && password == "password")
                return new SensibleDomainModel
                { HighValueData = "Bill Gate's Credit card = 1234 5678 9101 1213" };
            return null;
        }

    }
    class SensibleDomainModel
    {
        public string HighValueData { get; set; }
    }
}
