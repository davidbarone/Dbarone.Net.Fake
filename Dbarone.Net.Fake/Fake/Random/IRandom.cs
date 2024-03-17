namespace Dbarone.Net.Fake;

/// <summary>
/// Represents a class that implements a random generator.
/// </summary>
public interface IRandom<T> {

    /// <summary>
    /// Returns the next random value.
    /// </summary>
    /// <returns></returns>
    public T Next();

    /// <summary>
    /// The seed value.
    /// </summary>
    public long Seed { get; set; }
}