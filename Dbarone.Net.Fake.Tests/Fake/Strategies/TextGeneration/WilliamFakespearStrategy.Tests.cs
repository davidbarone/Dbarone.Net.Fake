using System.Security.Cryptography.X509Certificates;
using Dbarone.Net.Fake;
using Xunit;

public class WilliamFakespearStrategyTests {
    
    [Fact]
    public void TestWilliamFakespearStrategy() {
        WilliamFakespearStrategy strat = new WilliamFakespearStrategy();
        strat.Order = 2;
        strat.CreateModel();
    }
}