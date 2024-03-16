/// <summary>
/// Implementation of Box-Muller Transform. Generates pairs of independent, standard, normally distributed random numbers.
/// see: https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
/// </summary>
public class BoxMullerTransform : IRandom
{
    public int Seed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public double Mean { get; set; }
    public double StdDev { get; set; }
    public IRandom Random { get; set; } = new Lcg();

    public double Next()
    {
        const double two_pi = 2.0 * Math.PI;
        double u1, u2;
        do
        {
            u1 = Random.Next();
        } while (u1 == 0);
        u2 = Random.Next();

        // Compute Z1 and Z2
        double mag = this.StdDev * Math.Sqrt(-2.0 * Math.Log(u1));
        double z0 = mag * Math.Cos(two_pi * u2) + this.Mean;
        double z1 = mag * Math.Sin(two_pi * u2) + this.Mean;    // throw this one away
        return z0;
    }
}