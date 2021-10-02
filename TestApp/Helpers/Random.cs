using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Helpers
{
    public class RandomNumber
    {
        public static decimal Decimal(int min, int max)
        {
            Random rnd = new();
            int randomInt = rnd.Next(min, max);

            double randomDouble = rnd.Next(0, 999999999);
            return Convert.ToDecimal(randomInt) + Convert.ToDecimal((randomDouble / 1000000000));
        }

        public static int Int(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
