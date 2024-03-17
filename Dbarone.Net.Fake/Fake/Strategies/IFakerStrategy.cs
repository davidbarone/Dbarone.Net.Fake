namespace Dbarone.Net.Fake;

/// <summary>
/// Interface for a strategy that uses a random number generator to return a specific random strategy.
/// </summary>
/// <typeparam name="T">The type returned by the strategy.</typeparam>
public interface IFakerStrategy<T>
{
    IRandom<double> Random { get; set; }

    T Next(int i, T? last = default!);
}