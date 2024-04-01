using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using Xunit;
namespace Dbarone.Net.Fake.Tests;

public class SequenceSamplerTests
{

    [Fact]
    public void Generate1000NoSkip()
    {
        List<int> ints = new List<int>();
        SequenceSampler ss = new SequenceSampler();
        ss.Start = 1000000;
        ss.SkipFactor = 0;
        int? s = null;

        for (int i = 0; i < 1000; i++)
        {
            s = (int)ss.Next(i, s);
            ints.Add(s.Value);
        }

        Assert.Equal(1000000, ints[0]);
        Assert.Equal(1000999, ints[999]);
        Assert.Equal(1000, ints.Distinct().Count());
    }

    [Fact]
    public void Generate1000WithSkip()
    {
        List<int> ints = new List<int>();
        SequenceSampler ss = new SequenceSampler();
        ss.Start = 1000000;
        ss.SkipFactor = 1.5;    // 50% of rows will have skipped number.
        int? s = null;

        for (int i = 0; i < 1000000; i++)
        {
            s = (int)ss.Next(i, s);
            ints.Add(s.Value);
        }

        Assert.Equal(1000000, ints[0]);
        Assert.Equal(1001468, ints[999]);   // approx 1000 * 150%
        Assert.Equal(1000, ints.Distinct().Count());
    }
}