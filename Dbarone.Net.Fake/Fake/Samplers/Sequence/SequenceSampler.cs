namespace Dbarone.Net.Fake;

/// <summary>
/// Generates a sequential list of integers. Can generate auto-identity or sequence numbers with naturally occuring gaps.
/// </summary>
public class SequenceSampler : AbstractSampler<int>, ISampler<int>
{
    #region Properties

    /// <summary>
    /// A number to determine the skip factor. For numbers x <= 1, next will generate a sequential list of integers with no gaps. For numbers x > 1, a gap will be generated.
    /// </summary>
    public double SkipFactor { get; set; }

    /// <summary>
    /// Starting number.
    /// </summary>
    public int Start { get; set; } = 0;

    /// <summary>
    /// Previous number.
    /// </summary>
    private int? Previous { get; set; } = null;

    #endregion

    #region Ctor

    public SequenceSampler(int start = 1, double skipFactor = 1) : base()
    {
        this.Start = start;
        this.SkipFactor = skipFactor;
    }

    public SequenceSampler(IRandom<double> random, int start = 1, double skipFactor = 1) : base(random)
    {
        this.Start = start;
        this.SkipFactor = skipFactor;
    }

    #endregion

    #region Methods

    public int Next()
    {
        Previous = (Previous == null) ? Start : Previous + 1;
        while (Random.Next() * SkipFactor >= 1)
        {
            Previous++;
        }
        return Previous.Value;
    }

    #endregion
}