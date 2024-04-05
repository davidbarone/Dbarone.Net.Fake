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
        WeightedRandomSampler<string> s = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.en_GB_Names_Boy)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])));

        for (int i = 0; i < 1000; i++)
        {
            var name = (string)s.Next();
            names.Add(name);
        }
        var mostCommonName = names.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count()).OrderByDescending(g => g.Value).First().Key;
        Assert.Equal("Noah", mostCommonName);
    }

    [Fact]
    public void GirlsNames()
    {
        List<string> names = new List<string>();
        WeightedRandomSampler<string> s = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.en_GB_Names_Girl)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])));

        for (int i = 0; i < 100; i++)
        {
            var name = (string)s.Next();
            names.Add(name);
        }
        var mostCommonName = names.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count()).OrderByDescending(g => g.Value).First().Key;
        Assert.Equal("Evelyn", mostCommonName);
    }

    [Fact]
    public void SurnamesUSCensus2010()
    {
        List<string> names = new List<string>();
        WeightedRandomSampler<string> s = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.Surnames_US_Census_2010)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])));

        for (int i = 0; i < 10; i++)
        {
            var name = (string)s.Next();
            names.Add(name);
        }
        var mostCommonName = names.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count()).OrderByDescending(g => g.Value).First().Key;
        Assert.Equal("CASTRO", mostCommonName);
    }
}