using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MicroORM.Common
{
    public class MicroORMContext
    {
        private int NbAdds = 0;
        private int NbUpdates = 0;
        private int NbDeletes = 0;
        public event Action<(int NbAdd, int NbUpdates, int NbDeletes)> OnSaveChanged;

        #region Solution


        private static MicroORMContext s_instance;
        private static object s_threadSafety = new object();
        private readonly IDataAdapter _dataAdapter;

        public MicroORMContext(IDataAdapter dataAdapter)
        {
            _dataAdapter = dataAdapter;
        }

        public IEnumerable<T> Get<T>(Func<T, bool> predicate)
            where T : DataModel
            => _dataAdapter.Get<T>(predicate);

        public void Add(DataModel data)
        {
            _dataAdapter.Add(data);
            NbAdds++;
        }

        public void Update(DataModel data)
        {
            _dataAdapter.Update(data);
            NbUpdates++;
        }

        public void Delete(DataModel data)
        {
            _dataAdapter.Delete(data);
            NbDeletes++;
        }
    
        public void Save()
        {
            _dataAdapter.Save();
            OnSaveChanged?.Invoke((NbAdds, NbUpdates, NbDeletes));
            NbAdds = 0;
            NbUpdates = 0;
            NbDeletes = 0;
        }
        #endregion
    }
}
