
namespace FinalTask.Games.Dice
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int value, int min, int max)
            : base($"Неверное значение кубика: {value}. Допустимые значения от {min} до {max}.")
        {
        }

    }
}
