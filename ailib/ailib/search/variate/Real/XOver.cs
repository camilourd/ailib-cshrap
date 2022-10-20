using ailib.random;

namespace ailib.search.variate.Real
{
    public class XOver : Variation_2_m<double[]>
    {
        RandomGenerator<double> generator;

        public XOver(RandomGenerator<double> generator)
        {
            this.generator = generator;
        }

        public override double[][] apply(double[] parent1, double[] parent2)
        {
            double[] child1 = new double[parent1.Length];
            double[] child2 = new double[parent2.Length];
            int cut = (int)(parent1.Length * generator.next());
            for (int i = 0; i < parent1.Length; i++)
            {
                child1[i] = (i < cut) ? parent1[i] : parent2[i];
                child2[i] = (i < cut) ? parent2[i] : parent1[i];
            }
            return new double[][] { child1, child2 };
        }

        public override int range_arity()
        {
            return 2;
        }
    }
}
