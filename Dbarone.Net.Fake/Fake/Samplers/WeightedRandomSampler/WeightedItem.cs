namespace Dbarone.Net.Fake;

/// <summary>
/// A weighted item containing a weight and a value.
/// </summary>
/// <typeparam name="T">The type of the item. The type must implement the IWeightedItem interface.</typeparam>
public class WeightedItem<T> : IWeightedItem
{
    /// <summary>
    /// The value.
    /// </summary>
    public T Value { get; set; } = default!;

    /// <summary>
    /// The relative weighting or frequency allocated to this value.
    /// </summary>
    public double Weight { get; set; }

    public WeightedItem(IDictionary<string, object> data, Func<IDictionary<string, object>, T> mapper)
    {
        this.Value = mapper(data);
        this.Weight = double.Parse((string)data["Weight"]);
    }

    /// <summary>
    /// Creates a new WeightedItem instance. 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="weight"></param>
    public WeightedItem(T value, double weight)
    {
        this.Value = value;
        this.Weight = weight;
    }
}