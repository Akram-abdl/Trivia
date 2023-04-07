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

        List<string> playersList = new List<string> { "Chet" };
        runner.PlayAGameTest(playersList, true);
        
        Assert.Contains(Messages.NotEnoughPlayerException, consoleSpy.Content);

    }

    [Fact]
    public void MoreThan6()
    {
        var consoleSpy = new ConsoleSpy();

        var runner = new GameRunner(consoleSpy);

        List<string> playersList = new List<string> { "Chet", "Pat", "Sue", "Bob", "Tris", "Jok", "Tess"};
        runner.PlayAGameTest(playersList, true);
       
        Assert.Contains(Messages.TooManyPlayerException, consoleSpy.Content);
    }
}