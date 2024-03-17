namespace Dbarone.Net.Fake;

/// <summary>
/// Defines commonly used parameter-sets to initialise the linear congruential generator.
/// Values can be found at: https://en.wikipedia.org/wiki/Linear_congruential_generator
/// </summary>
public enum LcgParamsEnum
{
    /// <summary>
    /// Modulus = 2^16 + 1, multiplier = 75, increment = 74
    /// </summary>
    ZX81,

    /// <summary>
    /// Modulus = 2^32, multiplier = 1664525, increment = 1013904223
    /// </summary>
    Knuth_Numerical_Recipes,

    /// <summary>
    /// Modulus = 2^31, multiplier = 22695477, increment = 1
    /// </summary>
    Borland_C,

    /// <summary>
    /// Modulus = 2^31, multiplier = 1103515245, increment = 12345
    /// </summary>
    glibc,

    /// <summary>
    /// Modulus = 2^31, multiplier = 1103515245, increment = 12345
    /// </summary>
    ANSI_C,

    /// <summary>
    /// Modulus = 2^32, multiplier = 134775813, increment = 1
    /// </summary>
    Borland_Delphi,

    /// <summary>
    /// Modulus = 2^32, multiplier = 134775813, increment = 1
    /// </summary>
    Turbo_Pascal,

    /// <summary>
    /// Modulus = 2^31, multiplier = 214013 , increment  = 2531011 
    /// </summary>
    Microsoft_Visual_C,
 
    /// <summary>
    /// Modulus = 2^24, multiplier = 16598013  , increment = 12820163
    /// </summary>
    Microsoft_Visual_Basic,

    /// <summary>
    /// Modulus = 2^48, multiplier = 25214903917   , increment = 11
    /// </summary>
    POSIX
}