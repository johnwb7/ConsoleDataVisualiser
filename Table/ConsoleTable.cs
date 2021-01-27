using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDataVisualiser.Table
{
    public class ConsoleTable
    {
        public string[] Headers { get; private set; }
        public List<string[]> Data {get; private set; }
        
        private TableConfiguration _configuration { get; set; }

        public static ConsoleTable Create()
        {
            return new ConsoleTable();
        }
        private ConsoleTable()
        {
            Headers = new string[0];
            Data = new List<string[]>();
            _configuration = TableConfiguration.Default;
        }

        public ConsoleTable WithHeaders(string[] headers)
        {
            Headers = headers;
            return this;
        }

        public ConsoleTable WithData(List<string[]> data)
        {
            Data = data;
            return this;
        }

        public ConsoleTable WithData(List<IRowConvertable[]> data)
        {
            
        }

        public ConsoleTable WithData<T>(List<T[]> data)
        {

        }

        public ConsoleTable AddDataRow(string[] row)
        {
            Data.Add(row);
            return this;
        }

        public ConsoleTable AddDataRow(IRowConvertable obj)
        {
            Data.Add(obj.MapToRow());
            return this;
        }

        public ConsoleTable AddDataRow<T>(T[] row)
        {
            Data.Add(row.Select(x => x.ToString()).ToArray());
            return this;
        }

        public ConsoleTable WithConfiguration(TableConfiguration config)
        {
            _configuration = config;
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
