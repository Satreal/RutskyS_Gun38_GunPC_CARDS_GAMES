using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask.Games.Dice
{
    public struct Dice
    {
        
        private int _min;
        private int _max;
        private static Random _random = new Random();
        public readonly int Number { get; }
            
        public Dice(int min, int max)
        {
            if (min< 1||max>int.MaxValue||max<=min)
            {
                throw new WrongDiceNumberException(min, 1, int.MaxValue);
            }
            
            
            _min = min;
            _max = max;
            Number = _random.Next(min,max+1);    
        }
        


    }
}
