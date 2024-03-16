using System.Reflection.Metadata.Ecma335;

/// <summary>
/// Linear congruential generator.
/// </summary>
public class LinearCongruentialGenerator : IRandom
{
    private int m, a, c;

    public LinearCongruentialGeneratorParams Parameters { get; set; } 

    // See: https://en.wikipedia.org/wiki/Linear_congruential_generator
    public int Next()
    {
        throw new NotImplementedException();
    }
}