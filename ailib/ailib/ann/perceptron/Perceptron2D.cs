using System;
using System.IO;
using ailib.io;
using ailib.utils;

namespace ailib.math.ann.perceptron
{
    public class Perceptron2D : Perceptron<Perceptron1D, double[][]>
    {
        public Perceptron2D(Dimension dimension) : base(Perceptron2D.generateWeights(dimension), Perceptron2D.generateBeta())
        {
        }

        public Perceptron2D(Dimension dimension, ActivationFunction function) : base(Perceptron2D.generateWeights(dimension, function), Perceptron2D.generateBeta(), function)
        {
        }

        public Perceptron2D(Perceptron1D[] weights, double beta) : base(weights, beta)
        {
        }

        private static Perceptron1D[] generateWeights(Dimension dimension)
        {
            Perceptron1D[] weights = new Perceptron1D[dimension.height];
            for (int i = 0; i < dimension.height; i++)
                weights[i] = new Perceptron1D(dimension.width);
            return weights;
        }

        private static double generateBeta()
        {
            Random random = new Random();
            return (2.0 * random.NextDouble()) - 1.0;
        }

        private static Perceptron1D[] generateWeights(Dimension dimension, ActivationFunction function)
        {
            Perceptron1D[] weights = new Perceptron1D[dimension.height];
            for (int i = 0; i < dimension.height; i++)
                weights[i] = new Perceptron1D(dimension.width, function);
            return weights;
        }

        public override double weighted_input(Location start, double[][] input)
        {
            double sum = 0.0;
            for (int i = 0; i < weights.Length; i++)
                sum += weights[i].weighted_input(start, input[start.y + i]);
            return sum;
        }

        public int width()
        {
            return weights[0].size();
        }

        public int height()
        {
            return weights.Length;
        }

        public static Perceptron2D read(Scanner reader)
        {
            int height = reader.nextInt();
            Perceptron1D[] weights = new Perceptron1D[height];
            for (int i = 0; i < height; i++)
                weights[i] = Perceptron1D.read(reader);
            double beta = reader.nextDouble();
            return new Perceptron2D(weights, beta);
        }

        public void save(StreamWriter writer)
        {
            writer.Write(weights.Length);
            for (int i = 0; i < weights.Length; i++)
            {
                writer.Write(" ");
                weights[i].save(writer);
            }
            writer.Write(" " + beta);
        }

        public int update(double[] values, int index)
        {
            for (int i = 0; i < weights.Length; i++)
                index = weights[i].update(values, index);
            beta = values[index++];
            return index;
        }

        public int length()
        {
            int sum = 0;
            foreach (Perceptron1D perceptron in weights)
                sum += perceptron.length();
            return sum + 1;
        }

        public int upload(double[] values, int index)
        {
            for (int i = 0; i < weights.Length; i++)
                index = weights[i].upload(values, index);
            values[index++] = beta;
            return index;
        }
    }
}
