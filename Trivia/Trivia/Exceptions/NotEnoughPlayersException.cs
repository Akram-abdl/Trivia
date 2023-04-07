using System;

namespace Trivia;

public class NotEnoughPlayersException : Exception
{
    public NotEnoughPlayersException() : base() { }
}