using System;
using System.IO;
using ailib.io;
using ailib.utils;

namespace ailib.math.ann.perceptron
{
    public class Perceptron3D : Perceptron<Perceptron2D, double[][][]>
    {
        public Perceptron3D(Dimension dimension) : base(Perceptron3D.generateWeights(dimension), Perceptron3D.generateBeta())
        {
        }

        public Perceptron3D(Dimension dimension, ActivationFunction function) : base(Perceptron3D.generateWeights(dimension, function), Perceptron3D.generateBeta(), function)
        {
        }

        public Perceptron3D(Perceptron2D[] weights, double beta) : base(weights, beta)
        {
        }

        private static Perceptron2D[] generateWeights(Dimension dimension)
        {
            Perceptron2D[] weights = new Perceptron2D[dimension.length];
            for (int i = 0; i < dimension.length; i++)
                weights[i] = new Perceptron2D(dimension);
            return weights;
        }

        private static Perceptron2D[] generateWeights(Dimension dimension, ActivationFunction function)
        {
            Perceptron2D[] weights = new Perceptron2D[dimension.length];
            for (int i = 0; i < dimension.length; i++)
                weights[i] = new Perceptron2D(dimension, function);
            return weights;
        }

        private static double generateBeta()
        {
            Random random = new Random();
            return (2.0 * random.NextDouble()) - 1.0;
        }

        public override double weighted_input(Location start, double[][][] input)
        {
            double sum = 0.0;
            for (int i = 0; i < weights.Length; i++)
                sum += weights[i].weighted_input(start, input[start.z + i]);
            return sum;
        }

        public int width()
        {
            return weights[0].width();
        }

        public int height()
        {
            return weights[0].height();
        }

        public int size()
        {
            return weights.Length;
        }

        internal static Perceptron3D read(Scanner reader)
        {
            int size = reader.nextInt();
            Perceptron2D[] weights = new Perceptron2D[size];
            for (int i = 0; i < size; i++)
                weights[i] = Perceptron2D.read(reader);
            double beta = reader.nextDouble();
            return new Perceptron3D(weights, beta);
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
            foreach (Perceptron2D perceptron in weights)
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
