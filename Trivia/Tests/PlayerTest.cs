using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Utilities;
using Trivia;
using Trivia.Exceptions;
using Xunit;

namespace Tests;

public class PlayerTest
{
    [Fact]
    public void LessThan2()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);

        Player player = new Player("Chet");
        List<Player> playersList = new List<Player> { player };
        
        runner.PlayAGameTest(playersList, true,2);
        
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
        
        runner.PlayAGameTest(playersList, true, 2);
       
        Assert.Contains(Messages.TooManyPlayerException, consoleSpy.Content);
    }

    [Fact]
    public void QuitGame()
    {
        
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        Player player = new Player("Chet", 2,2);
        Player player2 = new Player("Pat", 1,2);
        Player player3 = new Player("Sue", 1,2);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,true, 2);

        
        string text = consoleSpy.Content;
        
        string Chet = "Chet";  
        string Pat = "Pat";
        string Sue = "Sue";
  
        //Convert the string into an array of words  
        string[] source = text.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);  
  
        // Create the query.  Use the InvariantCultureIgnoreCase comparision to match "data" and "Data"
        var matchQueryChet = from word in source  
            where word.Equals(Chet, StringComparison.InvariantCultureIgnoreCase)  
            select word;  
        
        var matchQueryPat = from word in source  
            where word.Equals(Pat, StringComparison.InvariantCultureIgnoreCase)  
            select word; 
        
        var matchQuerySue = from word in source  
            where word.Equals(Sue, StringComparison.InvariantCultureIgnoreCase)  
            select word; 
  
        // Count the matches, which executes the query.  
        int wordCountChet = matchQueryChet.Count();  
        int wordCountPat = matchQueryPat.Count();
        int wordCountSue = matchQuerySue.Count();
      
        Assert.Contains("Chet has left the game.", consoleSpy.Content);
        Assert.NotInRange(wordCountChet, 20,100);
        Assert.InRange(wordCountPat, 20,100);
        Assert.InRange(wordCountSue, 20,100);
        
    }
}