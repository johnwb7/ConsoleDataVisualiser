using System;
using System.Collections.Generic;
using ConsoleDataVisualiser;
using ConsoleDataVisualiser.Table;
using ConsoleDataVisualiser.Table.Configuration;

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
                                            new string[] { "Moeen Ali", "33", "A house" },
            };

            var data2 = new List<int[]>()
            {
                new int[] {111, 222, 333 },
                new int[] {444, 555, 6666 },
                new int[] {777, 888, 999 }
            };

            var myObjOne = new MyObject(1, 2, 3);
            var myObjTwo = new MyObject(4, 5, 6);
            var myObjThree = new MyObject(7, 8, 9);

            var objectData = new IRowConvertable[] { myObjOne, myObjThree, myObjTwo };

            var config = TableConfiguration.Create()
                            .WithHeaderDivider()
                            .WithColumnBorders()
                            .WithRowNumbers()
                            .WithRowBorders()
                            .WithColumnSpacing(5);

            ConsoleTable.Create()
                .WithHeaders(headers)
                .WithData(objectData)
                .WithConfiguration(config)
                .PrintTable();

        }
    }

    public class MyObject : IRowConvertable
    {
        public int PropOne { get; set; }
        public int PropTwo { get; set; }
        public int PropThree { get; set; }

        public MyObject(int one, int two, int three)
        {
            PropOne = one;
            PropTwo = two;
            PropThree = three;
        }

        public string[] MapToRow()
        {
            return new string[] { PropOne.ToString(), PropTwo.ToString(), PropThree.ToString() };
        }
    }

}
