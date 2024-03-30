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
    /// The transition matrix. Defines the current states, and the next possible states.
    /// </summary>
    public Dictionary<string[], Dictionary<string, double>> Matrix = new Dictionary<string[], Dictionary<string, double>>(new StringArrayEqualityComparer());

}