using ailib.math.statistic;
using ailib.objects;
using ailib.search;
using ailib.services.description;

namespace ailib.evolution.population
{
    public class PopulationDescriptors<T> : BasicGoalBased<T, double>, Descriptors<T[]>, Descriptors<Tagged<T>[]>
    {
        public double[] describe(T[] pop)
        {
            double[] quality = new double[pop.Length];
            for (int i = 0; i < quality.Length; i++)
                quality[i] = goal.apply(pop[i]);
            return (new StatisticsWithMedian(quality)).get();
        }

        public double[] describe(Tagged<T>[] pop)
        {
            double[] quality = new double[pop.Length];
            for (int i = 0; i < quality.Length; i++)
                quality[i] = goal.apply(pop[i]);
            return (new StatisticsWithMedian(quality)).get();
        }
    }
}
