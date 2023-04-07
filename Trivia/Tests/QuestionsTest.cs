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
        
        Player player = new Player("Chet", true);
        Player player2 = new Player("Pat", true);
        Player player3 = new Player("Sue", true);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,true);

        Assert.Contains("Techno", consoleSpy.Content);
        Assert.DoesNotContain("Rock", consoleSpy.Content);
    }
    
    [Fact]
    public void RockQuestion()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        Player player = new Player("Chet", true);
        Player player2 = new Player("Pat", true);
        Player player3 = new Player("Sue", true);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,false);

        Assert.Contains("Rock", consoleSpy.Content);
        Assert.DoesNotContain("Techno", consoleSpy.Content);
    }
}