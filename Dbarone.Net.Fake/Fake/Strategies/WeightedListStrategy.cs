namespace Dbarone.Net.Fake;

/// <summary>
/// Returns data based on a weighted list of values.
/// </summary>
public class WeightedListStrategy : IFakerStrategy
{
    public IRandom Random { get; set; } = new Lcg();

    public object Next(int i, object? last = null)
    {
        throw new NotImplementedException();
    }
}