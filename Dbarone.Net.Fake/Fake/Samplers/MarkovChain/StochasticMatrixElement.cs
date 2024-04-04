using System.Text.Json.Serialization;

namespace Dbarone.Net.Fake;

/// <summary>
/// Represents the next states for a given starting state in a stochastic model.
/// </summary>
public class StochasticMatrixElement
{
    /// <summary>
    /// The (next) state.
    /// </summary>
    [JsonPropertyName("s")]
    public string NextState { get; set; } = default!;
    
    /// <summary>
    /// The number of occurences of this value.
    /// </summary>
    [JsonPropertyName("c")]
    public int Occurences { get; set; }

    /// <summary>
    /// The probability of this value occurring.
    /// </summary>
    [JsonPropertyName("p")]
    public double Probability { get; set; }
}