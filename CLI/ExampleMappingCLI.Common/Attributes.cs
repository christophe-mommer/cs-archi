using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMappingCLI.Common
{
    public class MapFromClass : Attribute
    {
        public Type MappingType { get; set; }

        public MapFromClass(Type mappingType)
        {
            MappingType = mappingType;
        }
    }
}
