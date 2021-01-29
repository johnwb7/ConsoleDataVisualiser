using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDataVisualiser.Table
{
    public class TableConfiguration
    {
        internal bool DisplayRowNumbers { get; private set; } = false;
        internal bool DisplayColumnBorders { get; private set; } = false;
        internal bool DisplayRowBorders { get; private set; } = false;
        internal bool DisplayHeaderDivider { get; private set; } = true;
        internal int ColumnSpacing { get; private set; } = 2;

        internal char HeaderDividerChar { get; private set; } = '-';
        internal char ColumnBorderChar { get; private set; } = '|';
        internal char RowBorderChar { get; private set; } = '-';

        public TableConfiguration()
        {

        }

        private TableConfiguration(bool displayRowNumbers, bool displayColumnBorders, bool displayRowBorders, bool displayHeaderDivider, int columnSpacing, char headerDividerChar, char columnBorderChar, char rowBorderChar)
        {
            DisplayRowNumbers = displayRowNumbers;
            DisplayColumnBorders = displayColumnBorders;
            DisplayRowBorders = displayRowBorders;
            DisplayHeaderDivider = displayHeaderDivider;
            ColumnSpacing = columnSpacing;

            HeaderDividerChar = headerDividerChar;
            ColumnBorderChar = ColumnBorderChar;
            RowBorderChar = rowBorderChar;
        }

        public static TableConfiguration Minimal => new TableConfiguration(false, false, false, true, 2, '-', '|', '-');
        public static TableConfiguration Fancy => new TableConfiguration(true, true, true, true, 6, '*', '|', '-');

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
            ColumnBorderChar = dividerSymbol;
            DisplayColumnBorders = true;
            return this;
        }

        public TableConfiguration WithRowBorders(char borderSymbol = '-')
        {
            RowBorderChar = borderSymbol;
            DisplayRowBorders = true;
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
