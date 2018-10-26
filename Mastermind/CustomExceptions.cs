using System;

namespace Mastermind
{
    public class GuessInvalidException : Exception
    {
        public GuessInvalidException()
        {
        }

        public GuessInvalidException(string message)
            : base(message)
        {
        }
    }
}