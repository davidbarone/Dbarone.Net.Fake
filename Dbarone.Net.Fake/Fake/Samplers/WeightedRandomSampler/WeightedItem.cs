namespace Dbarone.Net.Fake;

/// <summary>
/// A weighted item containing a weight and a value.
/// </summary>
public class WeightedItem<T> : IWeightedItem {

    /// <summary>
    /// The value.
    /// </summary>
    public T Value { get; set; } = default!;
    
    /// <summary>
    /// The relative weighting or frequency allocated to this value.
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Ctor to create a new WeightedItem from a dictionary record.
    /// </summary>
    /// <param name="dictionary"></param>
    public WeightedItem(IDictionary<string, object> data, Func<IDictionary<string, object>, T> mapper) {
        this.Value = mapper(data);
        this.Weight = double.Parse((string)data["Weight"]);
    }
}