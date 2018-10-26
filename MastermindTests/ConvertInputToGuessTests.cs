using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mastermind;
using NUnit.Framework;

namespace MastermindTests
{
    [TestFixture]
    class ConvertInputToGuessTests
    {
        [TestCase("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [TestCase(@"`~@#$%^&*()[]{}_+-=""':;<>?,./\|")]
        public void Convert_WithNoneNumericInput_ThrowException(string input)
        {
            foreach (var letter in input)
            {
                Assert.Throws<ArgumentException>(() => ConvertInputToGuess.Convert(letter.ToString()));
            }           
        }
    }
}
