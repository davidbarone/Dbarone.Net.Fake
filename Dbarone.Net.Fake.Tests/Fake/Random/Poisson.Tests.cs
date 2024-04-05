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
        int sampleSize = 100;
        PoissonRandom rand = new PoissonRandom(expectedRate);

        List<int> numbers = new List<int>();
        for (int i = 0; i < sampleSize; i++)
        {
            numbers.Add(rand.Next());
        }

        // Check: With a rate of 10 per time frame, and 100 time frames, a total of 1000 events should have occurred.
        var actualEvents = numbers.Sum();
        Assert.InRange(actualEvents, (expectedRate * sampleSize) * 0.95, (expectedRate * sampleSize) * 1.05);
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

        // The numbers generated should have mean of 10;
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
