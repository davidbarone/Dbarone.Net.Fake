using System.ComponentModel;
using Dbarone.Net.Fake;

/// <summary>
/// Markov chain text sampler class. Generates pseudo-random text based on a model known as a transition matrix.
/// </summary>
public class MarkovChainSampler : AbstractSampler<string>, ISampler<string>
{
    #region Properties

    private MarkovChainModel Model { get; init; }

    /// <summary>
    /// Stores the current state.
    /// </summary>
    private Queue<string> CurrentState = new Queue<string>();

    #endregion

    #region Ctor

    /// <summary>
    /// Creates a new MarkovChainSampler instance from a pre-trained model.
    /// </summary>
    /// <param name="model">The model to use.</param>
    public MarkovChainSampler(MarkovChainModel model) : base()
    {
        this.Model = model;
    }

    /// <summary>
    /// Creates a new MarkovChainSampler instance from a pre-trained model and a specified random number generator.
    /// </summary>
    /// <param name="random">A provided random number generator.</param>
    /// <param name="model">The model to use.</param>
    public MarkovChainSampler(MarkovChainModel model, IRandom<double> random) : base(random)
    {
        this.Model = model;
    }

    #endregion

    #region Methods

    private string next(double rnd)
    {
        // Get a random next value using the transition matrix for the current state.
        var currentState = this.Model.Matrix.Rows.First(r => r.CurrentState.SequenceEqual(this.CurrentState.ToArray()));
        double total = 0;
        string selectedKey = "";

        foreach (var element in currentState.NextElements)
        {
            selectedKey = element.NextState;
            total = total + element.Probability;
            if (total > rnd)
            {
                break;
            }
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

    public override string Next()
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

    #endregion
}