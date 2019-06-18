using System;
using System.Collections.Generic;
using System.Text;

namespace MicroORM.Common
{
    class TrackedModel
    {
        private DataModel _instance;
        internal DataModel Instance
        {
            get => _instance;
            set
            {
                _instance = value;
                UpdateDate = DateTime.Now;
            }
        }
        public DateTime UpdateDate { get; set; }

        public TrackedModel(DataModel instance)
        {
            Instance = instance;
        }

        public override bool Equals(object obj)
        => (obj as TrackedModel)?.Instance?.Id == Instance.Id;

        public override int GetHashCode()
            => (Instance.GetType().AssemblyQualifiedName + Instance.Id).GetHashCode();
    }
}
