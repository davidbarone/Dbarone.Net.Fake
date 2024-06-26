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
    [InlineData(LcgParamsEnum.ANSI_C)]
    public void LcgTestRandomness(LcgParamsEnum param)
    {
        for (ulong i = 0; i < 100; i++)
        {
            List<double> randoms = new List<double>();
            Lcg lcg = new Lcg(param, i);
            for (int j = 0; j<100; j++) {
                randoms.Add(lcg.Next());
            }
            Assert.InRange(randoms.Distinct().Count(), 98, 100); // at least 98 different values.
            Assert.All(randoms, item => Assert.InRange(item, 0, 1));
        }
    }
}