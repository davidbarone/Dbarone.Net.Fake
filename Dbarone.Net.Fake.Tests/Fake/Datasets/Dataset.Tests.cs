using System.Linq;
using Xunit;

public class DatasetTests {

    [Fact]
    public void GetData()
    {
        // When
        var data = Dataset.GetData(DatasetEnum.en_GB_Postcode_Districts);

        // Then
        Assert.Equal(6705, data.Count());
    }
}