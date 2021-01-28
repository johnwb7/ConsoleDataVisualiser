using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ConsoleDataVisualiser.Table;
using System.Linq;

namespace ConsoleDataVisualiser.Tests.Table
{
    public class ConsoleTableTests
    {
        private string[] _headers = new string[] { "Column 1", "Column 2", "Column 3" };

        [Test]
        public void AddRow_WhereRowIsGenericAddsSuccessfully()
        {
            var testRow = new int[] { 1, 2, 3 };
            var expectedRow = new string[] { "1", "2", "3" };

            var table = ConsoleTable.Create().AddDataRow<int>(testRow);
            var lastRow = table.Data.Last();

            Assert.That(lastRow, Is.EqualTo(expectedRow));
        }

        [Test]
        public void AddRow_WhereRowIsStringArrayAddsSuccessfully()
        {
            var testRow = new string[] { "1", "2", "3" };

            var table = ConsoleTable.Create().AddDataRow<string>(testRow);
            var lastRow = table.Data.Last();

            Assert.That(lastRow, Is.EqualTo(testRow));
        }

        [Test]
        public void AddRow_WhereDataImplementsIRowConvertableAddsSuccessfully()
        {
            var rowConvertable = TestRowConvertable.Create();
            var expectedData = rowConvertable.MapToRow();

            var table = ConsoleTable.Create().AddDataRow(rowConvertable);
            var lastRow = table.Data.Last();

            Assert.That(lastRow, Is.EqualTo(expectedData));
        }
        [Test]
        public void AddingTwoSetsOfHeadersThrowsException()
        {
            Assert.Throws(typeof(Exception),
                delegate () 
                { 
                    ConsoleTable.Create().WithHeaders(_headers).WithHeaders(_headers); 
                });
        }
    }
}
