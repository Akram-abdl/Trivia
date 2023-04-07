using System;

namespace Trivia;

public class TooManyPlayersException : Exception
{
    public TooManyPlayersException() : base() { }
}