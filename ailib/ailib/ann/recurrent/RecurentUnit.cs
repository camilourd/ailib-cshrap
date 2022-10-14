using System.IO;

namespace ailib.ann.recurrent
{
    public abstract class RecurentUnit
    {
        public double[] multiply(double[] arr1, double[] arr2)
        {
            double[] result = new double[arr1.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = arr1[i] * arr2[i];
            return result;
        }

        public double[] join(double[] input, double[] output)
        {
            double[] result = new double[input.Length + output.Length];
            for (int i = 0; i < input.Length; i++)
                result[i] = input[i];
            for (int i = 0; i < output.Length; i++)
                result[input.Length + i] = output[i];
            return result;
        }

        public abstract int length();
        public abstract void save(StreamWriter writer);
        public abstract int update(double[] values, int index);
        public abstract int upload(double[] values, int index);
    }
}
