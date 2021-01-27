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
        private Dictionary<int, int> _requiredColumnWidths { get; set; }
        private TableConfiguration _configuration { get; set; }

        public static ConsoleTable Create()
        {
            return new ConsoleTable();
        }
        private ConsoleTable()
        {
            Headers = new string[0];
            Data = new List<string[]>();
            _requiredColumnWidths = new Dictionary<int, int>();
            _configuration = TableConfiguration.Default;
        }

        public ConsoleTable WithHeaders(string[] headers)
        {
            _headerTotalLength = headers.Sum(x => x.Length);
            UpdateColumnWidthValues(headers);

            Headers = headers;
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
            var formattedData = data.Select(row => row.Select(item => item.ToString()).ToArray()).ToList();

            Data = formattedData;
            return this;
        }

        public ConsoleTable AddDataRow(string[] row)
        {
            UpdateColumnWidthValues(row);

            Data.Add(row);
            return this;
        }

        public ConsoleTable AddDataRow(IRowConvertable obj)
        {
            var row = obj.MapToRow();
            UpdateColumnWidthValues(row);
            Data.Add(row);
            return this;
        }

        public ConsoleTable AddDataRow<T>(T[] row)
        {
            var formattedData = row.Select(x => x.ToString()).ToArray();
            UpdateColumnWidthValues(formattedData);

            Data.Add(formattedData);
            return this;
        }

        public ConsoleTable WithConfiguration(TableConfiguration config)
        {
            _configuration = config;
            return this;
        }

        public void PrintTable()
        {
            PrintHeaders();
            PrintData();
        }

        private void PrintHeaders()
        {
            var columnSpacing = CreateColumnSpacing();
            
            for(var x = 0; x < Headers.Length; x++)
            {
                var (beforeSpace, afterSpace) = CreateRequiredFillSpacing(x, Headers[x]);
                Console.Write($"{columnSpacing}{beforeSpace}{Headers[x]}{afterSpace}");
            }
            Console.Write(columnSpacing);
            Console.WriteLine();

            if (_configuration.DisplayHeaderDivider)
            {
                var lengthOfDivider = _headerTotalLength + (_configuration.ColumnSpacing * Headers.Length) + _configuration.ColumnSpacing;
                var divider = String.Concat(Enumerable.Repeat("-", lengthOfDivider));
                Console.WriteLine(divider);
            }
           
        }

        private void PrintData()
        {
            var columnSpacing = CreateColumnSpacing();
            foreach(var row in Data)
            {
                for(var x = 0; x < row.Length; x++)
                {
                    var (beforeSpace, afterSpace) = CreateRequiredFillSpacing(x, row[x]);
                    Console.Write($"{columnSpacing}{beforeSpace}{row[x]}{afterSpace}");
                }
                Console.WriteLine();
            }
        }

        private string CreateColumnSpacing()
        {
            return String.Concat(Enumerable.Repeat(" ", _configuration.ColumnSpacing));
        }

        private (string, string) CreateRequiredFillSpacing(int columnIndex, string data)
        {
            var leftoverSpace = _requiredColumnWidths[columnIndex] - data.Length;
            var numberOfSpaces = Math.Round((double)(leftoverSpace / 2), MidpointRounding.AwayFromZero);

            var beforeSpace = String.Concat(Enumerable.Repeat(" ", (int)numberOfSpaces));
            var afterSpace = String.Concat(Enumerable.Repeat(" ", leftoverSpace % 2 == 0 ? (int)numberOfSpaces : (int)numberOfSpaces - 1));

            return (beforeSpace, afterSpace);
        }

        private void UpdateColumnWidthValues(string[] data)
        {
            for(int x = 0; x < data.Length; x ++)
            {
                if(_requiredColumnWidths.ContainsKey(x))
                {
                    if (data[x].Length < _requiredColumnWidths[x])
                    {
                        _requiredColumnWidths[x] = data[x].Length;
                    }
                }
                else
                {
                    _requiredColumnWidths[x] = data[x].Length;
                }

                
            }
        }


    }
}
