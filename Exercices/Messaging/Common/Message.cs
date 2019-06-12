using MicroORM.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
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
}
