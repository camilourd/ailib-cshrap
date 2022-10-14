using System;
using ailib.utils;
using ailib.io;
using System.IO;

namespace ailib.math.ann.perceptron
{
    public class Perceptron1D : Perceptron<double, double[]>
    {
        public Perceptron1D(int size) : base(Perceptron1D.generateWeights(size), Perceptron1D.generateBeta())
        {
        }

        public Perceptron1D(int size, ActivationFunction function) : base(Perceptron1D.generateWeights(size), Perceptron1D.generateBeta(), function)
        {
        }

        public Perceptron1D(double[] weights, double beta) : base(weights, beta)
        {
        }

        private static double[] generateWeights(int size)
        {
            Random random = new Random();
            double[] weights = new double[size];
            for (int i = 0; i < weights.Length; i++)
                weights[i] = 6.0 * random.NextDouble() - 3.0;
            return weights;
        }

        private static double generateBeta()
        {
            Random random = new Random();
            return (2.0 * random.NextDouble()) - 1.0;
        }

        public override double weighted_input(Location start, double[] input)
        {
            double sum = 0.0;
            for (int i = 0; i < weights.Length; i++)
                sum += weights[i] * input[start.x + i];
            return sum;
        }

        public int size()
        {
            return weights.Length;
        }

        public static Perceptron1D read(Scanner reader)
        {
            int size = reader.nextInt();
            double[] weights = new double[size];
            for (int i = 0; i < weights.Length; i++)
                weights[i] = reader.nextDouble();
            double beta = reader.nextDouble();
            return new Perceptron1D(weights, beta);
        }

        public void save(StreamWriter writer)
        {
            writer.Write(weights.Length);
            for (int i = 0; i < weights.Length; i++)
                writer.Write(" " + weights[i]);
            writer.Write(" " + beta);
        }

        public int update(double[] values, int index)
        {
            for (int i = 0; i < weights.Length; i++)
                weights[i] = values[index++];
            beta = values[index++];
            return index;
        }

        public int length()
        {
            return weights.Length + 1;
        }

        public int upload(double[] values, int index)
        {
            for (int i = 0; i < weights.Length; i++)
                values[index++] = weights[i];
            values[index++] = beta;
            return index;
        }
    }
}
