using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Behavior
{
    class Context
    {
        public bool SystemOn { get; set; }
    }
    abstract class Expression
    {
        public abstract void Interpret(Context c);
    }

    class SqlExpression : Expression
    {
        public SqlExpression(string sqlCommand)
        {
            SqlCommand = sqlCommand;
        }

        public string SqlCommand { get; }

        public override void Interpret(Context c)
        {
            if (c.SystemOn)
            {
                Console.WriteLine("Sql Server is on, launching command and getting result");
            }
            else
            {
                Console.WriteLine("Sql Server is off, interpretation is impossible");
            }
        }
    }

    class NoSqlExpressoin : Expression
    {
        public NoSqlExpressoin(string jsonQuery)
        {
            JsonQuery = jsonQuery;
        }

        public string JsonQuery { get; }

        public override void Interpret(Context c)
        {
            if (c.SystemOn)
            {
                Console.WriteLine("MongoDb is on, launching command and getting result");
            }
            else
            {
                Console.WriteLine("MongoDb is off, interpretation is impossible");
            }
        }
    }
}
