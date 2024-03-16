using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

/// <summary>
/// Linear congruential generator.
/// </summary>
public class Lcg : IRandom
{
    public int Seed { get; set; }

    public LcgParams Parameters { get; set; } = LcgParams.Create(LcgParamsEnum.ZX81);

    // See: https://en.wikipedia.org/wiki/Linear_congruential_generator
    public double Next()
    {
        this.Seed = (Parameters.A * this.Seed + Parameters.C) % Parameters.M;
        return (double)this.Seed / this.Parameters.M;
    }
}