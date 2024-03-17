using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class WeightedListStrategyTests
{

    [Fact]
    public void BoysNames()
    {
        List<string> names = new List<string>();
        WeightedListStrategy s = new WeightedListStrategy(WeightedListEnum.en_GB_Names_Boy);
        for (int i = 0; i < 100; i++)
        {
            var name = (string)s.Next(i);
            names.Add(name);
        }
        var aa = 0;
    }

    [Fact]
    public void GirlsNames()
    {
        List<string> names = new List<string>();
        WeightedListStrategy s = new WeightedListStrategy(WeightedListEnum.en_GB_Names_Girl);
        for (int i = 0; i < 100; i++)
        {
            var name = (string)s.Next(i);
            names.Add(name);
        }
        var aa = 0;
    }

    [Fact]
    public void SurnamesUSCensus2010()
    {
        List<string> names = new List<string>();
        WeightedListStrategy s = new WeightedListStrategy(WeightedListEnum.Surnames_US_Census_2010);
        for (int i = 0; i < 10; i++)
        {
            var name = (string)s.Next(i);
            names.Add(name);
        }
        var aa = 0;
    }

}