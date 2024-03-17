namespace Dbarone.Net.Fake;

/// <summary>
/// Base abstract class for all classes implementing generic IRandom.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AbstractRandom<T> : IRandom<T>
{
    public long Seed { get; set; }

    public AbstractRandom()
    {
        this.Seed = 0;
    }

    public AbstractRandom(long seed)
    {
        this.Seed = seed;
    }

    public abstract T Next();
}