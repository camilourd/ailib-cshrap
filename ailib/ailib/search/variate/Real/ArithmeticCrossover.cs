using ailib.random;
using System;

namespace ailib.search.variate.Real
{
    public class ArithmeticCrossover : Variation_2_m<double[]>
    {
        RandomGenerator<double> generator;
        double min, max;

        public ArithmeticCrossover(RandomGenerator<double> generator) : this(generator, double.MinValue, double.MaxValue)
        {
        }

        public ArithmeticCrossover(RandomGenerator<double> generator, double min, double max)
        {
            this.generator = generator;
            this.min = min;
            this.max = max;
        }

        public override double[][] apply(double[] parent1, double[] parent2)
        {
            double[] child1 = new double[parent1.Length];
            double[] child2 = new double[parent1.Length];
            double alpha = generator.next();
            for (int i = 0; i < child1.Length; i++)
            {
                child1[i] = Math.Min(max, Math.Max(min, (alpha * parent1[i]) + ((1.0 - alpha) * parent2[i])));
                child2[i] = Math.Min(max, Math.Max(min, (alpha * parent2[i]) + ((1.0 - alpha) * parent1[i])));
            }
            return new double[][] { child1, child2 };
        }

        public override int range_arity()
        {
            return 2;
        }
    }
}
