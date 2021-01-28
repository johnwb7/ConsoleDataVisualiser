using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDataVisualiser.Tests.Table
{
    class TestRowConvertable : IRowConvertable
    {
        private string propertyOne = "one";
        private int propertyTwo = 2;
        private double propertyThree = 3.0;

        public string[] MapToRow()
        {
            return new string[] { propertyOne, propertyTwo.ToString(), propertyThree.ToString() };
        }

        public static TestRowConvertable Create()
        {
            return new TestRowConvertable();
        }
    }
}
