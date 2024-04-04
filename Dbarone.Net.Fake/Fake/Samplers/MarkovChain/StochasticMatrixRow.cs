using System.Text.Json.Serialization;

namespace Dbarone.Net.Fake;

/// <summary>
/// Represents a row in a transition matrix for a Markov chain model.
/// Each row stores a single current state n-gram, with it's corresponding next values.
/// The sum of the probabilities of the next values equals 1.
/// </summary>
public class StochasticMatrixRow {
    
    [JsonPropertyName("s")]
    public string[] CurrentState { get; set; } = default!;
    
    [JsonPropertyName("c")]
    public int Occurences { get; set; }

    [JsonPropertyName("p")]
    public double Probability { get; set; }
    
    [JsonPropertyName("n")]
    public List<StochasticMatrixElement> NextElements { get; set; } = new List<StochasticMatrixElement>();
}