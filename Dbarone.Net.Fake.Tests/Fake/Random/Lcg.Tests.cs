using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class UnitTest1
{
    [Fact]
    public void LcgTest()
    {
        Lcg lcg = new Lcg();
        lcg.Seed = lcg.Parameters.M / 2;
        var initial = lcg.Seed;
        var i = 0;

        do
        {
            i++;
            var rand = lcg.Next();

        } while (lcg.Seed != initial && i <= 100000);

        var a = i;
    }
}