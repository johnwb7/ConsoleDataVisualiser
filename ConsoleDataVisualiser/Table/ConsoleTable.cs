using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDataVisualiser.Table
{
    public class ConsoleTable
    {
        public string[] Headers { get; private set; } = new string[0];
        public List<string[]> Data {get; private set; } = new List<string[]>();

        private int _headerTotalLength { get; set; } = 0;
        private int _numberOfRows { get; set; } = 0;
        private Dictionary<int, int> _requiredColumnWidths { get; set; } = new Dictionary<int, int>();
        private TableConfiguration _configuration { get; set; } = TableConfiguration.Minimal;

        public static ConsoleTable Create()
        {
            return new ConsoleTable();
        }
        private ConsoleTable()
        {

        }

        public ConsoleTable WithHeaders(string[] headers)
        {
            if(Headers.Length > 0)
            {
                throw new Exception("Headers already set. Cannot set headers more than once.");
            }

            _headerTotalLength = headers.Sum(x => x.Length);
            UpdateMaxColumnWidthValues(headers);

            Headers = headers;
            return this;
        }

        public ConsoleTable WithData(List<string[]> data)
        {
            UpdateMaxColumnWidthValues(data);
            _numberOfRows += data.Count;
            Data = data;
            return this;
        }

        public ConsoleTable WithData(IRowConvertable[] data)
        {
            var formattedData = data.Select(obj => obj.MapToRow().ToArray()).ToList();
            UpdateMaxColumnWidthValues(formattedData);
            _numberOfRows += data.Length;
            return this;
        }

        public ConsoleTable WithData<T>(IEnumerable<T[]> data)
        {
            var formattedData = data.Select(row => row.Select(item => item.ToString()).ToArray()).ToList();
            UpdateMaxColumnWidthValues(formattedData);
            _numberOfRows += formattedData.Count;
            Data = formattedData;
            return this;
        }

        public ConsoleTable AddDataRow(IRowConvertable obj)
        {
            var row = obj.MapToRow();
            UpdateMaxColumnWidthValues(row);
            _numberOfRows += 1;
            Data.Add(row);
            return this;
        }

        public ConsoleTable AddDataRow<T>(T[] row)
        {
            var formattedData = row.Select(x => x.ToString()).ToArray();
            UpdateMaxColumnWidthValues(formattedData);
            _numberOfRows += 1;
            Data.Add(formattedData);
            return this;
        }

        public ConsoleTable AddDataRow(object[] row)
        {
            var formattedData = row.Select(x => x.ToString()).ToArray();
            UpdateMaxColumnWidthValues(formattedData);
            _numberOfRows += 1;
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
            Console.Write(CreateRowNumberBuffer());
            for (var x = 0; x < Headers.Length; x++)
            {
                Console.Write(CreateItemForDisplay(x, Headers[x], x == Headers.Length - 1));
            }
            Console.WriteLine();

            if (_configuration.DisplayHeaderDivider)
            {
                Console.WriteLine(CreateHeaderDivider());
            }     
        }

        private void PrintData()
        {
            for(var y = 0; y < Data.Count; y++)
            {
                var row = Data[y];
                var displayRowNumber = _configuration.DisplayRowNumbers ? (y + 1).ToString() : "";
                Console.Write(displayRowNumber);
                for (var x = 0; x < row.Length; x++)
                {
                    Console.Write(CreateItemForDisplay(x, row[x], x == row.Length - 1));
                }
                Console.WriteLine();
            }
        }

        private string CreateItemForDisplay(int columnIndex, string data, bool isLastValue)
        {

            var columnSpacing = CreateColumnSpacing();
            var borderString = _configuration.DisplayColumnBorders ? _configuration.ColumnDividerChar : '\0';
            var (beforeSpace, afterSpace) = CreateRequiredFillSpacing(columnIndex, data);

            var displayData = $"{borderString}{columnSpacing}{beforeSpace}{data}{afterSpace}{columnSpacing}";

            return isLastValue ? displayData + borderString : displayData;

        }

        private string CreateColumnSpacing()
        {
            return String.Concat(Enumerable.Repeat(" ", (int)Math.Ceiling(_configuration.ColumnSpacing / (double)2)));
        }

        private string CreateRowNumberBuffer()
        {
            var bufferSize = _configuration.DisplayRowNumbers ? _numberOfRows.ToString().Length : 0;
            return String.Concat(Enumerable.Repeat(" ", bufferSize));
        }

        private (string, string) CreateRequiredFillSpacing(int columnIndex, string data)
        {
            var leftoverSpace = _requiredColumnWidths[columnIndex] - data.Length;
            var numberOfSpaces = Math.Ceiling((double)leftoverSpace / 2);

            var beforeSpace = String.Concat(Enumerable.Repeat(" ", (int)numberOfSpaces));
            var afterSpace = String.Concat(Enumerable.Repeat(" ", leftoverSpace % 2 == 0 ? (int)numberOfSpaces : (int)numberOfSpaces - 1));

            return (beforeSpace, afterSpace);
        }

        private string CreateHeaderDivider()
        {
            var totalColumnDividers = _configuration.DisplayColumnBorders ? Headers.Length + 1 : 0;
            var maxWidthOfColumns = _requiredColumnWidths.Sum(x => x.Value);
            var totalSpacing = _configuration.ColumnSpacing * Headers.Length;
            var lengthOfDivider = maxWidthOfColumns + totalSpacing + totalColumnDividers;

            var divider = String.Concat(CreateRowNumberBuffer(), String.Concat(Enumerable.Repeat(_configuration.HeaderDividerChar, lengthOfDivider)));
            return divider;
        }

        private void UpdateMaxColumnWidthValues(string[] data)
        {
            for(int x = 0; x < data.Length; x ++)
            {
                if(_requiredColumnWidths.ContainsKey(x))
                {
                    if (data[x].Length > _requiredColumnWidths[x])
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

        private void UpdateMaxColumnWidthValues(List<string[]> data)
        {
            foreach(var row in data)
            {
                UpdateMaxColumnWidthValues(row);
            }
        }


    }
}
