using System;
using System.Collections.Generic;

namespace ConsoleDataVisualiser.Table
{
    public class ConsoleTable<T> where T : struct
    {
        public string[] Headers { get; private set; }
        public List<T[]> Data {get; private set; }
        
        private TableConfiguration _configuration { get; set; }

        public static ConsoleTable<T> NewConsoleTable()
        {
            return new ConsoleTable<T>();
        }
        private ConsoleTable()
        {
            Headers = new string[0];
            Data = new List<T[]>();
        }

        public ConsoleTable<T> WithHeaders(string[] headers)
        {
            Headers = headers;
            return this;
        }

        public ConsoleTable<T> WithData(List<T[]> data)
        {
            Data = data;
            return this;
        }

        public ConsoleTable<T> WithConfiguration(TableConfiguration config)
        {
            _configuration = config;
            return this;
        }

        public ConsoleTable<T> AddDataRow(T[] row)
        {
            Data.Add(row);
            return this;
        }

        public void PrintTable()
        {
            foreach(var header in Headers)
            {
                Console.Write($"  {header}  ");
            }
        }


    }
}
