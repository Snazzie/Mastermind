using System;
using Mastermind;
using NSubstitute;
using NUnit.Framework;

namespace MastermindTests
{
    [TestFixture]
    public class MastermindTests
    {
        private readonly Colour[] _correctSequence = { Colour.Red, Colour.Blue, Colour.Red, Colour.Green };

        private Mastermind.Mastermind MastermindGame => new Mastermind.Mastermind();

        [Test]
        public void IsGuessCorrect_WithCorrectGuess_ReturnTrue()
        {
            var guess = _correctSequence;

            var result = Mastermind.Mastermind.IsGuessCorrect(guess, _correctSequence, out _);

            Assert.That(result, Is.EqualTo(true));
        }

        [TestCase(new[] { Colour.Green, Colour.Green, Colour.Green, Colour.Purple }, 1, 0, Description = "Incorrect")]
        [TestCase(new[] { Colour.Red, Colour.Green, Colour.Red, Colour.Purple }, 3, 2, Description = "Incorrect")]
        [TestCase(new[] { Colour.Red, Colour.Red, Colour.Red, Colour.Red }, 2, 2, Description = "Incorrect")]
        [TestCase(new[] { Colour.Purple, Colour.Purple, Colour.Purple, Colour.Purple }, 0, 0, Description = "Incorrect")]
        [TestCase(new[] { Colour.Red, Colour.Blue, Colour.Red, Colour.Green }, 4, 4, Description = "Correct")]
        public void IsGuessCorrect_ReturnCorrectHints(Colour[] guess, int expectedCorrectColours, int expectedCorrectPlacement)
        {
            var expectedHint = new Tuple<int, int>(expectedCorrectColours, expectedCorrectPlacement);

            Mastermind.Mastermind.IsGuessCorrect(guess, _correctSequence, out var hint);

            Assert.That(hint, Is.EqualTo(expectedHint));
        }

        [TestCase(new[] {Colour.Green, Colour.Green, Colour.Green}, Description = "Not correct colour count")]
        [TestCase(null, Description = "null guess")]
        public void TryGuess_GivenIncorrectGuessFormats_ThrowsGuessInvalidException(Colour[] guess)
        {
            Assert.Throws<GuessInvalidException>(() => MastermindGame.TryGuess(guess));
        }
    }
}
