namespace Dbarone.Net.Fake;

/// <summary>
/// Interface for a samplter algorithm that uses a random number generator to implement a particular random sampler.
/// </summary>
/// <typeparam name="T">The type of data returned by the sampler.</typeparam>
public interface ISampler<T>
{
    /// <summary>
    /// The random number generator to use in the sampler.
    /// </summary>
    IRandom<double> Random { get; set; }

    /// <summary>
    /// The next value to return
    /// </summary>
    /// <param name="i"></param>
    /// <param name="last"></param>
    /// <returns></returns>
    T Next(int i, T? last = default!);
}