using System;
using System.Collections.Generic;
using System.Text;

namespace MicroORM.Common
{
    public abstract class DataModel
    {
        public Guid Id { get; protected internal set; }
    }
}
