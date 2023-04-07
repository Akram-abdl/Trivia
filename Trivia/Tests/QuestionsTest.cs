using System.Collections.Generic;
using Tests.Utilities;
using Trivia;
using Xunit;

namespace Tests;

public class QuestionsTest
{
    [Fact]
    public void TechnoQuestion()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        List<string> playersList = new List<string> { "Chet", "Pat", "Sue" };
        runner.PlayAGameTest(playersList,true);

        Assert.Contains("Techno", consoleSpy.Content);
        Assert.DoesNotContain("Rock", consoleSpy.Content);
    }
    
    [Fact]
    public void RockQuestion()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        List<string> playersList = new List<string> { "Chet", "Pat", "Sue" };
        runner.PlayAGameTest(playersList,false);

        Assert.Contains("Techno", consoleSpy.Content);
        Assert.DoesNotContain("Rock", consoleSpy.Content);
    }
}