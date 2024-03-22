using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Dbarone.Net.Fake;
using Xunit;

public class WilliamFakespearStrategyTests
{

    [Fact]
    public void TestWilliamFakespearStrategy()
    {
        WilliamFakespearStrategy strat = new WilliamFakespearStrategy();
        strat.Order = 2;
        strat.CreateModel();
        List<string> results = new List<string>();
        for (int i = 0; i < 1000; i++)
        {
            results.Add(strat.Next(i, null));
        }
        var b = results;
    }
}