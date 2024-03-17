namespace Dbarone.Net.Fake;

/// <summary>
/// List of initialisation parameters for linear congruent generator.
/// </summary>
public class LcgParams
{
    private static Dictionary<LcgParamsEnum, LcgParams> parameters = new Dictionary<LcgParamsEnum, LcgParams>{
        {LcgParamsEnum.ZX81, new LcgParams{M = (int)Math.Pow(2, 16), A=75, C=74}},
        {LcgParamsEnum.Knuth_Numerical_Recipes, new LcgParams{M = (int)Math.Pow(2, 32), A=1664525, C=1013904223}},
        {LcgParamsEnum.Borland_C, new LcgParams{M = (int)Math.Pow(2, 31), A=22695477, C=1}},
        {LcgParamsEnum.glibc, new LcgParams{M = (int)Math.Pow(2, 31), A=1103515245, C=12345}},
        {LcgParamsEnum.ANSI_C, new LcgParams{M = (int)Math.Pow(2, 31), A=1103515245, C=12345}},
        {LcgParamsEnum.Borland_Delphi, new LcgParams{M = (int)Math.Pow(2, 32), A=134775813, C=1}},
        {LcgParamsEnum.Turbo_Pascal, new LcgParams{M = (int)Math.Pow(2, 32), A=134775813 , C=1}},
        {LcgParamsEnum.Microsoft_Visual_C, new LcgParams{M = (int)Math.Pow(2, 31), A=214013 , C=2531011 }},
        {LcgParamsEnum.Microsoft_Visual_Basic, new LcgParams{M = (int)Math.Pow(2, 24), A=16598013 , C=12820163 }},
        {LcgParamsEnum.POSIX, new LcgParams{M = (int)Math.Pow(2, 48), A=25214903917 , C=11}}
    };

    /// <summary>
    /// The modulus.
    /// </summary>
    public long M { get; set; }

    /// <summary>
    /// The multiplier.
    /// </summary>
    public long A { get; set; }

    /// <summary>
    /// The increment.
    /// </summary>
    public long C { get; set; }

    public static LcgParams Create(LcgParamsEnum type)
    {
        return parameters[type];
    }
}