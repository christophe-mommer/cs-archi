using MicroORM.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Message
    {
        public string Data { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }

        public Message()
        {

        }
        public Message(DataModel data, string operation)
        {
            Data = JsonConvert.SerializeObject(data);
            Operation = operation;
            Type = data.GetType().AssemblyQualifiedName;
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
