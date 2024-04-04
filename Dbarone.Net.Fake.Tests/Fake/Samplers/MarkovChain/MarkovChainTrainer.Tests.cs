using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Dbarone.Net.Fake;
using Xunit;
using System.Linq;
using Xunit.Sdk;

namespace Dbarone.Net.Fake.Tests;

public class MarkovChainTrainerTests
{

    [Theory]
    [InlineData("When the going gets tough, the tough get going.", 1, MarkovChainLevel.Word, 9)]    // 6 words, 2 punctuation, and BOF marker = 9
    [InlineData("When the going gets tough, the tough get going.", 2, MarkovChainLevel.Word, 12)]    // BOF/BOF BOF/When When/the the/going going/gets gets/tough tough/, ,/the the/tough tough/get get/going going/. = 12
    [InlineData("When the going gets tough, the tough get going.", 1, MarkovChainLevel.Character, 11)]    // BOF W h e n t g o i s u  = 11
    [InlineData("When the going gets tough, the tough get going.", 2, MarkovChainLevel.Character, 19)]    // BOF/BOF BOF/W Wh he en BOF/t th BOF/g go oi in ng ge et ts to ou ug gh = 19
    public void TestMatrixSize(string corpus, int order, MarkovChainLevel level, int expectedMatrixSize)
    {
        MarkovChainTrainerConfiguration configuration = new MarkovChainTrainerConfiguration
        {
            Level = level,
            Order = order
        };

        MarkovChainTrainer trainer = new MarkovChainTrainer();
        var model = trainer.Train(corpus, configuration);
        Assert.Equal(expectedMatrixSize, model.Matrix.Rows.Count());
    }
}