using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDataVisualiser.Table
{
    public class ConsoleTable
    {
        public string[] Headers { get; private set; }
        public List<string[]> Data {get; private set; }

        private int _headerTotalLength { get; set; }
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
            _headerTotalLength = headers.Sum(x => x.Length);
            return this;
        }

        public ConsoleTable WithData(List<string[]> data)
        {
            Data = data;
            return this;
        }

        public ConsoleTable WithData(IRowConvertable[] data)
        {
            Data = data.Select(obj => obj.MapToRow().ToArray()).ToList();
            return this;
        }

        public ConsoleTable WithData<T>(IEnumerable<T[]> data)
        {
            Data = data.Select(row => row.Select(item => item.ToString()).ToArray()).ToList();
            return this;
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
            PrintHeaderSection();

            foreach(var row in Data)
            {
                foreach(var item in row)
                {
                    Console.Write($"  {item}  ");
                }
            }
        }

        private void PrintHeaderSection()
        {
            var headerSpacing = String.Concat(Enumerable.Repeat(" ", _configuration.HeaderSpacing));
            foreach(var header in Headers)
            {
                Console.Write($"{headerSpacing}{header}");
            }
            Console.Write(headerSpacing);
            Console.WriteLine();

            if (_configuration.DisplayHeaderDivider)
            {
                var lengthOfDivider = _headerTotalLength + (_configuration.HeaderSpacing * Headers.Length) + _configuration.HeaderSpacing;
                var divider = String.Concat(Enumerable.Repeat("-", lengthOfDivider));
                Console.WriteLine(divider);
            }
           
        }


    }
}
