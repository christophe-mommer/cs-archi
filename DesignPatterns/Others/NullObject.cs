using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Others
{
    class DomainObjectThatCanBeNull
    {
        public static DomainObjectThatCanBeNull Empty
            => new DomainObjectThatCanBeNull(string.Empty);

        public string ValueData { get; set; }

        public DomainObjectThatCanBeNull(string valueData)
        {
            ValueData = valueData;
        }

        public override bool Equals(object obj) 
            => (obj as DomainObjectThatCanBeNull)?.ValueData == ValueData;

        public override int GetHashCode()
             => ValueData.GetHashCode();
    }
}
