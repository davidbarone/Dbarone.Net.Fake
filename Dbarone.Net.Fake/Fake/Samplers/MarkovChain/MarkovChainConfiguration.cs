namespace Dbarone.Net.Fake;

/// <summary>
/// Defines the configuration used to train a corpus.
/// </summary>
public class MarkovChainTrainerConfiguration
{
    /// <summary>
    /// String array of word delimiters. Typically space character.
    /// </summary>
    public string[] WordDelimiters { get; set; } = new string[] { " " };

    /// <summary>
    /// Punctuation characters. When tokenising a string, any punctuation characters at
    /// the end of a word are separated from the word token. If the n-gram is a character, punctuation is removed.
    /// </summary>
    public string[] PunctuationCharacters { get; set; } = new string[] { ".", ",", ";", ":", "!" };
    
    /// <summary>
    /// Number of states to be considered when generating the next state.
    /// </summary>
    public int Order { get; set; } = 1;

    /// <summary>
    /// Defines the n-gram unit within the model.
    /// </summary>
    public MarkovChainLevel Level { get; set; } = MarkovChainLevel.Word;

    /// <summary>
    /// Callback function which can be used to determine whether or not to include a particular line of a corpus.
    /// </summary>
    public IncludeLineDelegate? IncludeLine { get; set; } = null;

    /// <summary>
    /// Callback function which can be used to execute any pre-processing transformation on a line before it is processed.
    /// </summary>
    public ProcessLineDelegate? ProcessLine { get; set; } = null;
}