using System.Collections.Generic;
using Tests.Utilities;
using Trivia;
using Xunit;

namespace Tests;

public class QuestionsTest
{
    [Fact]
    public void DemoFeature1()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy, true);
        
        List<string> playersList = new List<string> { "Chet", "Pat", "Sue" };
        runner.PlayAGame(playersList);

        Assert.Contains("Rock", consoleSpy.Content);
    }
}