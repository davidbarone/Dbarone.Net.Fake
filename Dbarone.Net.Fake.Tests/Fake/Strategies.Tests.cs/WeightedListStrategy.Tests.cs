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
        WeightedListStrategy s = new WeightedListStrategy();
        s.List = WeightedListEnum.Names_Boy;
        var name = s.Next(0);
        var aa = 0;
    }

}