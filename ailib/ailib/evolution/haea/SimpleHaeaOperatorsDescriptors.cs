using ailib.math.statistic;
using ailib.services.description;

namespace ailib.evolution.haea
{
    public class SimpleHaeaOperatorsDescriptors<T> : Descriptors<HaeaOperators<T>>
    {
        public double[] describe(HaeaOperators<T> operators)
        {
            double[][] rates = new double[operators.getRates().Count][];
            for (int i = 0; i < rates.Length; i++)
                rates[i] = operators.getRates(i);
            Statistics[] stat = statistics(rates);
            double[] avg = new double[stat.Length];
            for (int i = 0; i < avg.Length; i++)
                avg[i] = stat[i].avg;
            return avg;
        }

        public static Statistics[] statistics(double[][] x)
        {
            int m = x[0].Length;
            Statistics[] s = new Statistics[m];
            for (int j = 0; j < m; j++)
                s[j] = new Statistics(x, j);
            return s;
        }
    }
}
