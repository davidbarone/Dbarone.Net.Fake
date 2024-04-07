namespace Dbarone.Net.Fake;

/// <summary>
/// Base abstract class for all classes implementing generic ISampler interface.
/// </summary>
/// <typeparam name="T">The type of random value to return.</typeparam>
public abstract class AbstractSampler<T> : ISampler<T>
{
    public AbstractSampler() : this(new Lcg()) { }

    public AbstractSampler(IRandom<double> random)
    {
        this.Random = random;
    }

    public IRandom<double> Random { get; init; }

    public T Next()
    {
        throw new NotImplementedException();
    }
}
