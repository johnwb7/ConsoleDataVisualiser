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

        public TableConfiguration WithRowNumbers()
        {
            DisplayRowNumbers = true;
            return this;
        }

        public TableConfiguration WithColumnBorders(char dividerSymbol = '|')
        {
            ColumnDividerChar = dividerSymbol;
            DisplayColumnBorders = true;
            return this;
        }

        public TableConfiguration WithHeaderDivider(char dividerSymbol = '-')
        {
            HeaderDividerChar = dividerSymbol;
            DisplayHeaderDivider = true;
            return this;
        }

        public TableConfiguration WithColumnSpacing(int columnSpacing)
        {
            ColumnSpacing = columnSpacing % 2 == 0 ? columnSpacing : columnSpacing + 1;
            return this;
        }
    }

}
