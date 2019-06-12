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
    }
}
