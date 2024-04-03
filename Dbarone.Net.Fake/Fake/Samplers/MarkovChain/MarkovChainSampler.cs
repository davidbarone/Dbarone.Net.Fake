using System.ComponentModel;
using Dbarone.Net.Fake;

/// <summary>
/// Markov chain text sampler class. Generates pseudo-random text based on a model known as a transition matrix.
/// </summary>
public class MarkovChainSampler : ISampler<string>
{
    private MarkovChainModel Model { get; init; }

    /// <summary>
    /// Default ctor.
    /// </summary>
    public MarkovChainSampler(MarkovChainModel model) : this(model, new Lcg()) { }

    /// <summary>
    /// Stores the current state.
    /// </summary>
    private Queue<string>? CurrentState = null;

    /// <summary>
    /// Ctor with specified random number generator.
    /// </summary>
    /// <param name="random"></param>
    public MarkovChainSampler(MarkovChainModel model, IRandom<double> random)
    {
        this.Random = random;
        this.Model = model;
    }

    /// <summary>
    /// The random number generator used.
    /// </summary>
    public IRandom<double> Random { get; set; } = new Lcg();

    private string next(double rnd)
    {
        // Get a random next value using the transition matrix for the current state.
        var dictValues = this.Model.Matrix[this.CurrentState.ToArray()];
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
        if (this.CurrentState.Count() == this.Model.Order)
        {
            this.CurrentState.Dequeue();
        }
        this.CurrentState.Enqueue(selectedKey);

        // Now we have the selected key, return it. 
        return selectedKey;

    }

    public string Next(int i, string? last = null)
    {
        var rnd = Random.Next();

        // For character based model, we clear the current state each time
        // For both character and word based models, we always start from a known point.
        if (Model.Level == MarkovChainLevel.Character || CurrentState is null)
        {
            CurrentState = new Queue<string>();
        }

        if (Model.Level == MarkovChainLevel.Word)
        {
            return next(rnd);
        }
        else
        {
            // for character-based, keep getting successive characters until we reach an end-of-word ("") character
            string word = "";
            string character = "";
            do
            {
                character = next(rnd);
                word = word + character;
                rnd = Random.Next();
            } while (character != "");
            return word;
        }
    }

    private Queue<string> GetRandomStartingState(double rnd)
    {
        // Get a random existing state if non exists
        int i = (int)(rnd * this.Model.Matrix.Keys.Count());
        var values = this.Model.Matrix.Keys.OrderBy(k => k, new StringArrayComparer()).ToList()[i];
        return new Queue<string>(values);
    }
}