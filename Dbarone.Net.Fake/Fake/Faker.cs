namespace Dbarone.Net.Fake;

/// <summary>
/// Main class that generates fake data.
/// </summary>
public class Faker
{
    private ulong Seed;
    private IRandom<double> Random { get; init; }
    public Faker(ulong seed)
    {
        this.Seed = seed;
        this.Random = new Lcg(seed);
    }

    #region Random Number Generators

    public IRandom<double> GetRandomContinuous()
    {
        return Random;
    }

    public IRandom<double> GetRandomStandard(double mean, double stdDev)
    {
        return new BoxMullerTransform(this.Random, mean, stdDev);
    }

    public IRandom<double> GetRandomExponential(double expectedRate)
    {
        return new ExponentialRandom(expectedRate, this.Seed);
    }

    public IRandom<int> GetRandomPoisson(double expectedRate)
    {
        return new PoissonRandom(expectedRate, this.Seed);
    }

    #endregion

    #region Samplers

    #endregion
}