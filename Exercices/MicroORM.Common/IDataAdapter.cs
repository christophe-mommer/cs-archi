using System;
using System.Collections.Generic;

namespace MicroORM.Common
{
    public interface IDataAdapter
    {
        IEnumerable<T> Get<T>(Func<T, bool> lambda) where T : DataModel;
        void Delete(DataModel model);
        void Add(DataModel model);
        void Update(DataModel model);
        void Save();
    }
}