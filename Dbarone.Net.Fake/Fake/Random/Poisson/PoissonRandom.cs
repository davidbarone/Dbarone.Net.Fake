
using Dbarone.Net.Fake;

/// <summary>
/// Generates random Poisson-distributed numbers. Algorithm provided by Knuth.
/// Each random number represents how many events occur in the given period of time, based on the expected rate (Lambda).
/// https://en.wikipedia.org/wiki/Poisson_distribution
/// </summary>
public class PoissonRandom : AbstractRandom<int>, IRandom<int>
{
    /// <summary>
    /// The expected rate of occurence in a given time frame.
    /// </summary>
    public double Lambda { get; set; }

    private IRandom<double> Random { get; set; } = default!;

    public PoissonRandom(double expectedRate, ulong seed) : base(seed)
    {
        this.Lambda = expectedRate;
        this.Random = new Lcg(seed);
    }

    public PoissonRandom(double expectedRate) : base()
    {
        this.Lambda = expectedRate;
        this.Random = new Lcg();
    }

    /// <summary>
    /// Generates random Poisson-distributed number.
    /// Algorithm attributed to Knuth: https://en.wikipedia.org/wiki/Poisson_distribution#Generating_Poisson-distributed_random_variables
    /// </summary>
    /// <returns></returns>
    public override int Next()
    {
        var L = Math.Exp(-Lambda);
        int k = -1;
        double p = 1;

        do
        {
            k++;
            p *= Random.Next();
        } while (p > L);

        return k;
    }
}