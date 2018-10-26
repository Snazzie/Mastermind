using System;
using System.Linq;

namespace Mastermind
{
    public class Mastermind
    {
        public int GuessesLeft { get; private set; } = 10;
        public bool IsWinner { get; private set; }
        public bool GameEnded { get; private set; }
        public Colour[] Sequence
        {
            get
            {
                if (GameEnded)
                    return _sequence;
                throw new AccessViolationException("Game must be over to be able to see answer!");
            }
        }
        private readonly Colour[] _sequence = new Colour[4];


        public Mastermind()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < _sequence.Length; i++)
            {
                _sequence[i] = (Colour)rand.Next(0, Enum.GetValues(typeof(Colour)).Length - 1);
            }
        }

        public Tuple<int, int> TryGuess(Colour[] guess)
        {
            if (GameEnded)
                throw new Exception("Game has ended");
            ValidateGuess(guess);

            GuessesLeft--;

            if (IsGuessCorrect(guess, _sequence, out var hint))
            {
                GameEnded = true;
                IsWinner = true;
            }

            if (GuessesLeft < 1)
                EndGame();
            return hint;
        }

        public void EndGame()
        {
            GameEnded = true;
        }

        private void ValidateGuess(Colour[] guess)
        {
            if (guess == null)
                throw new GuessInvalidException("Guess cannot be null");
            if (guess.Length != _sequence.Length)
                throw new GuessInvalidException($"Guess must contain {_sequence.Length} colours. Received {guess.Length}.");
        }

        public static bool IsGuessCorrect(Colour[] guess, Colour[] correctSequence, out Tuple<int, int> hint)
        {
            int correctColors = 0, correctColorPlacements = 0;

            var seq = correctSequence.ToList();

            for (int i = 0; i < correctSequence.Length; i++)
            {
                if (seq.Contains(guess[i]))
                {
                    correctColors++;
                    seq.Remove(guess[i]);
                }

                if (correctSequence[i] == guess[i])
                {
                    correctColorPlacements++;
                }
            }

            hint = new Tuple<int, int>(correctColors, correctColorPlacements);

            return correctColorPlacements == correctSequence.Length;
        }
    }

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