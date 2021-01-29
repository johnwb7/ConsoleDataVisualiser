using System;
using System.Collections.Generic;
using ConsoleDataVisualiser.Table;

namespace ConsoleDataVisualiserUser
{
    class Program
    {
        static void Main(string[] args)
        {
            var headers = new string[] { "C1", "C2", "C3" };
            var data = new List<string[]>(){ 
                                            new string[] { "Mark Ramprakash", "40", "Some house" }, 
                                            new string[] { "Kumar Sangakkarra", "39", "Another house" }, 
                                            new string[] { "Moeen Ali", "33", "A house" } };

            var data2 = new List<int[]>()
            {
                new int[] {111, 222, 333 },
                new int[] {444, 555, 6666 },
                new int[] {777, 888, 999 }
            };
            
            var config = TableConfiguration.Create()
                            .WithHeaderDivider()
                            .WithColumnBorders()
                            .WithRowNumbers()
                            .WithColumnSpacing(5);

            ConsoleTable.Create()
                .WithHeaders(headers)
                .WithData(data)
                .WithConfiguration(config)
                .PrintTable();

            // To Do: Exception if too many row fields. Move all required tracking stats into one method call. Tests
        }
    }
}
