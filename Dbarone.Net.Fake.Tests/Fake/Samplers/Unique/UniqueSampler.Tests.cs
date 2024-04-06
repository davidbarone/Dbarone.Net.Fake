using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class UniqueSamplerTests
{

    [Fact]
    public void TestUniqueSampler()
    {

        var fooBarBasSampler = new WeightedRandomSampler<string>(
        new WeightedItem<string>("foo", 98),
        new WeightedItem<string>("bar", 1),
        new WeightedItem<string>("baz", 1)
    );

        UniqueSampler<string> uniqueSampler = new UniqueSampler<string>(fooBarBasSampler);

        List<string> samples = new List<string>();
        List<string> uniqueSamples = new List<string>();

        // Get 3 values for each:
        for (int i = 0; i < 3; i++)
        {
            samples.Add(fooBarBasSampler.Next());
            uniqueSamples.Add(uniqueSampler.Next());
        }

        Assert.True(samples.Distinct().Count() < 3);
        Assert.True(uniqueSamples.Distinct().Count() == 3);

        // Trying to get 1 more value from unique sampler should through error:
        Assert.Throws<Exception>(() => uniqueSampler.Next());
    }
}