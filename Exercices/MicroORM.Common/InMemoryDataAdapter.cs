using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroORM.Common
{
    public class InMemoryDataAdapter : IDataAdapter
    {

        #region Members

        private ConcurrentBag<TrackedModel> _models { get; set; } = new ConcurrentBag<TrackedModel>();
        private List<TrackedModel> _tempList { get; set; } = new List<TrackedModel>();
        private List<TrackedModel> _toDeleteList { get; set; } = new List<TrackedModel>();
        private List<(Type, Guid)> _toDelete { get; set; } = new List<(Type, Guid)>();

        #endregion

        #region Singleton

        internal static ConcurrentBag<TrackedModel> Models { get; private set; }
            = new ConcurrentBag<TrackedModel>();
        private static InMemoryDataAdapter s_instance;
        private static object s_threadSafety = new object();

        public static InMemoryDataAdapter Instance
        {
            get
            {
                if (s_instance == null)
                {
                    lock (s_threadSafety)
                    {
                        if (s_instance == null)
                        {
                            s_instance = new InMemoryDataAdapter();
                        }
                    }
                }
                return s_instance;
            }
        }

        #endregion

        #region Public methods

        public IEnumerable<T> Get<T>(Func<T, bool> predicate)
            where T : DataModel
        => _models
                .Where(m => m.Instance.GetType() == typeof(T))
                .Select(m => m.Instance)
                .Cast<T>()
                .Where(predicate);
        public void Add(DataModel data)
        {
            if (_models.Any(m => m.Instance.Id == data.Id && m.Instance.GetType() == data.GetType()))
            {
                throw new InvalidOperationException($"An element of type {data.GetType().Name} with the Id {data.Id} already exists.");
            }
            if (data.Id == Guid.Empty)
            {
                data.Id = Guid.NewGuid();
            }
            _tempList.Add(new TrackedModel(data));
        }
        public void Update(DataModel data)
        {
            var existingData = _models
                .FirstOrDefault(m => m.Instance.Id == data.Id && m.Instance.GetType() == data.GetType());
            if (existingData != null)
            {
                _toDeleteList.Add(existingData);
            }
            _tempList.Add(new TrackedModel(data));
        }
        public void Delete(DataModel data)
        {
            if (!_models.Any(m => m.Instance.Id == data.Id && m.Instance.GetType() == data.GetType()))
            {
                throw new InvalidOperationException($"Cannot delete element of type {data.GetType().Name} with the Id {data.Id} cause it doesn't exists");
            }
            _toDelete.Add((data.GetType(), data.Id));
        }
        public void Save()
        {
            lock (s_threadSafety)
            {
                foreach (var item in _toDelete)
                {
                    _toDeleteList.Add(_models.First(m => m.Instance.Id == item.Item2 && m.Instance.GetType() == item.Item1));
                }
                _models = new ConcurrentBag<TrackedModel>(_models.Except(_toDeleteList));
                foreach (var item in _tempList)
                {
                    _models.Add(item);
                }
                _toDelete.Clear();
                _tempList.Clear();
                _toDeleteList.Clear();
            }
        }

        #endregion

    }
}
