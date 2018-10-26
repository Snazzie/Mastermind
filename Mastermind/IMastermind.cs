using System;

namespace Mastermind
{
    public interface IMastermind
    {
        int GuessesLeft { get; }
        bool IsWinner { get; }
        bool GameEnded { get; }
        ISequence Sequence { get; }
        Tuple<int, int> TryGuess(Colour[] guess);
    }
}