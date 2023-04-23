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
        
        Player player = new Player("Chet", 1,2);
        Player player2 = new Player("Pat", 1, 2);
        Player player3 = new Player("Sue", 1, 2);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,true, 1);

        Assert.Contains("Techno", consoleSpy.Content);
        Assert.DoesNotContain("Rock", consoleSpy.Content);
    }
    
    [Fact]
    public void RockQuestion()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        Player player = new Player("Chet", 1, 2);
        Player player2 = new Player("Pat", 1, 2);
        Player player3 = new Player("Sue", 1,2);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,false, 2);

        Assert.Contains("Rock", consoleSpy.Content);
        Assert.DoesNotContain("Techno", consoleSpy.Content);
    }

    [Fact]
    public void AllSubjectQuestion()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        Player player = new Player("Chet", 1, 2);
        Player player2 = new Player("Pat", 1, 2);
        Player player3 = new Player("Sue", 1,2);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,false, 2,50);
        
        Assert.Contains("Rock", consoleSpy.Content);
        Assert.Contains("Pop", consoleSpy.Content);
        Assert.Contains("Rap", consoleSpy.Content);
        Assert.Contains("Science", consoleSpy.Content);
        Assert.Contains("Sports", consoleSpy.Content);
        Assert.Contains("Philosophy", consoleSpy.Content);
        Assert.Contains("Literature", consoleSpy.Content);
        Assert.Contains("Geography", consoleSpy.Content);
        Assert.Contains("People", consoleSpy.Content);
        
    }
}