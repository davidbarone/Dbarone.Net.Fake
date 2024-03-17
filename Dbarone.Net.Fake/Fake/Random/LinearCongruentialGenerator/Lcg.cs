using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace Dbarone.Net.Fake;

/// <summary>
/// Linear congruential generator. Pseudo-random number generator.
/// </summary>
public class Lcg : AbstractRandom<double>
{
    public Lcg() : base()
    {
        this.Parameters = LcgParams.Create(LcgParamsEnum.ZX81);
    }

    public Lcg(LcgParamsEnum parameters) : base()
    {
        this.Parameters = LcgParams.Create(parameters);
    }

    public Lcg(LcgParamsEnum parameters, long seed) : base(seed)
    {
        this.Parameters = LcgParams.Create(parameters);
    }

    public LcgParams Parameters { get; set; } = LcgParams.Create(LcgParamsEnum.ZX81);

    // See: https://en.wikipedia.org/wiki/Linear_congruential_generator
    public override double Next()
    {
        this.Seed = (Parameters.A * this.Seed + Parameters.C) % Parameters.M;
        return (double)this.Seed / this.Parameters.M;
    }
}