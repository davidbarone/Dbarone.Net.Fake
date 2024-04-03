using System.Collections.Generic;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class MarkovChainSamplerTests {

    [Fact]
    public void LoremIpsumCharacterSampler() {

        // Creates a character-based Markov chain model for Lorem ipsum... corpus, then generates a new text using _new_ words that sound vaguely latin...
        var corpus = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        MarkovChainTrainer trainer = new MarkovChainTrainer();
        MarkovChainTrainerConfiguration configuration = new MarkovChainTrainerConfiguration {
            Order = 2,
            Level = MarkovChainLevel.Character
        };
        var model = trainer.Train(corpus, configuration);
        MarkovChainSampler sampler = new MarkovChainSampler(model);

        List<string> words = new List<string>();
        for (int i = 1; i < 10; i++) {
            words.Add(sampler.Next(i));
        }
        var actual = string.Join(" ", words);
        Assert.Equal("am commod conseriscinis non am in nost consecte commod", actual); // that looks like real latin!
    }
}