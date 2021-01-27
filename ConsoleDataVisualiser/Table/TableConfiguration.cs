using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDataVisualiser.Table
{
    public class TableConfiguration
    {
        internal bool DisplayRowNumbers { get; set; } = false;
        internal bool DisplayColumnBorders { get; set; } = false;
        internal bool DisplayHeaderDivider { get; set; } = false;
        internal int HeaderSpacing { get; set; } = 2;
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

        public TableConfiguration WithColumnBorders(bool displayColumnBorders)
        {
            DisplayColumnBorders = displayColumnBorders;
            return this;
        }

        public TableConfiguration WithHeaderDivider(bool displayHeaderDivider)
        {
            DisplayHeaderDivider = displayHeaderDivider;
            return this;
        }

        public TableConfiguration WithHeaderSpacing(int headerSpacing)
        {
            HeaderSpacing = headerSpacing;
            return this;
        }
    }

}
