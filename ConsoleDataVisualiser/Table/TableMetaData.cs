using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDataVisualiser.Table
{
    internal class TableMetaData
    {
        internal int NumberOfRows { get; set; }
        internal int NumberOfColumns { get; set; }
        internal Dictionary<int, int> RequiredColumnWidths { get; set; }

        public TableMetaData()
        {
            NumberOfRows = 0;
            NumberOfColumns = 0;
            RequiredColumnWidths = new Dictionary<int, int>();
        }

        public static TableMetaData Create()
        {
            return new TableMetaData();
        }

        internal void UpdateTableMetaData(string[] row)
        {
            UpdateMaxColumnWidthValues(row);
            IncrementRowCount(1);
            UpdateNumberOfColumns(row.Length);
        }

        private void IncrementRowCount(int count)
        {
            NumberOfRows+= count;
        }

        private void UpdateNumberOfColumns(int count)
        {
            NumberOfColumns = count;
        }

        private void UpdateMaxColumnWidthValues(string[] data)
        {
            for (int x = 0; x < data.Length; x++)
            {
                if (RequiredColumnWidths.ContainsKey(x))
                {
                    if (data[x].Length > RequiredColumnWidths[x])
                    {
                        RequiredColumnWidths[x] = data[x].Length;
                    }
                }
                else
                {
                    RequiredColumnWidths[x] = data[x].Length;
                }
            }
        }

        private void UpdateMaxColumnWidthValues(List<string[]> data)
        {
            foreach (var row in data)
            {
                UpdateMaxColumnWidthValues(row);
            }
        }
    }
}
