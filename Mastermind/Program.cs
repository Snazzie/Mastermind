using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Mastermind();

            while (!game.IsWinner && game.GuessesLeft != 0 && !game.GameEnded)
            {
                Console.WriteLine("Red = 0, Blue = 1, Green = 2, Purple = 3, Yellow = 4, White = 5");
                Console.Write("Input guess: ");
                var input = Console.ReadLine();
                try
                {
                    var guess = ConvertInputToGuess(input);
                    var result = game.TryGuess(guess);
                    
                    Console.WriteLine(Environment.NewLine + (game.IsWinner
                        ? "GG EZ"
                        : $"Correct colours: {result.Item1}    Correct placements: {result.Item2}    Guesses left: {game.GuessesLeft}"));
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
            }

            var answer = "";
            foreach (var colour in game.Sequence)
            {
                answer += colour.ToString() +" ";
            }

            Console.WriteLine($"Game Over! The correct sequence was {answer} ");

            Console.ReadKey();
        }

        private static Colour[] ConvertInputToGuess(string input)
        {

            var guess = new Colour[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                var convertSuccess = int.TryParse(input[i].ToString(), out var convertedInput);

                guess[i] = !convertSuccess ? throw new GuessInvalidException($"'{input[i]}' is not a valid input") : (Colour)convertedInput;
            }

            return guess;
        }
    }
}
