using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Helpers
{
    internal class RandomHelper
    {
        private static readonly Random _random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
