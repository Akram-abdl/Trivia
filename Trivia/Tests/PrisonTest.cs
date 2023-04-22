using System.Collections.Generic;
using Tests.Utilities;
using Trivia;
using Xunit;

namespace Tests;

public class PrisonTest
{
    [Fact]
    public void TechnoQuestion()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        Player player = new Player("Chet", 1);
        Player player2 = new Player("Pat", 1);
        Player player3 = new Player("Sue", 1);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,true);

        Assert.Contains("Techno", consoleSpy.Content);
        Assert.DoesNotContain("Rock", consoleSpy.Content);
    }
}