namespace Dbarone.Net.Fake;

/// <summary>
/// Represents a transition matrix for a Markov chain model.
/// The public property is a List to allow simpler serialisation / deserialisation.
/// A private dictionary is also implemented to allow faster retrieval of rows by key.
/// </summary>
public class StochasticMatrix {

    private Dictionary<string[], StochasticMatrixRow> dictionary = new Dictionary<string[], StochasticMatrixRow>(new StringArrayEqualityComparer());

    /// <summary>
    /// The rows in the matrix.
    /// </summary>
    public List<StochasticMatrixRow> Rows { get; set; } = new List<StochasticMatrixRow>();

    public void AddRow(StochasticMatrixRow row) {
        dictionary.Add(row.CurrentState, row);
        Rows.Add(row);
    }

    public StochasticMatrixRow GetRow(string[] key){
        if (Exists(key)){
            return dictionary[key];
        } else {
            throw new Exception("Key does not exist.");
        }
    }

    public StochasticMatrixRow? GetRowOrDefault(string[] key){
        if (Exists(key)){
            return dictionary[key];
        } else {
            return null;
        }
    }

    public bool Exists(string[] key) {
        return dictionary.ContainsKey(key);
    }
}