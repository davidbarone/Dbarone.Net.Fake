namespace Dbarone.Net.Fake;

/// <summary>
/// Interface for a sampler algorithm that uses a random number generator to implement a particular random sampler.
/// </summary>
/// <typeparam name="T">The type of data returned by the sampler.</typeparam>
public interface ISampler<T>
{
    /// <summary>
    /// The random number generator to use in the sampler.
    /// </summary>
    IRandom<double> Random { get; init; }

    /// <summary>
    /// The next value to return.
    /// </summary>
    /// <returns>A value of type T.</returns>
    T Next();
}