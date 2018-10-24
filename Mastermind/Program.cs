using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    enum Colour
    {
        Red,
        Blue,
        Green,
        Purple,
        Yellow,
        White
    }

    class Mastermind
    {
        public int GuessesLeft { get; set; } = 10;
        public bool IsWinner { get; set; } = false;
        private Colour[] Sequence { get; set; } = new Colour[4];
        public Mastermind()
        {
            for (int i = 0; i < Sequence.Length; i++)
            {
                var rand = new Random(DateTime.Now.Millisecond);
                Sequence[i] = (Colour)rand.Next(0, Enum.GetValues(typeof(Colour)).Length);
            }
        }

        public Tuple<int, int> TryGuess(Colour[] guess)
        {
            int correctColors = 0, correctColorPlacements = 0;

            var seq = Sequence.ToList();

            for (int i = 0; i < Sequence.Length; i++)
            {
                if (seq.Contains(guess[i]))
                {
                    correctColors++;
                    seq.Remove(guess[i]);
                }

                if (Sequence[i] == guess[i])
                {
                    correctColorPlacements++;
                }
            }

            GuessesLeft--;
            if (correctColorPlacements == 4)
                IsWinner = true;
            return new Tuple<int, int>(correctColors, correctColorPlacements);
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            var game = new Mastermind();

            while (!game.IsWinner && game.GuessesLeft != 0)
            {
                Console.WriteLine("Red = 0, Blue = 1, Green = 2, Purple = 3, Yellow = 4, White = 5");
                var input = Console.ReadLine();

                Colour[] guess = new Colour[4];

                for (int i = 0; i < guess.Length; i++)
                {
                    guess[i] = (Colour)int.Parse(input[i].ToString());
                }

                var result = game.TryGuess(guess);
                if (game.IsWinner)
                {
                    Console.WriteLine("GG EZ");
                }
                else
                {
                    Console.WriteLine($"correct colours: {result.Item1}    correct placements: {result.Item2}    guesses left: {game.GuessesLeft}");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
