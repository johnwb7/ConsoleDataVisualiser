# ConsoleDataVisualiser

A tool for displaying data in the console in a neat and formatted fashion. Displays include the following:
- Console Table

## Console Table
ConsoleTable.cs displays given data in a configurable data table.

An instance of ConsoleTable is created by calling the static Create() method. The table can then be constructed with the following chained method calls:

#### WithHeaders()
This method sets the headers for the table. Headers are optional. It provides overloads for the following argument signatures:
- `WithHeaders(string[] headers)`

#### WithData()
Sets the data in the table. Note each row of data must be the same length, otherwise an Exception will be thrown. If headers have been provided, then the given length of a row must be the same length as the number of headers. The following overloads are available:
- `WithData(List<string[]> data)`
- `WithData(IEnumerable<object[]> data)`
- `WithData<T>(IEnumerable<T[]> data)`
- `WithData(IRowConvertable[] data)`
  
The `IRowConvertable` interface is discussed below.
  
#### AddDataRow()
Appends a new row to the data in the table. The length of the row must match the expected length of the table as defined by previous rows and headers. The following overloads are available:
- `AddDataRow(string[] row)`
- `AddDataRow<T>(T[] row)`
- `AddDataRow(IRowConvertable[] obj)`
- `AddDataRow(object[] row)`

#### WithConfiguration()
Sets the configuration of the table. If not provided, the configuration defaults to `TableConfiguration.Minimal`. 
- WithConfiguration(TableConfiguration config)

See the TableConfiguration section on how to customise a configuration and available out of the box configurations

#### PrintTable()
Prints the given data to the console to the specification as defined by the configuration

## IRowConvertableInterface
A class object can be provided as a row in the table providing if it implements the `IRowConvertable` interface, and implements the defined `MapToRow()` method. This method should map the desired properties of the class object to how the row should look in the table. For example:

```
public class MyObject : IRowConvertable
{
    public string PropertyOne = "One"
    public string PropertyTwo = "Two"
    public int PropertyThree = 3
    
    public string[] MapToRow()
    {
      return new string[]{PropertyOne, PropertyTwo, PropertyThree.ToString()}
    }
}
```
The row in the table will show as: | One | Two | 3 |


## TableConfiguration


