using ailib.random;

namespace ailib.math.integer
{
    public class Roulette : RandomGenerator<int>
    {
        RandomGenerator<double> generator;
        protected double[] density;

        public Roulette(int n, RandomGenerator<double> generator)
        {
            this.generator = generator;
            density = new double[n];
            double total = n * (n + 1) / 2.0;
            for (int i = 0; i < n; i++)
                density[i] = (n - i) / total;
            setDensity(density);
        }

        public Roulette(double[] density, RandomGenerator<double> generator)
        {
            this.generator = generator;
            setDensity(density);
        }

        public void setDensity(double[] x)
        {
            density = new double[x.Length];
            for (int i = 0; i < density.Length; i++)
                density[i] = (i > 0) ? density[i - 1] + x[i] : x[i];
        }

        public int next()
        {
            double rnd = generator.next();
            for (int i = 0; i < density.Length; i++)
                if (rnd < density[i])
                    return i;
            return density.Length - 1;
        }

        public int[] next(int n)
        {
            int[] result = new int[n];
            for (int i = 0; i < n; i++)
                result[i] = next();
            return result;
        }
    }
}
