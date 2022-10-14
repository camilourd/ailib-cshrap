namespace ailib.utils
{
    public class ArrayTools
    {
        public static double[] normalize(double[] rates)
        {
            double sum = 0;
            for (int i = 0; i < rates.Length; i++)
                sum += rates[i];
            double[] result = new double[rates.Length];
            for (int i = 0; i < rates.Length; i++)
                result[i] = rates[i] / sum;
            return result;
        }
    }
}
