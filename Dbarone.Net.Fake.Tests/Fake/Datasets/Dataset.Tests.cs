using System.Linq;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class DatasetTests {

    [Fact]
    public void GetData()
    {
        // When
        var data = Dataset.GetData(DatasetEnum.Surnames_US_Census_2010);

        // Then
        Assert.Equal(1000, data.Count());
    }
}