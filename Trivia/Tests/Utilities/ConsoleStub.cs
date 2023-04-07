using Trivia;

namespace Tests.Utilities;

public class ConsoleStub : IConsole
{
    /// <inheritdoc />
    public void WriteLine(string message)
    {
    }
}