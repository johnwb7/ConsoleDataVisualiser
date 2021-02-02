using ConsoleDataVisualiser.Table.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDataVisualiser.Table
{
    public class ConsoleTable
    {
        public string[] Headers { get; private set; }
        public List<string[]> Data { get; private set; }

        public TableConfiguration Configuration { get; private set; }
        private TableMetaData _metaData {get; set;}


        public static ConsoleTable Create()
        {
            return new ConsoleTable();
        }
        private ConsoleTable()
        {
            Headers = new string[0];
            Data = new List<string[]>();
            _metaData = TableMetaData.Create();
            Configuration = TableConfiguration.Minimal;
        }

        public ConsoleTable WithHeaders(string[] headers)
        {
            EnsureValidNumberOfColumns(headers.Length);
            if(Headers.Length > 0)
            {
                throw new Exception("Headers already set. Cannot set headers more than once.");
            }


            _metaData.UpdateTableMetaData(headers);
            Headers = headers;
            return this;
        }

        public ConsoleTable WithData(List<string[]> data)
        {
            data.ForEach(x =>
            {
                EnsureValidNumberOfColumns(x.Length);
                _metaData.UpdateTableMetaData(x);
            });

            Data = data;
            return this;
        }

        public ConsoleTable WithData(IRowConvertable[] data)
        {
            var formattedData = data.Select(obj =>
            {
                var row = obj.MapToRow();
                EnsureValidNumberOfColumns(row.Length);
                _metaData.UpdateTableMetaData(row);
                return row.ToArray();
            }).ToList();
            Data = formattedData;
            return this;
        }

        public ConsoleTable WithData(IEnumerable<object[]> data)
        {
            var formattedData = data.Select(row =>
            {
                var formattedRow = row.Select(x => x.ToString()).ToArray();
                EnsureValidNumberOfColumns(formattedRow.Length);
                _metaData.UpdateTableMetaData(formattedRow);
                return formattedRow;
            }).ToList();
  
            Data = formattedData;
            return this;
        }

        public ConsoleTable WithData<T>(IEnumerable<T[]> data)
        {
            var formattedData = data.Select(row =>
            {
                var formattedRow = row.Select(x => x.ToString()).ToArray();
                EnsureValidNumberOfColumns(formattedRow.Length);
                _metaData.UpdateTableMetaData(formattedRow);
                return formattedRow;
            }).ToList();

            Data = formattedData;
            return this;
        }

        public ConsoleTable AddDataRow(string[] row)
        {
            EnsureValidNumberOfColumns(row.Length);
            _metaData.UpdateTableMetaData(row);

            Data.Add(row);
            return this;
        }

        public ConsoleTable AddDataRow(IRowConvertable obj)
        {
            var row = obj.MapToRow();
            EnsureValidNumberOfColumns(row.Length);
            _metaData.UpdateTableMetaData(row);

            Data.Add(row);
            return this;
        }

        public ConsoleTable AddDataRow<T>(T[] row)
        {
            var formattedData = row.Select(x => x.ToString()).ToArray();
            EnsureValidNumberOfColumns(formattedData.Length);
            _metaData.UpdateTableMetaData(formattedData);

            Data.Add(formattedData);
            return this;
        }

        public ConsoleTable AddDataRow(object[] row)
        {
            var formattedData = row.Select(x => x.ToString()).ToArray();
            EnsureValidNumberOfColumns(formattedData.Length);
            _metaData.UpdateTableMetaData(formattedData);

            Data.Add(formattedData);
            return this;
        }

        public ConsoleTable WithConfiguration(TableConfiguration config)
        {
            Configuration = config;
            return this;
        }

        public void PrintTable()
        {
            if (Headers.Length == 0 && Data.Count == 0) return;

            PrintHeaders();
            PrintData();
        }

        private void PrintHeaders()
        {
            if (!HasHeaders()) return;
            // Top Table Border
            if(Configuration.DisplayRowBorders)
            {
                Console.WriteLine(CreateRowBorder());
            }

            // Headers
            Console.Write(CreateRowNumberBuffer());
            for (var x = 0; x < Headers.Length; x++)
            {
                Console.Write(CreateItemForDisplay(x, Headers[x], x == Headers.Length - 1));
            }
            Console.WriteLine();

            // Header Divider
            if (Configuration.DisplayHeaderDivider)
            {
                Console.WriteLine(CreateHeaderDivider());
            }     
        }

        private void PrintData()
        {
            if(!HasHeaders() && Configuration.DisplayRowBorders)
            {
                Console.WriteLine(CreateRowBorder());
            }
            for(var y = 0; y < Data.Count; y++)
            {
                var row = Data[y];
                var displayRowNumber = Configuration.DisplayRowNumbers ? CreateRowNumber(y + 1) : "";

                // Row Number
                Console.Write(displayRowNumber);
                
                // Data
                for (var x = 0; x < row.Length; x++)
                {
                    Console.Write(CreateItemForDisplay(x, row[x], x == row.Length - 1));
                }
                Console.WriteLine();

                // Row Border
                if(Configuration.DisplayRowBorders)
                {
                    Console.WriteLine(CreateRowBorder());
                }
            }
        }

        private bool HasHeaders()
        {
            return Headers.Length > 0;
        }

        private string CreateRowNumber(int number)
        {
            var displayNumber = number.ToString();
            var lengthOfMaxRowNumber = _metaData.NumberOfRows.ToString().Length;
            var requiredSpacing = lengthOfMaxRowNumber - displayNumber.Length;

            return String.Concat(String.Concat(Enumerable.Repeat(" ", requiredSpacing)), displayNumber);
        }

        private string CreateItemForDisplay(int columnIndex, string data, bool isLastValue)
        {

            var columnSpacing = CreateColumnSpacing();
            var borderString = Configuration.DisplayColumnBorders ? Configuration.ColumnBorderChar : '\0';
            var (beforeSpace, afterSpace) = CreateRequiredFillSpacing(columnIndex, data);

            var displayData = $"{borderString}{columnSpacing}{beforeSpace}{data}{afterSpace}{columnSpacing}";

            return isLastValue ? displayData + borderString : displayData;

        }

        private string CreateColumnSpacing()
        {
            return String.Concat(Enumerable.Repeat(" ", (int)Math.Ceiling(Configuration.ColumnSpacing / (double)2)));
        }

        private string CreateRowNumberBuffer()
        {
            var bufferSize = Configuration.DisplayRowNumbers ? _metaData.NumberOfRows.ToString().Length : 0;
            return String.Concat(Enumerable.Repeat(" ", bufferSize));
        }

        private (string, string) CreateRequiredFillSpacing(int columnIndex, string data)
        {
            var leftoverSpace = _metaData.RequiredColumnWidths[columnIndex] - data.Length;
            var numberOfSpaces = Math.Ceiling((double)leftoverSpace / 2);

            var beforeSpace = String.Concat(Enumerable.Repeat(" ", (int)numberOfSpaces));
            var afterSpace = String.Concat(Enumerable.Repeat(" ", leftoverSpace % 2 == 0 ? (int)numberOfSpaces : (int)numberOfSpaces - 1));

            return (beforeSpace, afterSpace);
        }

        private string CreateHeaderDivider()
        {
            var lengthOfDivider = CalculateLengthofRow();
            return String.Concat(CreateRowNumberBuffer(), String.Concat(Enumerable.Repeat(Configuration.HeaderDividerChar, lengthOfDivider)));
        }

        private string CreateRowBorder()
        {
            var requiredLength = CalculateLengthofRow();
            return String.Concat(CreateRowNumberBuffer(), String.Concat(Enumerable.Repeat(Configuration.RowBorderChar, requiredLength)));
        }

        private int CalculateLengthofRow()
        {
            var totalColumnDividers = Configuration.DisplayColumnBorders ? _metaData.NumberOfColumns + 1 : 0;
            var maxWidthOfColumns = _metaData.RequiredColumnWidths.Sum(x => x.Value);
            var totalSpacing = Configuration.ColumnSpacing * _metaData.NumberOfColumns;

            var length = maxWidthOfColumns + totalSpacing + totalColumnDividers;
            return length;
        }

        public void EnsureValidNumberOfColumns(int numberOfColumns)
        {
            if (!IsValidNumberOfColumns(numberOfColumns))
            {
                throw new Exception($"Row does not fit the required number of columns: {_metaData.NumberOfColumns} " +
                    $"\n It is likely you have already set headers or a data row with a different length to {numberOfColumns}");
            }
        }

        private bool IsValidNumberOfColumns(int number)
        {
            return _metaData.NumberOfColumns == 0 ? true : number == _metaData.NumberOfColumns;
        }

        private void SortData()
        {
            if (Configuration.SortByMethod == SortBy.None)
            {
                return;
            }

            var sortByIndex = Configuration.ColumnSortByIndex;
            if(sortByIndex > _metaData.NumberOfColumns - 1)
            {
                throw new Exception("Invalid Sort By Index");
            }

            Data = Configuration.SortByMethod == SortBy.Ascending ?
                Data.OrderBy(x => x[sortByIndex]).ToList() :
                Data.OrderByDescending(x => x[sortByIndex]).ToList();
        }
    }
}
