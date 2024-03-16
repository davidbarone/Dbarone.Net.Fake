/// <summary>
/// Implementation of Box-Muller Transform. Generates pairs of independent, standard, normally distributed random numbers.
/// see: https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
/// </summary>
public class BoxMullerTransform : IRandom
{
    public int Seed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public double Next()
    {
        throw new NotImplementedException();
    }
}