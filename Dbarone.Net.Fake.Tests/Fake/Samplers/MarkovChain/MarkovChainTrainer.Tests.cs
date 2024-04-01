using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Dbarone.Net.Fake;
using Xunit;
using System.Linq;
using System.Net.Http;
using Xunit.Sdk;

public class MarkovChainTrainerTests
{
    [Fact]
    public void WilliamFakespearTrainer()
    {
        // Project Gutenberg - Complete works of William Shakespeare
        string url = "https://www.gutenberg.org/cache/epub/100/pg100.txt";
        string text = "";

        HttpClient httpClient = new HttpClient();
        text = httpClient.GetStringAsync(url).Result;

        MarkovChainTrainer trainer = new MarkovChainTrainer();

        IncludeLineDelegate includeLine = (string line, int index, ref Dictionary<string, object> state) =>
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
        };

        ProcessLineDelegate processLine = (string line) =>
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

        };

        var configuration = new MarkovChainTrainerConfiguration
        {
            WordDelimiters = new string[] {" "},
            Order = 2,
            IncludeLine = includeLine,
            ProcessLine = processLine
        };

        var model = trainer.Train(text, configuration);

        var markov = new MarkovChainSampler(model);

        List<string> results = new List<string>();
        for (int i = 0; i < 1000; i++)
        {
            results.Add(markov.Next(i, null));
        }
        var b = results;
    }

    [Theory]
    [InlineData("When the going gets tough, the tough get going.", 1, 10)]
    public void SimpleWordTrainers(string corpus, int order, int expectedMatrixSize) {
        MarkovChainTrainerConfiguration configuration = new MarkovChainTrainerConfiguration
        {
            Level = MarkovChainLevel.Word,
            Order = order
        };

        MarkovChainTrainer trainer = new MarkovChainTrainer();
        var model = trainer.Train(corpus, configuration);



    }
}