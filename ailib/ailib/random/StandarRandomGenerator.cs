using System;

namespace ailib.random
{
    public class StandarRandomGenerator : RandomGenerator<double>
    {
        Random random = new Random();

        public double next()
        {
            return random.NextDouble();
        }

        public double[] next(int total)
        {
            double[] result = new double[total];
            for (int i = 0; i < total; i++)
                result[i] = random.NextDouble();
            return result;
        }

        public Random getGenerator()
        {
            return this.random;
        }
    }
}
