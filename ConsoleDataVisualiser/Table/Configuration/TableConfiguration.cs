using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDataVisualiser.Table
{
    public class TableConfiguration
    {
        internal bool DisplayRowNumbers { get; private set; } = false;
        internal bool DisplayColumnBorders { get; private set; } = false;
        internal bool DisplayHeaderDivider { get; private set; } = true;
        internal int ColumnSpacing { get; private set; } = 2;

        internal char HeaderDividerChar { get; private set; } = '-';
        internal char ColumnDividerChar { get; private set; } = '|';

        public TableConfiguration()
        {

        }

        private TableConfiguration(bool displayRowNumbers, bool displayColumnBorders, bool displayHeaderDivider, int columnSpacing, char headerDividerChar, char columnDividerChar)
        {
            DisplayRowNumbers = displayRowNumbers;
            DisplayColumnBorders = displayColumnBorders;
            DisplayHeaderDivider = displayHeaderDivider;
            ColumnSpacing = columnSpacing;
            HeaderDividerChar = headerDividerChar;
            ColumnDividerChar = columnDividerChar;
        }

        public static TableConfiguration Minimal => new TableConfiguration(false, false, true, 2, '-', '|');
        public static TableConfiguration Fancy => new TableConfiguration(true, true, true, 6, '*', '|');

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
