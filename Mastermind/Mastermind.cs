using System;
using System.Linq;

namespace Mastermind
{
    public class Mastermind : IMastermind
    {
        public int GuessesLeft { get; private set; } = 10;
        public bool IsWinner { get; private set; }
        public bool GameEnded { get; private set; }
        public ISequence Sequence
        {
            get
            {
                if (GameEnded)
                    return _sequence;
                throw new AccessViolationException("Game must be over to be able to see answer!");
            }
        }
        private readonly ISequence _sequence = new Sequence() { Value = new Colour[4] };


        public Mastermind()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < _sequence.Value.Length; i++)
            {
                _sequence.Value[i] = (Colour)rand.Next(0, Enum.GetValues(typeof(Colour)).Length - 1);
            }
        }

        public Tuple<int, int> TryGuess(Colour[] guess)
        {
            if (GameEnded || GuessesLeft < 1)
                throw new Exception("Game has ended");
            ValidateGuess(guess);

            GuessesLeft--;

            if (IsGuessCorrect(guess, _sequence.Value, out var hint))
            {
                GameEnded = true;
                IsWinner = true;
            }

            if (GuessesLeft < 1)
                GameEnded = true;
            return hint;
        }

        private void ValidateGuess(Colour[] guess)
        {
            if (guess == null)
                throw new GuessInvalidException("Guess cannot be null");
            if (guess.Length != _sequence.Value.Length)
                throw new GuessInvalidException($"Guess must contain {_sequence.Value.Length} colours. Received {guess.Length}.");
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
}