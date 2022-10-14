using System;

namespace ailib.random.real
{
    public class StandardPowerLaw : RandomGenerator<double>
    {
        RandomGenerator<double> generator;
        double coarse_alpha = -1.0;

        public StandardPowerLaw(RandomGenerator<double> generator, double alpha)
        {
            this.generator = generator;
            coarse_alpha = 1.0 / (1.0 - alpha);
        }

        public double next()
        {
            return Math.Pow(1.0 - generator.next(), coarse_alpha);
        }

        public double[] next(int total)
        {
            double[] result = new double[total];
            for (int i = 0; i < total; i++)
                result[i] = next();
            return result;
        }
    }
}
