using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FinalTask.Games.BlackJack
{
    public sealed class BlackJackGame : CasinoGameBase
    {
        private readonly int _cardCount;
        private Queue<Card> _deck;
        private readonly Random _random = new Random();
        public BlackJackGame(int cardCount)

        {
            if (cardCount != 36)
            {
                cardCount = 36;
                throw new ArgumentException(nameof(cardCount), "Количество карт должно быть 36");
                
            }
            _cardCount = cardCount;
            FactoryMethod();
        }
        protected override void FactoryMethod()
        {
            List<Card> allCard = new();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValues value in Enum.GetValues(typeof(CardValues)))
                {
                    allCard.Add(new Card(suit, value));
                }
            }
            _deck = Shuffle(allCard);

        }
        private Queue<Card> Shuffle(List<Card> card)
        {
            Queue<Card> shuffledDeck = new Queue<Card>();
            for (int i = card.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (card[i], card[j]) = (card[j], card[i]);
            }
            foreach (Card c  in card)
            {
                shuffledDeck.Enqueue(c);
            }
            return shuffledDeck;
        }

        private int CalculatePoint(List<Card> cards)
        {
            int points = 0;
            foreach (var card in cards)
            {
                switch (card.Value)
                {
                    case CardValues.Six: points += 6; break;
                    case CardValues.Seven: points += 7; break;
                    case CardValues.Eight: points += 8; break;
                    case CardValues.Nine: points += 9; break;
                    case CardValues.Ten: points += 10; break;
                    case CardValues.Jack: points += 2; break;
                    case CardValues.Queen: points += 3; break;
                    case CardValues.King: points += 4; break;
                    case CardValues.Ace: points += 11; break;
                }
            }
            return points;
        }
        public override void PlayGame()
        {
            Console.WriteLine(" Игра BlackJack началась!\nСейчас будет раздача по две карты каждой стороне");
            List<Card> player = new List<Card>();//требуется внимание
            List<Card> computer = new List<Card>();


            player.Add(_deck.Dequeue());
            computer.Add(_deck.Dequeue());
            player.Add(_deck.Dequeue());
            computer.Add(_deck.Dequeue());

            int playerPoints = CalculatePoint(player);
            int computerPoints = CalculatePoint(computer);
            Console.WriteLine($"Ваши карты: {string.Join(", ", player)} (очков: {playerPoints})");
            while (playerPoints == computerPoints && playerPoints <= 21 && computerPoints <= 21)
            {
                Console.WriteLine("Игроки берут еще по одной карте\nНажми любую клавижу для раздачи" );
                Console.ReadKey();
                var playerCard = _deck.Dequeue();//возможно ошибка
                player.Add(playerCard);
                var computerCard = _deck.Dequeue();//возможно ошибка
                computer.Add(computerCard);

                playerPoints = CalculatePoint(player);
                computerPoints = CalculatePoint(computer);
                Console.WriteLine($"Выпала карта: {playerCard}.\n Ваши карты: {string.Join(", ", player)} (очков: {playerPoints})");
            }
            if (playerPoints <= 21 && (computerPoints > 21 || playerPoints > computerPoints))
            {
                Console.WriteLine("Вы победили!");
                OnWinInvoke();
            }
            else if (computerPoints <= 21 && (playerPoints > 21 || computerPoints > playerPoints))
            {
                Console.WriteLine("Вы проиграли! Не отчаивайся!");
                OnLooseInvoke();
            }
            else
            {
                Console.WriteLine("Боевая ничья!");
                OnDrawInvoke();
            }
        }
    }

}
