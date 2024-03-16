/// <summary>
/// Represents a class that implements a random number generator.
/// </summary>
public interface IRandom {

    /// <summary>
    /// Returns a pseudo-random number greater than or equal to 0.0, and less than 1.0.
    /// </summary>
    /// <returns></returns>
    public double Next();

    /// <summary>
    /// Initial seed to generate next random number.
    /// </summary>
    public int Seed { get; set; }
}