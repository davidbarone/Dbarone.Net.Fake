using System.ComponentModel;
using Dbarone.Net.Fake;

/// <summary>
/// Markov chain text generator based on complete works of William Shakespear.
/// </summary>
public class WilliamFakespearStrategy : IFakerStrategy<string>
{
    public WilliamFakespearStrategy() : this(new Lcg()) { }

    public WilliamFakespearStrategy(IRandom<double> random)
    {
        this.Random = random;
    }

    /// <summary>
    /// Number of states to be considered when generating the next state.
    /// 1 means only current state considered.
    /// 2 means current and previous states considered.
    /// </summary>
    public int Order { get; set; } = 1;

    /// <summary>
    /// The random number generator used.
    /// </summary>
    public IRandom<double> Random { get; set; } = new Lcg();

    /// <summary>
    /// The transformation matrix. Defines the current states, and the next possible states.
    /// </summary>
    public Dictionary<string[], Dictionary<string, double>> Matrix = new Dictionary<string[], Dictionary<string, double>>(new StringArrayEqualityComparer());

    /// <summary>
    /// Define the token delimiters. Defaults to ' ' character.
    /// </summary>
    public string[] TokenDelimiters { get; set; } = new string[] { " " };

    public List<string> IgnoredLines { get; set; } = new List<string>();

    private Queue<string>? CurrentState = null;

    public string PreProcessLine(string line)
    {
        line = line.Trim();
        var start = line.IndexOf("[");
        var end = line.Reverse().ToList().IndexOf(']');
        if (start >= 0 && end >= 0)
        {
            // remove stage directions from line
            line = line.Substring(line.Length - end);
        }
        return line.Trim();
    }
    public bool ShouldIncludeLine(string line, int index, ref Dictionary<string, object> state)
    {
        int i = 0;
        if (index <= 80)
        {
            // file header information
            return false;
        }
        else if (line.Trim() == "Contents" || line.Trim() == "THE END")
        {
            state["InContents"] = true;
            return false;
        }
        else if (string.IsNullOrEmpty(line.Trim()))
        {
            // empty line
            return false;
        }
        else if (line.Trim().Equals(line.Trim().ToUpper()) && line.Reverse().ToList()[0] == '.')
        {
            // Named part / speaker
            return false;
        }
        else if (line.Trim().StartsWith('[') && line.Trim().EndsWith(']'))
        {
            // Stage direction
            return false;
        }
        else if (line.Trim().StartsWith("Dramatis Personæ"))
        {
            state["Dramatis Personæ"] = true;
            return false;
        }
        else if (line.ToUpper().StartsWith("SCENE I.") && state.ContainsKey("Dramatis Personæ") && (bool)state["Dramatis Personæ"] == true && line.Trim().Length > "SCENE I.".Length)
        {
            state["Dramatis Personæ"] = false;
            state["InContents"] = false;
            return false;
        }
        else if (line.ToUpper().StartsWith("SCENE") || line.ToUpper().StartsWith("ACT"))
        {
            return false;
        }
        else if (int.TryParse(line.Trim(), out i) == true)
        {
            // Chapter numbers
            return false;
        }
        else if (state.ContainsKey("InContents") && (bool)state["InContents"] == true)
        {
            return false;
        }
        else if (line.Equals(line.ToUpper()))
        {
            // ignore lines completely in upper case
            return false;
        }
        else
        {
            return true;
        }
    }

    public void CreateModel()
    {
        // Read text corpus to generate transition matrix
        var str = File.Open("C:\\Users\\david\\Desktop\\WilliamShakespear.txt", FileMode.Open);
        var outputPath = "C:\\Users\\david\\Desktop\\WilliamShakespear_Output.txt";
        Queue<string> queue = new Queue<string>();  // stores the current state

        var sr = new StreamReader(str);
        Dictionary<string, object> state = new Dictionary<string, object>();

        var line = 0;
        List<string> lines = new List<string>();
        var linesIncluded = 0;
        var next = sr.ReadLine();
        while (next is not null)
        {
            line++;
            if (next is not null && ShouldIncludeLine(next, line, ref state))
            {
                var processed = next;
                processed = PreProcessLine(next);
                lines.Add(processed);
                linesIncluded++;
                if (processed is not null)
                {
                    var tokens = processed.Split(TokenDelimiters, StringSplitOptions.TrimEntries);
                    foreach (var token in tokens)
                    {
                        // Check if the queue is full:
                        if (queue.Count() == this.Order)
                        {
                            // Add the next token into the matrix
                            if (!Matrix[queue.ToArray()].ContainsKey(token))
                            {
                                Matrix[queue.ToArray()][token] = 0;
                            }

                            // Update the transition matrix
                            Matrix[queue.ToArray()][token]++;

                            // remove the oldest item
                            queue.Dequeue();
                        }
                        queue.Enqueue(token);

                        var key = queue.ToArray();
                        if (!Matrix.ContainsKey(key) && queue.Count() == this.Order)
                        {
                            Matrix.Add(key, new Dictionary<string, double>());
                        }
                    }
                }
            }
            next = sr.ReadLine();
        }

        // Next we convert to probability
        foreach (var key1 in this.Matrix.Keys)
        {
            double total = 0;
            foreach (var key2 in this.Matrix[key1].Keys)
            {
                total = total + (double)this.Matrix[key1][key2];
            }
            // Now we rewrite values between 0 and 1
            foreach (var key2 in this.Matrix[key1].Keys)
            {
                this.Matrix[key1][key2] = this.Matrix[key1][key2] / total;
            }
        }

        File.WriteAllLines(outputPath, lines);
        var b = Matrix.Keys.Count();
        var a = line;

    }

    private Queue<string> GetStartingState(double rnd)
    {
        // Get a random existing state if non exists
        int i = (int)(rnd * this.Matrix.Keys.Count());
        var values = Matrix.Keys.OrderBy(k => k, new StringArrayComparer()).ToList()[i];
        return new Queue<string>(values);
    }

    public string Next(int i, string? last = null)
    {
        var rnd = Random.Next();

        if (CurrentState is null)
        {
            this.CurrentState = GetStartingState(rnd);
        }

        // Get a random next value, weighted by weights
        var dictValues = Matrix[this.CurrentState.ToArray()];
        var dictKeys = dictValues.Keys.OrderBy(k => k).ToList();
        double total = 0;
        string selectedKey = dictKeys.First();

        for (int j = 0; j < dictKeys.Count(); j++)
        {
            total = total + dictValues[selectedKey];
            if (rnd > total)
            {
                break;
            }
            selectedKey = dictKeys[j];
        }

        // Update the current state
        this.CurrentState.Dequeue();
        this.CurrentState.Enqueue(selectedKey);

        // Now we have the selected key, return it. 
        return selectedKey;
    }
}