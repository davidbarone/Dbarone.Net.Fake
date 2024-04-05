
using Dbarone.Net.Fake;

/// <summary>
/// Generates random number for a Poisson process. Each call to Next() returns how much time elapses between consecutive events.
/// https://en.wikipedia.org/wiki/Exponential_distribution
/// https://preshing.com/20111007/how-to-generate-random-timings-for-a-poisson-process/
/// </summary>
public class ExponentialRandom : AbstractRandom<double>, IRandom<double>
{
    /// <summary>
    /// The expected rate of occurence in a given time frame.
    /// </summary>
    public double Lambda { get; set; }

    private IRandom<double> Random { get; set; } = default!;

    public ExponentialRandom(double expectedRate, ulong seed) : base(seed)
    {
        this.Lambda = expectedRate;
        this.Random = new Lcg(seed);
    }

    public ExponentialRandom(double expectedRate) : base()
    {
        this.Lambda = expectedRate;
        this.Random = new Lcg();
    }

    /// <summary>
    /// Returns a random elapsed time between consecutive events.
    /// </summary>
    /// <returns></returns>
    public override double Next()
    {
        return -Math.Log(1.0 - Random.Next()) / Lambda;
    }
}