using System.Text.Json;

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
    /// The transition matrix. Defines the current states, and the next possible states with the corresponding frequency expressed as a value between 0 and 1.
    /// </summary>
    public StochasticMatrix Matrix { get; set; } = new StochasticMatrix();

    /// <summary>
    /// Serialiases the current model to json string.
    /// </summary>
    /// <returns>The model serialiased as a json string.</returns>
    public string Serialise()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    /// Creates a new MarkovChainModel from a json string.
    /// </summary>
    /// <param name="json">The json string.</param>
    /// <returns>Returns a MarkovChainModel instance.</returns>
    public static MarkovChainModel Deserialise(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            throw new Exception("Cannot deserialise model json.");
        }
        var result = JsonSerializer.Deserialize<MarkovChainModel>(json);
        if (result is null)
        {
            throw new Exception("Invalid deserialised result.");
        }
        else
        {
            return result;
        }
    }
}