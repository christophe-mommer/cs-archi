using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MicroORM.Common
{
    public class MicroORMContext
    {
        private List<TrackedModel> _models { get; set; } = new List<TrackedModel>();
        private List<TrackedModel> _tempList { get; set; } = new List<TrackedModel>();
        private List<(Type, Guid)> _toDelete { get; set; } = new List<(Type, Guid)>();
        private int NbAdds = 0;
        private int NbUpdates = 0;
        public event Action<(int NbAdd, int NbUpdates, int NbDeletes)> OnSaveChanged;

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
            NbAdds++;
            if (data.Id == Guid.Empty)
            {
                data.Id = Guid.NewGuid();
            }
            _tempList.Add(new TrackedModel(data));
        }

        public void Update(DataModel data)
        {
            var existingData = _models.FirstOrDefault(m => m.Instance.Id == data.Id && m.Instance.GetType() == data.GetType());
            if (existingData != null)
            {
                _models.Remove(existingData);
            }
            NbUpdates++;
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

        public void Delete<T>(Guid id) where T : DataModel
        {
            if (!_models.Any(m => m.Instance.Id == id && m.Instance.GetType() == typeof(T)))
            {
                throw new InvalidOperationException($"Cannot delete element of type {typeof(T).Name} with the Id {id} cause it doesn't exists");
            }
            _toDelete.Add((typeof(T), id));
        }

        public void Save()
        {
            foreach (var item in _toDelete)
            {
                var element = _models.First(m => m.Instance.Id == item.Item2 && m.Instance.GetType() == item.Item1);
                _models.Remove(element);
            }
            foreach (var item in _tempList)
            {
                _models.Add(item);
            }
            OnSaveChanged?.Invoke((NbAdds, NbUpdates, _toDelete.Count));
            NbAdds = 0;
            NbUpdates = 0;
            _toDelete.Clear();
            _tempList.Clear();
        }
    }
}
