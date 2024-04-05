using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class PoissonTests
{

    [Fact]
    public void TestPoissonRandom()
    {
        int expectedRate = 10;
        int sampleSize = 10000;
        PoissonRandom rand = new PoissonRandom(expectedRate);

        List<int> numbers = new List<int>();
        for (int i = 0; i < sampleSize; i++)
        {
            numbers.Add(rand.Next());
        }

        // Check numbers are randomly poisson-distributed:
        var uniques = numbers.Distinct().OrderBy(n=>n).ToList();
        foreach (var unique in uniques)
        {
            var actualProbability = (double)numbers.Count(n => n == unique) / sampleSize;
            var calculatedProbability = (Math.Pow(expectedRate, unique) * Math.Pow(Math.E, -expectedRate)) / Factorial(unique);
            var variance = calculatedProbability - actualProbability;
            var pctVar = Math.Abs(variance / calculatedProbability);
            Assert.True(pctVar < 0.10 || calculatedProbability < 0.01); // Allow 10% tolerance. Ignore events with low probability
        }
    }

    [Fact]
    public void TestExponentialRandom() {
        double expectedRate = 1f/10;    // i.e. 1 event every 10 units of time.
        int sampleSize = 100000;
        ExponentialRandom rand = new ExponentialRandom(expectedRate);

        List<double> numbers = new List<double>();
        for (int i = 0; i < sampleSize; i++)
        {
            numbers.Add(rand.Next());
        }

        // The number generated should have mean of 10;
        var mean = numbers.Sum() / numbers.Count();
        Assert.InRange(mean, 9.9, 10.1);
    }

    private int Factorial(int number)
    {
        if (number <= 1)
        {
            return 1;
        }
        else
        {
            return number * Factorial(number - 1);
        }
    }
}
