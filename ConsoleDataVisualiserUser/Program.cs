using System;
using System.Collections.Generic;
using ConsoleDataVisualiser.Table;

namespace ConsoleDataVisualiserUser
{
    class Program
    {
        static void Main(string[] args)
        {
            var headers = new string[] { "Col 1", "Col 2", "Column 3" };
            var data = new List<int[]>(){ new int[] { 1, 2387768, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } };
            var config = TableConfiguration.Create()
                            .WithHeaderDivider(true)
                            .WithColumnSpacing(5);

            ConsoleTable.Create()
                .WithHeaders(headers)
                .WithData(data)
                .WithConfiguration(config)
                .PrintTable();
        }
    }
}
