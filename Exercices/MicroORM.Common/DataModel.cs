using System;
using System.Collections.Generic;
using System.Text;

namespace MicroORM.Common
{
    [Serializable]
    public abstract class DataModel
    {
        public Guid Id { get; protected internal set; }
    }
}
