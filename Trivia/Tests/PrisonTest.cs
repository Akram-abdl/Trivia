using System.Collections.Generic;
using Tests.Utilities;
using Trivia;
using Xunit;

namespace Tests;

public class PrisonTest
{
    //si erreur de réponse, normal peut etre que aucun joueur parti en prison
    [Fact]
    public void OutOfPrisonQuestion()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        Player player = new Player("Chet", 1,2);
        Player player2 = new Player("Pat", 1,2);
        Player player3 = new Player("Sue", 1,2);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,true);

        Assert.Contains("incorrectly answered", consoleSpy.Content);
        Assert.Contains("is getting out of the penalty box", consoleSpy.Content);
    }
}