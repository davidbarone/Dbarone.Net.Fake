namespace Dbarone.Net.Fake;

/// <summary>
/// Delegate for callback function for manipulating the current line prior to it being processed.
/// You can remove any unwanted characters here.
/// </summary>
/// <param name="line">The line from the corpus.</param>
/// <returns>A modified line.</returns>
public delegate string ProcessLineDelegate(string line);