using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDataVisualiser.Table
{
    public class TableConfiguration
    {
        internal bool DisplayRowNumbers { get; set; } = false;
        internal bool DisplayColumnBorders { get; set; } = false;
        internal bool DisplayHeaderDivider { get; set; } = true;
        internal int ColumnSpacing { get; set; } = 2;

        internal char HeaderDividerChar = '-';
        internal char ColumnDividerChar = '|';
        public static TableConfiguration Default => new TableConfiguration();

        public TableConfiguration()
        {

        }

        public static TableConfiguration Create()
        {
            return new TableConfiguration();
        }

        public TableConfiguration WithRowNumbers(bool displayRowNumbers)
        {
            DisplayRowNumbers = displayRowNumbers;
            return this;
        }

        public TableConfiguration WithColumnBorders(bool displayColumnBorders, char dividerSymbol = '|')
        {
            ColumnDividerChar = dividerSymbol;
            DisplayColumnBorders = displayColumnBorders;
            return this;
        }

        public TableConfiguration WithHeaderDivider(bool displayHeaderDivider, char dividerSymbol = '-')
        {
            HeaderDividerChar = dividerSymbol;
            DisplayHeaderDivider = displayHeaderDivider;
            return this;
        }

        public TableConfiguration WithColumnSpacing(int columnSpacing)
        {
            ColumnSpacing = columnSpacing;
            return this;
        }
    }

}
