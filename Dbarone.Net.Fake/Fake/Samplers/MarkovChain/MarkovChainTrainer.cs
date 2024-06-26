namespace Dbarone.Net.Fake;

/// <summary>
/// Creates a transition matrix for a given corpus or training input.
/// </summary>
public class MarkovChainTrainer
{
    public MarkovChainModel Train(string corpus, MarkovChainTrainerConfiguration configuration)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(corpus);
        writer.Flush();
        stream.Position = 0;
        return Train(stream, configuration);
    }

    private void UpdateTransitionMatrix(
        StochasticMatrix matrix,
        MarkovChainTrainerConfiguration configuration,
        Queue<string> queue,
        string next)
    {
        // Add the next token into the matrix for the existing n-gram. The n-gram must exist at this point.
        if (!matrix.Exists(queue.ToArray()))
        {
            throw new Exception("Current state does not exist!");
        }

        var row = matrix.GetRow(queue.ToArray());
        var nextElement = row.NextElements.FirstOrDefault(e => e.NextState.Equals(next));
        if (nextElement == null)
        {
            nextElement = new StochasticMatrixElement
            {
                NextState = next
            };
            row.NextElements.Add(nextElement);
        }

        nextElement.Occurences++;

        // Do NOT add the next item to the matrix if "". This is used as end of word marker when Level = Character.
        if (next != "")
        {
            // Check if the queue length equals the n-gram size, dequeue the oldest element:
            if (queue.Count() == configuration.Order)
            {
                // remove the oldest item
                queue.Dequeue();
            }
            queue.Enqueue(next);

            // Make sure the current queue is in the transformation matrix
            // Can include n-grams with length < Order (e.g. the first words in the corpus).
            row = matrix.GetRowOrDefault(queue.ToArray());
            if (row == null)
            {
                row = new StochasticMatrixRow
                {
                    CurrentState = queue.ToArray(),
                    Occurences = 0
                };
                matrix.AddRow(row);
            }
            row.Occurences++;
        }
    }

    /// <summary>
    /// Creates a transition matrix for a given corpus or training text.
    /// </summary>
    /// <param name="stream">The stream containing the corpus.</param>
    /// <param name="configuration">The configuration settings.</param>
    /// <returns>A trained model.</returns>
    public MarkovChainModel Train(Stream stream, MarkovChainTrainerConfiguration configuration)
    {
        StochasticMatrix matrix = new StochasticMatrix();
        Queue<string> queue = new Queue<string>();  // stores the current state

        // Initialise the matrix with the empty n-gram (this represents the beginning of text, before 1st token is read)
        matrix.AddRow(new StochasticMatrixRow { CurrentState = queue.ToArray(), Occurences = 1 });

        using (var sr = new StreamReader(stream))
        {
            // Used as general state bag when calling IncludeLine callback.
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

                        var words = next.Split(configuration.WordDelimiters, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            if (configuration.Level == MarkovChainLevel.Character)
                            {
                                // reset the queue at the start of each word if level == character.
                                queue = new Queue<string>();
                                foreach (var character in word)
                                {
                                    UpdateTransitionMatrix(matrix, configuration, queue, character.ToString());
                                }
                                // Add null / EOF character - need mechanism to 'end' character-based transitions.
                                UpdateTransitionMatrix(matrix, configuration, queue, "");
                            }
                            else
                            {
                                UpdateTransitionMatrix(matrix, configuration, queue, word);
                            }
                        }
                    }
                }
                next = sr.ReadLine();
            }

            // Next we convert counts to probability between 0 and 1
            int total = 0;
            foreach (var row in matrix.Rows)
            {
                total = total + row.Occurences;
                int rowTotal = 0;
                foreach (var element in row.NextElements)
                {
                    rowTotal = rowTotal + element.Occurences;
                }
                // Now write element probabilities
                foreach (var element in row.NextElements)
                {
                    element.Probability = (double)element.Occurences / rowTotal;
                }
            }
            // Now write row probabilities
            foreach (var row in matrix.Rows)
            {
                row.Probability = (double)row.Occurences / total;
            }

            return new MarkovChainModel
            {
                Order = configuration.Order,
                Level = configuration.Level,
                Matrix = matrix
            };
        }
    }
}