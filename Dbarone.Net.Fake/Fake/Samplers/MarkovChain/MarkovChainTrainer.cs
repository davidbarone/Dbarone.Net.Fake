namespace Dbarone.Net.Fake;

/// <summary>
/// Creates a transition matrix for a given corpus or training input.
/// </summary>
public class MarkovChainTrainer
{
    public MarkovChainModel Train(string str, MarkovChainTrainerConfiguration configuration)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(str);
        writer.Flush();
        stream.Position = 0;
        return Train(stream, configuration);
    }

    /// <summary>
    /// Creates a transition matrix for a given corpus or training text.
    /// </summary>
    /// <returns></returns>
    public MarkovChainModel Train(Stream str, MarkovChainTrainerConfiguration configuration)
    {
        Dictionary<string[], Dictionary<string, double>> matrix = new Dictionary<string[], Dictionary<string, double>>(new StringArrayEqualityComparer());
        Dictionary<string[], int> occurences = new Dictionary<string[], int>(new StringArrayEqualityComparer());

        Queue<string> queue = new Queue<string>();  // stores the current state

        // Initialise the matrix with the empty n-gram (this represents before 1st tokens read)
        matrix.Add(queue.ToArray(), new Dictionary<string, double>());
        occurences.Add(queue.ToArray(), 0);

        using (var sr = new StreamReader(str))
        {
            Dictionary<string, object> state = new Dictionary<string, object>();

            var line = 0;
            var next = sr.ReadLine();
            while (next is not null)
            {
                // For character n-grams, we remove punctuation characters.
                if (configuration.Level == MarkovChainLevel.Character)
                {
                    foreach (var item in configuration.PunctuationCharacters)
                    {
                        next = next.Replace(item, "");
                    }
                }

                line++;
                if (next is not null && (configuration.IncludeLine is null || configuration.IncludeLine(next, line, ref state)))
                {
                    next = configuration.ProcessLine is null ? next : configuration.ProcessLine(next);
                    if (next is not null)
                    {
                        // Separate punctuation from neighbour words.
                        foreach (var punctuationCharacter in configuration.PunctuationCharacters)
                        {
                            next = next.Replace(punctuationCharacter, $"{configuration.WordDelimiters[0]}{punctuationCharacter}{configuration.WordDelimiters[0]}");
                        }

                        var tokens = next.Split(configuration.WordDelimiters, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                        foreach (var token in tokens)
                        {

                            // Add the next token into the matrix for the existing n-gram. The n-gram must exist at this point.
                            if (!matrix.ContainsKey(queue.ToArray()))
                            {
                                throw new Exception("Key does not exist!");
                            }

                            if (!matrix[queue.ToArray()].ContainsKey(token))
                            {
                                matrix[queue.ToArray()][token] = 0;
                            }

                            // Update the transition matrix
                            matrix[queue.ToArray()][token]++;

                            // Check if the queue length equals the n-gram size, dequeue the oldest element:
                            if (queue.Count() == configuration.Order)
                            {
                                // remove the oldest item
                                queue.Dequeue();
                            }
                            queue.Enqueue(token);

                            // Make sure the current queue is in the transformation matrix
                            // Can include n-grams with length < Order (e.g. the first words in the corpus).
                            var key = queue.ToArray();
                            if (!matrix.ContainsKey(key))
                            {
                                matrix.Add(key, new Dictionary<string, double>());
                                occurences.Add(key, 0);
                            }
                            occurences[key] = occurences[key] + 1;
                        }
                    }
                }
                next = sr.ReadLine();
            }

            // Next we convert counts to probability between 0 and 1
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
                Order = configuration.Order,
                Level = configuration.Level,
                Matrix = matrix,
                Occurences = occurences
            };
        }
    }
}