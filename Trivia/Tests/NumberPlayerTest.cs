using System;
using System.Collections.Generic;
using Tests.Utilities;
using Trivia;
using Trivia.Exceptions;
using Xunit;

namespace Tests;

public class NumberPlayerTest
{
    [Fact]
    public void LessThan2()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);

        Player player = new Player("Chet");
        List<Player> playersList = new List<Player> { player };
        
        runner.PlayAGameTest(playersList, true);
        
        Assert.Contains(Messages.NotEnoughPlayerException, consoleSpy.Content);

    }

    [Fact]
    public void MoreThan6()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);

        Player player = new Player("Chet");
        Player player2 = new Player("Pat");
        Player player3 = new Player("Sue");
        Player player4 = new Player("Bob");
        Player player5 = new Player("Tris");
        Player player6 = new Player("Jok");
        Player player7 = new Player("Tess");
        List<Player> playersList = new List<Player> { player, player2, player3, player4, player5, player6, player7 };
        
        runner.PlayAGameTest(playersList, true);
       
        Assert.Contains(Messages.TooManyPlayerException, consoleSpy.Content);
    }
}