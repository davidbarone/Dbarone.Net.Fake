namespace Dbarone.Net.Fake;

/// <summary>
/// Base abstract class for all classes implementing generic IRandom.
/// </summary>
/// <typeparam name="T">The type of random value to return.</typeparam>
public abstract class AbstractRandom<T> : IRandom<T>
{
    public ulong Seed { get; set; }

    public AbstractRandom()
    {
        this.Seed = 0;
    }

    public AbstractRandom(ulong seed)
    {
        this.Seed = seed;
    }

    public abstract T Next();
}