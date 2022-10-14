using System;

namespace ailib.math.statistic
{
    public class StatisticsWithMedian : Statistics
    {
        public double median;

        public StatisticsWithMedian(double[] x) : base(x)
        {
            computeMedian(x);
        }

        private void computeMedian(double[] x)
        {
            Array.Sort(copy(x));
            int n = x.Length;
            median = ((n % 2) == 0) ? (x[n / 2] + x[n / 2 - 1]) / 2.0 : x[n / 2];
        }

        private double[] copy(double[] x)
        {
            double[] result = new double[x.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = x[i];
            return result;
        }

        public override double[] get()
        {
            double[] values = new double[8];
            values[0] = minIndex;
            values[1] = maxIndex;
            values[2] = min;
            values[3] = max;
            values[4] = median;
            values[5] = avg;
            values[6] = variance;
            values[7] = deviation;
            return values;
        }
    }
}
