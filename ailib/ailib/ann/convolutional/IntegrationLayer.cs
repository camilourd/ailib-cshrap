using System;
using System.IO;
using ailib.io;
using ailib.math;
using ailib.math.ann;
using ailib.math.ann.perceptron;
using ailib.utils;

namespace ailib.ann.convolutional
{
    public class IntegrationLayer : Layer<Perceptron3D, double[][][], double[]>
    {
        public IntegrationLayer(Dimension inputSize, int layerSize) : base(generateNeurons(inputSize, layerSize))
        {
        }

        public IntegrationLayer(Perceptron3D[] neurons) : base(neurons)
        {
        }

        private static Perceptron3D[] generateNeurons(Dimension inputSize, int layerSize)
        {
            Perceptron3D[] neurons = new Perceptron3D[layerSize];
            for (int i = 0; i < layerSize; i++)
                neurons[i] = new Perceptron3D(inputSize);
            return neurons;
        }

        public override double[] activate(double[] winput)
        {
            double[] result = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].activate(winput[i]);
            return result;
        }

        public override double[] propagate(double[][][] input)
        {
            double[] result = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].propagate(input);
            return result;
        }

        public override double[] weighted_input(double[][][] input)
        {
            double[] result = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].weighted_input(input);
            return result;
        }

        public static IntegrationLayer read(Scanner reader)
        {
            int size = reader.nextInt();
            Perceptron3D[] neurons = new Perceptron3D[size];
            for (int i = 0; i < size; i++)
                neurons[i] = Perceptron3D.read(reader);
            return new IntegrationLayer(neurons);
        }

        public void save(StreamWriter writer)
        {
            writer.Write(neurons.Length);
            for (int i = 0; i < neurons.Length; i++)
            {
                writer.Write(" ");
                neurons[i].save(writer);
            }
        }

        public void setActivationFunction(ActivationFunction function)
        {
            foreach (Perceptron3D perceptron in neurons)
                perceptron.setActivationFunction(function);
        }

        public int length()
        {
            int sum = 0;
            foreach (Perceptron3D perceptron in neurons)
                sum += perceptron.length();
            return sum;
        }

        public int update(double[] values, int index)
        {
            for (int i = 0; i < neurons.Length; i++)
                index = neurons[i].update(values, index);
            return index;
        }

        public int upload(double[] values, int index)
        {
            for (int i = 0; i < neurons.Length; i++)
                index = neurons[i].upload(values, index);
            return index;
        }
    }
}
