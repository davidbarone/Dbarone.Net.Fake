namespace Dbarone.Net.Fake;

/// <summary>
/// List of initialisation parameters for linear congruent generator.
/// </summary>
public class LcgParams
{
    private static Dictionary<LcgParamsEnum, LcgParams> parameters = new Dictionary<LcgParamsEnum, LcgParams>{
        {LcgParamsEnum.ZX81, new LcgParams{
            M = (ulong)Math.Pow(2, 16),
            A = 75,
            C = 74,
            OutputMask = 0b1111111111111111111111111111111111111111111111111111111111111111}},
        {LcgParamsEnum.Knuth_Numerical_Recipes, new LcgParams{
            M = (ulong)Math.Pow(2, 32),
            A = 1664525,
            C = 1013904223,
            OutputMask = 0}},
        {LcgParamsEnum.Borland_C, new LcgParams{M = (ulong)Math.Pow(2, 31), A=22695477, C=1, OutputMask = 0}},
        {LcgParamsEnum.glibc, new LcgParams{
            M = (ulong)Math.Pow(2, 31),
            A = 1103515245,
            C = 12345,
            OutputMask = 0b0000000000000000000000000000000000111111111111111111111111111111}},
        {LcgParamsEnum.ANSI_C, new LcgParams{
            M = (ulong)Math.Pow(2, 31),
            A = 1103515245,
            C = 12345,
            OutputMask = 0b0000000000000000000000000000000001111111111111110000000000000000}},
        {LcgParamsEnum.Borland_Delphi, new LcgParams{M = (ulong)Math.Pow(2, 32), A=134775813, C=1, OutputMask = 0}},
        {LcgParamsEnum.Turbo_Pascal, new LcgParams{M = (ulong)Math.Pow(2, 32), A=134775813 , C=1, OutputMask = 0}},
        {LcgParamsEnum.Microsoft_Visual_C, new LcgParams{M = (ulong)Math.Pow(2, 31), A=214013 , C=2531011 , OutputMask = 0}},
        {LcgParamsEnum.Microsoft_Visual_Basic, new LcgParams{M = (ulong)Math.Pow(2, 24), A=16598013 , C=12820163 , OutputMask = 0}},
        {LcgParamsEnum.POSIX, new LcgParams{M = (ulong)Math.Pow(2, 48), A=25214903917 , C=11, OutputMask = 0}}
    };

    /// <summary>
    /// The modulus.
    /// </summary>
    public ulong M { get; set; }

    /// <summary>
    /// The multiplier.
    /// </summary>
    public ulong A { get; set; }

    /// <summary>
    /// The increment.
    /// </summary>
    public ulong C { get; set; }

    /// <summary>
    /// The output mask used to return the final result.
    /// </summary>
    public ulong OutputMask { get; set; }

    /// <summary>
    /// Creates a new LcgParams instance based on a type enum.
    /// </summary>
    /// <param name="type">The type enum to select the LcgParams to use.</param>
    /// <returns>Returns an LcgParams instance.</returns>
    public static LcgParams Create(LcgParamsEnum type)
    {
        return parameters[type];
    }
}