using System.Linq;
using Xunit;

public class DatasetTests {

    [Fact]
    public void GetData()
    {
        // When
        var data = Dataset.GetData(DatasetEnum.Surnames_US_Census_2010);

        // Then
        Assert.Equal(6705, data.Count());
    }
}