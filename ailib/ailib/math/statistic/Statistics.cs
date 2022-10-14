using System;

namespace ailib.math.statistic
{
    public class Statistics
    {
        public int minIndex = 0;
        public int maxIndex = 0;
        public double min = 0.0;
        public double max = 0.0;
        public double avg = 0.0;
        public double variance = 0.0;
        public double deviation = 0.0;

        public Statistics(double[] x)
        {
            int n = x.Length;
            min = max = avg = x[0];
            minIndex = maxIndex = 0;
            for (int i = 1; i < n; i++)
            {
                if (x[i] < min)
                {
                    min = x[i];
                    minIndex = i;
                }
                else if (x[i] > max)
                {
                    max = x[i];
                    maxIndex = i;
                }
                avg += x[i];
            }
            avg /= n;
            for (int i = 0; i < n; i++)
                variance += (x[i] - avg) * (x[i] - avg);
            variance /= (n > 1) ? (n - 1) : 1.0;
            deviation = Math.Sqrt(variance);
        }

        public Statistics(double[][] x, int c)
        {
            int n = x.Length;
            min = max = avg = x[0][c];
            for (int i = 1; i < n; i++)
            {
                if (x[i][c] < min)
                {
                    min = x[i][c];
                    minIndex = i;
                }
                else if (x[i][c] > max)
                {
                    max = x[i][c];
                    maxIndex = i;
                }
                avg += x[i][c];
            }
            avg /= n;
            for (int i = 0; i < n; i++)
                variance += (x[i][c] - avg) * (x[i][c] - avg);
            variance /= (n > 1) ? (n - 1) : 1.0;
            deviation = Math.Sqrt(variance);
        }

        public virtual double[] get()
        {
            double[] values = new double[7];
            values[0] = minIndex;
            values[1] = maxIndex;
            values[2] = min;
            values[3] = max;
            values[4] = avg;
            values[5] = variance;
            values[6] = deviation;
            return values;
        }
    }
}
