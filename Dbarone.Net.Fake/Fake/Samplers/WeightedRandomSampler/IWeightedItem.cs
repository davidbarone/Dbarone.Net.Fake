/// <summary>
/// Represents a type which can have a weight applied to it to affect its frequency of being selected using a weighted random sampler.
/// </summary>
public interface IWeightedItem {
    double Weight { get; set; }
}