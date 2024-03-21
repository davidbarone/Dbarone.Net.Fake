using System.ComponentModel;

/// <summary>
/// Markov chain text generator based on complete works of William Shakespear.
/// </summary>
public class WilliamFakespearStrategy
{

    /// <summary>
    /// Number of states to be considered when generating the next state. 1 means only current state considered. 2 means current and previous states considered.
    /// </summary>
    public int Order { get; set; } = 1;

    public Queue<string> queue = new Queue<string>();
    
    public Dictionary<string, int> matrix = new Dictionary<string, int>();

    public string[] TokenDelimiters { get; set; } = new string[] { " " };

    public List<string> IgnoredLines { get; set; } = new List<string>();

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
        else if (line.Equals(line.ToUpper())){
            // ignore lines completely in upper case
            return false;
        }
        else
        {
            return true;
        }
    }

    public WilliamFakespearStrategy()
    {
        // Read text corpus to generate transition matrix
        var str = File.Open("C:\\Users\\david\\Desktop\\WilliamShakespear.txt", FileMode.Open);
        var outputPath = "C:\\Users\\david\\Desktop\\WilliamShakespear_Output.txt";

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
                        if (!matrix.ContainsKey(token))
                        {
                            matrix.Add(token, 0);
                        }
                    }
                }
            }
            next = sr.ReadLine();
        }
        File.WriteAllLines(outputPath, lines);
        var b = matrix.Keys.Count();
        var a = line;

    }
}