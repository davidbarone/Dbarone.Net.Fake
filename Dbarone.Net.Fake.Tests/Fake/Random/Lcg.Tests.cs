using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class LcgTests
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

    [Theory]
    [InlineData(LcgParamsEnum.ZX81)]
    public void LcgCheckBetween0And1(LcgParamsEnum param)
    {
        for (int i = 0; i < 1000; i++)
        {
            List<double> randoms = new List<double>();
            Lcg lcg = new Lcg(param, i);
            for (int j = 0; j<1000; j++) {
                randoms.Add(lcg.Next());
            }
            Assert.Equal(1000, randoms.Distinct().Count()); // all 1000 must be different
            Assert.All(randoms, item => Assert.InRange(item, 0, 1));
        }
    }
}