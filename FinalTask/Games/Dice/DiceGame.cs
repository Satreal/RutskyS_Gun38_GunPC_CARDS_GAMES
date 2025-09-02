using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask.Games.Dice
{
    public sealed class DiceGame : CasinoGameBase
    {
        private readonly int _countDice;
        private readonly int _min;
        private readonly int _max;
        private List<Dice> _playerDice = new List<Dice>();
        private List<Dice> _computerDice = new List<Dice>();

        public DiceGame(int countDice, int min, int max)
        {
            if (countDice < 1)
            {
                throw new ArgumentException("Количество костей должно быть больше 0.");
            }
            if (min < 1 || max > int.MaxValue || max <= min)
            {
                throw new WrongDiceNumberException(min, 1, int.MaxValue);
            }

            _countDice = countDice;
            _min = min;
            _max = max;
            FactoryMethod();
        }
        protected override void FactoryMethod()
        {
            for (int i = 0; i < _countDice; i++)
            {
                _playerDice.Add(new Dice(_min, _max));
                _computerDice.Add(new Dice(_min, _max));
            }
        }
        public override void PlayGame()
        {
            int playerSum = 0;
            int computerSum = 0;
            Console.WriteLine("Игра в кости началась!");
            Console.WriteLine("Игрок бросает кости...");
            foreach (var dice in _playerDice)
            {
                Console.WriteLine($"Выпало: {dice.Number}");
                playerSum += dice.Number;
            }
            Console.WriteLine($"Сумма очков игрока: {playerSum}");
            Console.WriteLine("Компьютер бросает кости...");
            foreach (var dice in _computerDice)
            {
                Console.WriteLine($"Выпало: {dice.Number}");
                computerSum += dice.Number;
            }
            Console.WriteLine($"Сумма очков компьютера: {computerSum}");
            if (playerSum > computerSum)
            {
                Result("Игрок выиграл!");
                OnWinInvoke();
            }
            else if (playerSum < computerSum)
            {
                Result("Компьютер выиграл!");
                OnLooseInvoke();
            }
            else
            {
                Result("Ничья!");
                OnDrawInvoke();
            }


        }



    }
}
