namespace Dbarone.Net.Fake;

/// <summary>
/// Generates a sequential list of integers.
/// </summary>
public class SequenceStrategy
{

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

    public object Next(int i, object? last = null)
    {
        int n = last == null ? Start : (int)last + 1;
        while (Random.Next() * SkipFactor >= 1)
        {
            n++;
        }
        return n;
    }
}