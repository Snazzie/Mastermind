namespace Mastermind
{
    public class Sequence : ISequence
    {
        public Colour[] Value { get; set; }
    }

    public enum Colour
    {
        Red,
        Blue,
        Green,
        Purple,
        Yellow,
        White
    }
}