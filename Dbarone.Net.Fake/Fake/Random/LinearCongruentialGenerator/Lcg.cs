using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace Dbarone.Net.Fake;

/// <summary>
/// Linear congruential generator. Pseudo-random number generator. Returns a double value between 0 (inclusive) and 1 (exclusive).
/// </summary>
public class Lcg : AbstractRandom<double>
{
    /// <summary>
    /// Creates a new Lcg instance with a provided seed.
    /// </summary>
    /// <param name="seed">The initial seed.</param>
    public Lcg(ulong seed) : base(seed)
    {
        this.Parameters = LcgParams.Create(LcgParamsEnum.ANSI_C);
    }

    /// <summary>
    /// Creates a new Lcg instance with a default seed.
    /// </summary>
    public Lcg() : base(ulong.MaxValue / 2)
    {
        this.Parameters = LcgParams.Create(LcgParamsEnum.ANSI_C);
    }

    /// <summary>
    /// Creates a new Lcg instance using specific parameters to define the algorithm type.
    /// </summary>
    /// <param name="parameters">The parameters to use.</param>
    public Lcg(LcgParamsEnum parameters) : base(ulong.MaxValue / 2)
    {
        this.Parameters = LcgParams.Create(parameters);
    }

    /// <summary>
    /// Creates a new Lcg instance using specific parameters to define the algorithm type, and an initial seed.
    /// </summary>
    /// <param name="parameters">The parameters to use.</param>
    /// <param name="seed">The initial seed.</param>
    public Lcg(LcgParamsEnum parameters, ulong seed) : base(seed)
    {
        this.Parameters = LcgParams.Create(parameters);
    }

    /// <summary>
    /// The parameters for the Lcg instance. Defines the algorithm type used. defaults to the ANSI C implementation of the Lcg algorithm.
    /// </summary>
    public LcgParams Parameters { get; set; } = LcgParams.Create(LcgParamsEnum.ANSI_C);

    /// <summary>
    /// Gets the next random value.
    /// See: https://en.wikipedia.org/wiki/Linear_congruential_generator
    /// </summary>
    /// <returns>Returns the next random value.</returns>
    public override double Next()
    {
        this.Seed = (Parameters.A * this.Seed + Parameters.C) % Parameters.M;
        var output = this.Seed & Parameters.OutputMask;
        return (double)(output) / this.Parameters.M;
    }
}