
using Dbarone.Net.Fake;

/// <summary>
/// Generates random Poisson-distributed numbers. Algorithm provided by Knuth. 
/// </summary>
public class PoissonKnuthRandom : AbstractRandom<int>, IRandom<int>
{
    /// <summary>
    /// The expected rate of occurence Must be integer > 0.
    /// </summary>
    public int Lambda { get; set; }

    private IRandom<double> Random { get; set; } = default!;

    public PoissonKnuthRandom(int expectedRate, ulong seed) : base(seed)
    {
        this.Lambda = expectedRate;
        this.Random = new Lcg(seed);
    }

    public PoissonKnuthRandom(int expectedRate) : base()
    {
        this.Lambda = expectedRate;
        this.Random = new Lcg();
    }

    /// <summary>
    /// Generates random Poisson-distributed number. Attribted to Knuth: https://en.wikipedia.org/wiki/Poisson_distribution#Generating_Poisson-distributed_random_variables
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