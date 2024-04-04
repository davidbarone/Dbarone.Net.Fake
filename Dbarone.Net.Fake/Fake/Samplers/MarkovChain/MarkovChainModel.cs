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
    /// Stores the frequencies that each n-gram is seen across the entire corpus
    /// </summary>
    public Dictionary<string[], int> Occurences { get; set; } = new Dictionary<string[], int>();

    /// <summary>
    /// The transition matrix. Defines the current states, and the next possible states with the corresponding frequency expressed as a value between 0 and 1.
    /// </summary>
    public Dictionary<string[], Dictionary<string, double>> Matrix = new Dictionary<string[], Dictionary<string, double>>(new StringArrayEqualityComparer());

    /// <summary>
    /// Serialiases the current model to json string.
    /// </summary>
    /// <returns>The model serialiased as a json string.</returns>
    public string Serialise() {
        var ms = new MemoryStream();
        var ser = new System.Xml.Serialization.XmlSerializer(typeof(MarkovChainModel));
        ser.Serialize(ms, this);
        var sr = new StreamReader(ms);
        return sr.ReadToEnd();
        //return JsonSerializer.Serialize(this);
    }

    /// <summary>
    /// Creates a new MarkovChainModel from a json string.
    /// </summary>
    /// <param name="json">The json string.</param>
    /// <returns>Returns a MarkovChainModel instance.</returns>
    public MarkovChainModel Deserialise(string json) {
        return JsonSerializer.Deserialize<MarkovChainModel>(json);
    } 
}