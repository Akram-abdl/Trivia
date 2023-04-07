using System;

namespace Trivia;

public class SystemConsole : IConsole
{
    /// <inheritdoc />
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}