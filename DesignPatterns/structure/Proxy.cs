using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structure
{

    interface ISensibleInfosGetter
    {
        SensibleDomainModel GetSensibleInfos();
    }

    class Proxy : ISensibleInfosGetter
    {
        private readonly string _userName;
        private readonly string _password;

        public Proxy(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }
        public SensibleDomainModel GetSensibleInfos()
        {
            if (_userName == "admin" && _password == "password")
                return new SensibleDomainModel
                { HighValueData = "Bill Gate's Credit card = 1234 5678 9101 1213" };
            return null;
        }

    }

    class SensibleDomainModel : ISensibleInfosGetter
    {
        public string HighValueData { get; set; }

        public SensibleDomainModel GetSensibleInfos()
            => this;
    }
}
