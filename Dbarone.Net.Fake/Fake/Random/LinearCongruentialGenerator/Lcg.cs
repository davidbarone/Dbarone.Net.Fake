using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace Dbarone.Net.Fake;

/// <summary>
/// Linear congruential generator. Pseudo-random number generator. Returns a double 0 <= x < 1.
/// </summary>
public class Lcg : AbstractRandom<double>
{
    public Lcg() : base()
    {
        this.Parameters = LcgParams.Create(LcgParamsEnum.ANSI_C);
    }

    public Lcg(LcgParamsEnum parameters) : base()
    {
        this.Parameters = LcgParams.Create(parameters);
    }

    public Lcg(LcgParamsEnum parameters, ulong seed) : base(seed)
    {
        this.Parameters = LcgParams.Create(parameters);
    }

    public LcgParams Parameters { get; set; } = LcgParams.Create(LcgParamsEnum.ANSI_C);

    // See: https://en.wikipedia.org/wiki/Linear_congruential_generator
    public override double Next()
    {
        this.Seed = (Parameters.A * this.Seed + Parameters.C) % Parameters.M;
        var output = this.Seed & Parameters.OutputMask;
        return (double)(output) / this.Parameters.M;
    }
}