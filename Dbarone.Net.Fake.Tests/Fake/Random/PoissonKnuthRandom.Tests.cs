using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class PoissonKnuthRandomTests
{

    [Fact]
    public void TestPoissonknuthRandom()
    {
        int expectedRate = 10;
        int sampleSize = 10000;
        PoissonKnuthRandom rand = new PoissonKnuthRandom(expectedRate);

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
