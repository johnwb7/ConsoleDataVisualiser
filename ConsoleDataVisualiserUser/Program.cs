using System;
using System.Collections.Generic;
using ConsoleDataVisualiser.Table;

namespace ConsoleDataVisualiserUser
{
    class Program
    {
        static void Main(string[] args)
        {
            var headers = new string[] { "Col 1", "Col 2", "Col 3" };
            var data = new List<object[]>(){ new object[] { 1, 2, 3 } };
            var config = TableConfiguration.Create()
                            .WithHeaderDivider(true)
                            .WithHeaderSpacing(5);

            ConsoleTable.Create()
                .WithHeaders(headers)
                .WithData(data)
                .WithConfiguration(config)
                .PrintTable();
        }
    }
}
