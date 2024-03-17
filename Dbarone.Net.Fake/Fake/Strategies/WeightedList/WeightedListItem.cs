namespace Dbarone.Net.Fake;

/// <summary>
/// A manually created weighted list item.
/// </summary>
public class WeightedListItem {

    /// <summary>
    /// The value or label.
    /// </summary>
    public string Value { get; set; } = default!;
    
    /// <summary>
    /// The relative weighting or frequency allocated to this value.
    /// </summary>
    public int Weight { get; set; }
}