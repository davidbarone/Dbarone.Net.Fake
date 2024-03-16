using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class BoxMullerTests
{
    [Fact]
    public void BoxMuller()
    {
        BoxMullerTransform bm = new BoxMullerTransform();
        bm.Mean = 0.6;
        bm.StdDev = 0.1;    // 95% of values should be between 0.4 and 0.8;
        List<double> values = new List<double>();

        for (int i = 0; i < 10000; i++)
        {
            double value = bm.Next();
            values.Add(value);
        }

        // Get the Mean + StdDev of the values
        var mean = values.Average();
        var stdDev = this.stdDev(values);

        // Asserts
        Assert.InRange<double>(mean, 0.59, 0.61);
        Assert.InRange<double>(stdDev, 0.099, 0.101);
    }

    private double stdDev(IEnumerable<double> values)
    {
        double avg = values.Average();
        return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
    }
}