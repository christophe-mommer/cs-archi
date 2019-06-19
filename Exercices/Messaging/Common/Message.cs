using MicroORM.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    [Serializable]
    public class Message
    {
        public DataModel Data { get; set; }
        public string Operation { get; set; }

        public Message()
        {

        }
        public Message(DataModel data, string operation)
        {
            Data = data;
            Operation = operation;
        }
    }

    public class Message<T> : Message where T : DataModel
    {
        public Message(T value, string operation)
            :base(value, operation)
        {

        }
    }

}
