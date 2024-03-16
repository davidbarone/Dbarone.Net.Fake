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
        WeightedListStrategy s = new WeightedListStrategy();
        s.List = WeightedListEnum.Names_Boy;
        for (int i = 0; i < 100; i++)
        {
            var name = (string)s.Next(i);
            names.Add(name);
        }
        var aa = 0;
    }
}