using ailib.random;
using System;

namespace ailib.search.variate.Real
{
    public class Differential : Variation_4_m<double[]>
    {
        RandomGenerator<double> generator;
        double factor;
        double correction;
        double min, max;

        public Differential(double factor, double correction, RandomGenerator<double> generator) : this(factor, correction, generator, double.MinValue, double.MaxValue)
        {
        }

        public Differential(double factor, double correction, RandomGenerator<double> generator, double min, double max)
        {
            this.generator = generator;
            this.factor = factor;
            this.correction = correction;
            this.min = min;
            this.max = max;
        }

        public override double[][] apply(double[] p0, double[] p1, double[] p2, double[] p3)
        {
            return new double[][] {
                combine(p0, p1, p2, p3),
                combine(p0, p1, p3, p2),
                combine(p0, p2, p1, p3),
                combine(p0, p2, p3, p1),
                combine(p0, p3, p1, p2),
                combine(p0, p3, p2, p1)
            };
        }

        private double[] combine(double[] p0, double[] p1, double[] p2, double[] p3)
        {
            double[] sample = new double[p0.Length];
            for (int i = 0; i < sample.Length; i++)
                sample[i] = Math.Min(max, Math.Max(min, (generator.next() < correction) ? p3[i] + (factor * (p1[i] - p2[i])) : p0[i]));
            int cut = (int)(generator.next() * sample.Length);
            sample[cut] = Math.Min(max, Math.Max(min, p3[cut] + (factor * (p1[cut] - p2[cut]))));
            return sample;
        }

        public override int range_arity()
        {
            return 6;
        }
    }
}
