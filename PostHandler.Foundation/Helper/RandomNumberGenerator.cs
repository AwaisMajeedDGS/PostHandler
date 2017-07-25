namespace PostHandler.Foundation.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Thread-safe random number generator class
    /// </summary>
    public class RandomNumberGenerator
    {
        readonly ThreadLocal<Random> random =
        new ThreadLocal<Random>(() => new Random(GetSeed()));
        private int _minValue = 1000;
        private int _maxValue = 2500;

        public RandomNumberGenerator()
        {

        }

        public RandomNumberGenerator(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public int Rand()
        {
            return random.Value.Next(_minValue, _maxValue);
        }

        static int GetSeed()
        {
            return Environment.TickCount * Thread.CurrentThread.ManagedThreadId;
        }
    }
}
