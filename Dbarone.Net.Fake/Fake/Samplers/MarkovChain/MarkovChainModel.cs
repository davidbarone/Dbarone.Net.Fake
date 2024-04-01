namespace Dbarone.Net.Fake;

/// <summary>
/// Supplies the model for a Markov chain text generator.
/// </summary>
public class MarkovChainModel
{
    /// <summary>
    /// Number of states to be considered when generating the next state.
    /// 1 means only current state considered.
    /// 2 means current and previous states considered.
    /// </summary>
    public int Order { get; set; } = 1;

    /// <summary>
    /// Defines the n-gram unit within the model.
    /// </summary>
    public MarkovChainLevel Level { get; set; } = MarkovChainLevel.Word;

    /// <summary>
    /// Stores the frequencies that each n-gram is seen across the entire corpus
    /// </summary>
    public Dictionary<string[], int> Occurences { get; set; } = new Dictionary<string[], int>();

    /// <summary>
    /// The transition matrix. Defines the current states, and the next possible states with the corresponding frequency expressed as a value between 0 and 1.
    /// </summary>
    public Dictionary<string[], Dictionary<string, double>> Matrix = new Dictionary<string[], Dictionary<string, double>>(new StringArrayEqualityComparer());
}