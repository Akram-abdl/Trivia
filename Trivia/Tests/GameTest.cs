using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Utilities;
using Trivia;
using Xunit;

public class GameTest
{
    
    [Fact]
    public void noReGame()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        Player player = new Player("Chet", 1,2);
        Player player2 = new Player("Pat", 1, 2);
        Player player3 = new Player("Sue", 1, 2);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,true, 2);
        
        string text = consoleSpy.Content;
        
        string Game = "Game";  
        string Over = "Over";
        
        //Convert the string into an array of words  
        string[] source = text.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);  
  
        // Create the query.  Use the InvariantCultureIgnoreCase comparision to match "data" and "Data"
        var matchQueryGame = from word in source  
            where word.Equals(Game, StringComparison.InvariantCultureIgnoreCase)  
            select word;  
        
        // Create the query.  Use the InvariantCultureIgnoreCase comparision to match "data" and "Data"
        var matchQueryOver = from word in source  
            where word.Equals(Over, StringComparison.InvariantCultureIgnoreCase)  
            select word;  
        
  
        // Count the matches, which executes the query.  
        int wordCountGame = matchQueryGame.Count();  
        int wordCountOver = matchQueryOver.Count();
        
        // first one for win the game and second one for the game over
        Assert.Equal(2,wordCountGame);
        Assert.Equal(1,wordCountOver );
    }
    
    [Fact]
    public void reGame()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);
        
        Player player = new Player("Chet", 1,2);
        Player player2 = new Player("Pat", 1, 2);
        Player player3 = new Player("Sue", 1, 2);
        List<Player> playersList = new List<Player> { player, player2, player3 };
        
        runner.PlayAGameTest(playersList,true, 1);
        
        string text = consoleSpy.Content;
        
        string add = "added";
        string Game = "Game";  
        string Over = "Over";
     
  
        //Convert the string into an array of words  
        string[] source = text.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);  
  
        // Create the query.  Use the InvariantCultureIgnoreCase comparision to match "data" and "Data"
        var matchQueryGame = from word in source  
            where word.Equals(Game, StringComparison.InvariantCultureIgnoreCase)  
            select word;  
        
        // Create the query.  Use the InvariantCultureIgnoreCase comparision to match "data" and "Data"
        var matchQueryOver = from word in source  
            where word.Equals(Over, StringComparison.InvariantCultureIgnoreCase)  
            select word;  
  
        // Create the query.  Use the InvariantCultureIgnoreCase comparision to match "data" and "Data"
        var matchQueryAdd = from word in source  
            where word.Equals(add, StringComparison.InvariantCultureIgnoreCase)  
            select word;  
        
        
        // Count the matches, which executes the query.  
        int wordCountAdd = matchQueryAdd.Count();
        int wordCountGame = matchQueryGame.Count();  
        int wordCountOver = matchQueryOver.Count();
        
        //nous n'ajoutons pas de nouveau joueur lord du restart, nous gardons bien les anciens joueurs et paramètre
        Assert.Equal(3,wordCountAdd);
    
        // (first one for win the game and second one for the game over) X 2 because regame
        Assert.Equal(4, wordCountGame);
        Assert.Equal( 2, wordCountOver);
    }

}