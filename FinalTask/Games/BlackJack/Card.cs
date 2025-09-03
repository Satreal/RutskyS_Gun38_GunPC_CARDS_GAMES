
namespace FinalTask.Games.BlackJack
{
    internal readonly struct Card
    {
        public readonly CardSuit Suit { get; }
        public readonly CardValues Value { get; }
        public Card(CardSuit suit, CardValues value)
        {
            Suit = suit;
            Value = value;
        }
        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}
