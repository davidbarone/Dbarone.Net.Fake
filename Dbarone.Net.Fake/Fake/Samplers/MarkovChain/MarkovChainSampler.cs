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

    public string Next(int i, string? last = null)
    {
        var rnd = Random.Next();

        // If no current state, create one randomly.
        if (CurrentState is null)
        {
            this.CurrentState = GetStartingState(rnd);
        }

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
        this.CurrentState.Dequeue();
        this.CurrentState.Enqueue(selectedKey);

        // Now we have the selected key, return it. 
        return selectedKey;
    }

    private Queue<string> GetStartingState(double rnd)
    {
        // Get a random existing state if non exists
        int i = (int)(rnd * this.Model.Matrix.Keys.Count());
        var values = this.Model.Matrix.Keys.OrderBy(k => k, new StringArrayComparer()).ToList()[i];
        return new Queue<string>(values);
    }
}