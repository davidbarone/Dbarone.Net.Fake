namespace Dbarone.Net.Fake;

/// <summary>
/// Generates a sequential list of integers. Can generate auto-identity or sequence numbers with naturally occuring gaps.
/// </summary>
public class SequenceSampler : ISampler<int>
{
    public SequenceSampler(double skipFactor = 1)
    {
        this.SkipFactor = skipFactor;
    }

    /// <summary>
    /// A number to determine the skip factor. For numbers x <= 1, next will generate a sequential list of integers with no gaps. For numbers x > 1, a gap will be generated.
    /// </summary>
    public double SkipFactor { get; set; }

    /// <summary>
    /// Starting number.
    /// </summary>
    public int Start { get; set; } = 0;

    /// <summary>
    /// The random number generator.
    /// </summary>
    public IRandom<double> Random { get; set; } = new Lcg();

    private int? Last { get; set; } = null;

    public int Next()
    {
        Last = (Last == null) ? Start : Last + 1;
        while (Random.Next() * SkipFactor >= 1)
        {
            Last++;
        }
        return Last.Value;
    }
}