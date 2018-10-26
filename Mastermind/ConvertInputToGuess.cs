using System;

namespace Mastermind
{
    public class ConvertInputToGuess
    {
        public static Colour[] Convert(string input)
        {
            var guess = new Colour[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                var convertSuccess = int.TryParse(input[i].ToString(), out var convertedInput);

                guess[i] = !convertSuccess ? throw new ArgumentException($"'{input[i]}' is not a number") : (Colour)convertedInput;
            }

            return guess;
        }
    }
}
