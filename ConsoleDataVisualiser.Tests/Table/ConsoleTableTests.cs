using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ConsoleDataVisualiser.Table;
using System.Linq;
using ConsoleDataVisualiser.Table.Configuration;

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

            var table = ConsoleTable.Create().AddDataRow(testRow);
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
        public void AddRow_WhereRowIsObjectArrayAddsSuccessfully()
        {

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

        [Test]
        public void AddingDataWithDifferentNumberOfColumnsThrowsException()
        {
            var rowOne = new string[] { "One", "Two", "Three" };
            var rowTwo = new string[] { "Four", "Five" };

            Assert.Throws(typeof(Exception),
                delegate ()
                {
                    ConsoleTable.Create().WithData(new List<string[]>() { rowOne, rowTwo });
                });
        }

        [Test]
        public void AddingTwoRowsSeparatelyWithDifferingNumbersOfColumnsThrowsException()
        {
            var rowOne = new string[] { "One", "Two", "Three" };
            var rowTwo = new string[] { "Four", "Five" };

            Assert.Throws(typeof(Exception),
                delegate ()
                {
                    ConsoleTable.Create().AddDataRow(rowOne).AddDataRow( rowTwo);
                });
        }

        [Test]
        public void SortByAscendingSortsCorrectly()
        {
            var rowOne = new string[] { "2", "Word", "Three" };
            var rowTwo = new string[] { "3", "Blah", "String" };
            var rowThree = new string[] { "1", "Earth", "Genau" };

            var data = new List<string[]>() { rowOne, rowTwo, rowThree };
            var expectedData = new List<string[]>() { rowThree, rowOne, rowTwo };

            var config = TableConfiguration.Create().SortByColumnIndex(0, SortBy.Ascending);

            var table = ConsoleTable.Create()
                            .WithData(data)
                            .WithConfiguration(config);

            table.PrintTable();
            Assert.That(table.Data, Is.EqualTo(expectedData));
        }

        [Test]
        public void SortByDescendingSortsCorrectly()
        {
            var rowOne = new string[] { "2", "Word", "Three" };
            var rowTwo = new string[] { "3", "Blah", "String" };
            var rowThree = new string[] { "1", "Earth", "Genau" };

            var data = new List<string[]>() { rowOne, rowTwo, rowThree };
            var expectedData = new List<string[]>() { rowTwo, rowOne, rowThree};

            var config = TableConfiguration.Create().SortByColumnIndex(0, SortBy.Descending);

            var table = ConsoleTable.Create()
                            .WithData(data)
                            .WithConfiguration(config);

            table.PrintTable();
            Assert.That(table.Data, Is.EqualTo(expectedData));
        }

        [Test]
        public void NoSortByDoesNotSortData()
        {
            var rowOne = new string[] { "1", "Word", "Three" };
            var rowTwo = new string[] { "3", "Blah", "String" };
            var rowThree = new string[] { "2", "Earth", "Genau" };

            var data = new List<string[]>() { rowOne, rowTwo, rowThree };

            var config = TableConfiguration.Create();

            var table = ConsoleTable.Create()
                            .WithData(data)
                            .WithConfiguration(config);
            
            table.PrintTable();
            Assert.That(table.Data, Is.EqualTo(data));
        }

        [Test]
        public void SortByInvalidColumnIndexThrowsException()
        {
            var rowOne = new string[] { "1", "Word", "Three" };
            var rowTwo = new string[] { "3", "Blah", "String" };
            var rowThree = new string[] { "2", "Earth", "Genau" };

            var data = new List<string[]>() { rowOne, rowTwo, rowThree };

            var config = TableConfiguration.Create().SortByColumnIndex(3, SortBy.Ascending);

            Assert.Throws(typeof(Exception),
                delegate ()
                {
                    ConsoleTable.Create()
                    .WithData(data)
                    .WithConfiguration(config)
                    .PrintTable();
                });
        }
    }
}
