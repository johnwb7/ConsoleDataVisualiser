using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDataVisualiser.Table
{
    public class TableConfiguration
    {
        public bool ShowRowNumbers { get; private set; } = false;
        public bool ShowColumnBorders { get; private set; } = false;
        public bool ShowHeaderDivider { get; private set; } = false;
        public static TableConfiguration Default => new TableConfiguration();
    }

}
