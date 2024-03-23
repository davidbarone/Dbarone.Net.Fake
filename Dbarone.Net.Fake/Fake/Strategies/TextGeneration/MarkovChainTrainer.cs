namespace Dbarone.Net.Fake;

/// <summary>
/// Creates a transition matrix for a given corpus or training input.
/// </summary>
public class MarkovChainTrainer
{
    public MarkovChainModel Train(string str, string[] tokenDelimiters, int order = 1, IncludeLineDelegate? includeLine = null, ProcessLineDelegate? processLine = null)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(str);
        writer.Flush();
        stream.Position = 0;
        return Train(stream, tokenDelimiters, order, includeLine, processLine);
    }

    /// <summary>
    /// Creates a transition matrix for a given corpus or training text.
    /// </summary>
    /// <returns></returns>
    public MarkovChainModel Train(Stream str, string[] tokenDelimiters, int order = 1, IncludeLineDelegate? includeLine = null, ProcessLineDelegate? processLine = null)
    {
        Dictionary<string[], Dictionary<string, double>> matrix = new Dictionary<string[], Dictionary<string, double>>();
        Queue<string> queue = new Queue<string>();  // stores the current state
        using (var sr = new StreamReader(str))
        {
            Dictionary<string, object> state = new Dictionary<string, object>();

            var line = 0;
            List<string> lines = new List<string>();
            var linesIncluded = 0;
            var next = sr.ReadLine();
            while (next is not null)
            {
                line++;
                if (next is not null && (includeLine is null || includeLine(next, line, ref state)))
                {
                    next = processLine is null ? next : processLine(next);
                    lines.Add(next);
                    linesIncluded++;
                    if (next is not null)
                    {
                        var tokens = next.Split(tokenDelimiters, StringSplitOptions.TrimEntries);
                        foreach (var token in tokens)
                        {
                            // Check if the queue is full:
                            if (queue.Count() == order)
                            {
                                // Add the next token into the matrix
                                if (!matrix[queue.ToArray()].ContainsKey(token))
                                {
                                    matrix[queue.ToArray()][token] = 0;
                                }

                                // Update the transition matrix
                                matrix[queue.ToArray()][token]++;

                                // remove the oldest item
                                queue.Dequeue();
                            }
                            queue.Enqueue(token);

                            var key = queue.ToArray();
                            if (!matrix.ContainsKey(key) && queue.Count() == order)
                            {
                                matrix.Add(key, new Dictionary<string, double>());
                            }
                        }
                    }
                }
                next = sr.ReadLine();
            }

            // Next we convert to probability
            foreach (var key1 in matrix.Keys)
            {
                double total = 0;
                foreach (var key2 in matrix[key1].Keys)
                {
                    total = total + (double)matrix[key1][key2];
                }
                // Now we rewrite values between 0 and 1
                foreach (var key2 in matrix[key1].Keys)
                {
                    matrix[key1][key2] = matrix[key1][key2] / total;
                }
            }

            return new MarkovChainModel
            {
                Order = order,
                Matrix = matrix
            };
        }
    }
}