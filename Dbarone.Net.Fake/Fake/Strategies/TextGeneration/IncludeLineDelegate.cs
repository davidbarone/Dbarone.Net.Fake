namespace Dbarone.Net.Fake;

/// <summary>
/// Delegate for callback function for determining if line in corpus should be included in the model.
/// </summary>
/// <param name="line">The line of text.</param>
/// <param name="index">The line number.</param>
/// <param name="state">Current state. Can be used to place state variables when processing the corpus.</param>
/// <returns>Returns true if the line is to be included in the model.</returns>
public delegate bool IncludeLineDelegate(string line, int index, ref Dictionary<string, object> state);