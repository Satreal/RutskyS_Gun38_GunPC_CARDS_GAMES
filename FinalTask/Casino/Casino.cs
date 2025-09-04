using FinalTask.Profile;
using FinalTask.Service;
using FinalTask.Games.BlackJack;
using FinalTask.Games.Dice;

namespace FinalTask.Casino
{
    public class Casino : IGame
    {
        private readonly ISaveLoadService<ProfilePlayer> _saveLoadService;
        private ProfilePlayer _player;
        private string _fileName;
        public Casino(ISaveLoadService<ProfilePlayer> saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
        public void StartGame()
        {
            Console.WriteLine("Добро пожаловать в самопальное казино!:");
            Console.WriteLine("Введите имя игрока для загрузки профиля:");
            string playerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                playerName = "Player";
            }
            _fileName = playerName;

            try
            {
                _player = _saveLoadService.LoadData(_fileName);
                Console.WriteLine($" Профиль загружен: {_player.Name}, банк: {_player.Bank}");
            }
            catch
            {
                Console.WriteLine("Создаем новый профиль");
                _player = new ProfilePlayer(playerName);
                Console.WriteLine($"Создан новый профиль: {_player.Name}, банк: {_player.Bank}");
                _saveLoadService.SaveData(_player, _fileName);
            }
            Console.WriteLine($"{_player.Name}, дружок, выбери игру: 1 - Блэкджек, 2 - Игра в кости");
            string input = "";
            do
            {
                input = Console.ReadLine();
                if(input!="1" && input != "2")
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, выберите 1 или 2.");
                }
               
            } while (input != "1" && input != "2");
            if (input == "1")
            {
                BlackJackGame();
            }
            else
            {
                DiceGame();
            }
        }
        private void BlackJackGame()
        {
            if (_player.Bank <= 0)
            {
                Console.WriteLine("No money? Kicked!");
                EndGame();
                return;
            }
            int betPlayer = Bet();
            Console.WriteLine("Введите количество карт (в демо версии работает только 36)");
            var game = new BlackJackGame(int.Parse(Console.ReadLine()));
           
            game.OnWin += () =>
            {
                _player.Bank += betPlayer;
                Console.WriteLine($"Вы выиграли! Ваш банк теперь: {_player.Bank}");
                MoneyLimits();
            };
            game.OnLoose += () =>
            {
                _player.Bank -= betPlayer;
                Console.WriteLine($"Вы проиграли! Ваш банк теперь: {_player.Bank}");
                MoneyLimits();
            };
            game.OnDraw += () =>
            {
                Console.WriteLine($"Ничья! Ваш банк остался: {_player.Bank}");
                MoneyLimits();
            };
            game.PlayGame();
            EndGame();
        }
        private void DiceGame()
        {
            if (_player.Bank <= 0)
            {
                Console.WriteLine("No money? Kicked!");
                EndGame();
                return;
            }
            Console.WriteLine("Вы выбрали игру в кости.");
            int betPlayer = Bet();
            Console.WriteLine("Введите количество костей");
            int countDice = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите минимальное значени на кости");
            int min = int.Parse(Console.ReadLine());
            Console.WriteLine("и максимальное значение на кости:");
            int max = int.Parse(Console.ReadLine());
            var game = new DiceGame(countDice, min, max);
           
            game.OnWin += () =>
            {
                _player.Bank += betPlayer;
                Console.WriteLine($"Вы выиграли! Ваш банк теперь: {_player.Bank}");
                MoneyLimits();
            };
            game.OnLoose += () =>
            {
                _player.Bank -= betPlayer;
                Console.WriteLine($"Вы проиграли! Ваш банк теперь: {_player.Bank}");
                MoneyLimits();
            };
            game.OnDraw += () =>
            {
                Console.WriteLine($"Ничья! Ваш банк остался: {_player.Bank}");
                MoneyLimits();
            };
            game.PlayGame();
            EndGame();
        }
        private void MoneyLimits()
        {
            if (_player.Bank <= 0)
            {
                Console.WriteLine("No money? Kicked!");
                EndGame();
                
            }
            else if (_player.Bank > int.MaxValue)
            {
                Console.WriteLine("You wasted half of your bank money in casino’s bar");
                _player.Bank = _player.Bank / 2;
            }
        }
        private int Bet()
        {
            int bet = 0;
            while (true)
            {
                Console.WriteLine($"Ваш банк: {_player.Bank}. Введите сумму ставки:");

                if (int.TryParse(Console.ReadLine(), out bet) && bet > 0 && bet <= _player.Bank)
                {
                    return bet;
                }
                else
                {
                    Console.WriteLine("Ставка должна быть положительным числом и не превышать ваш банк.");
                }
            }
        }
        public void EndGame()
        {
            Console.WriteLine("Игра окончена. Сохраняем профиль...");
            _saveLoadService.SaveData(_player, _fileName);
            Console.WriteLine("Профиль сохранен. До новых встреч! Нажми любую клавишу для выхода");
            Console.ReadKey();
            return;
        }
    }
}

