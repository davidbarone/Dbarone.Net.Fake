public class LcgParams
{

    private static Dictionary<LcgParamsEnum, LcgParams> parameters = new Dictionary<LcgParamsEnum, LcgParams>{
        {LcgParamsEnum.ZX81, new LcgParams{M = (int)Math.Pow(2, 16), A=75, C=74}}
    };

    /// <summary>
    /// The modulus.
    /// </summary>
    public int M { get; set; }

    /// <summary>
    /// The multiplier.
    /// </summary>
    public int A { get; set; }

    /// <summary>
    /// The increment.
    /// </summary>
    public int C { get; set; }

    public static LcgParams Create(LcgParamsEnum type)
    {
        return parameters[type];
    }
}