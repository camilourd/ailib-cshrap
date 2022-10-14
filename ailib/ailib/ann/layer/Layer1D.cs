using System;
using System.IO;
using ailib.io;
using ailib.math.ann.perceptron;

namespace ailib.math.ann.layer
{
    public class Layer1D : Layer<Perceptron1D, double[], double[]>
    {
        public Layer1D(int inputSize, int layerSize) : this(Layer1D.generateNeurons(inputSize, layerSize))
        {
        }

        public Layer1D(Perceptron1D[] neurons) : base(neurons)
        {
        }

        public static Perceptron1D[] generateNeurons(int inputSize, int layerSize)
        {
            Perceptron1D[] neurons = new Perceptron1D[layerSize];
            for (int i = 0; i < layerSize; i++)
                neurons[i] = new Perceptron1D(inputSize);
            return neurons;
        }

        public override double[] activate(double[] winput)
        {
            double[] result = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].activate(winput[i]);
            return result;
        }

        public override double[] propagate(double[] input)
        {
            double[] result = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].propagate(input);
            return result;
        }

        public override double[] weighted_input(double[] input)
        {
            double[] result = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].weighted_input(input);
            return result;
        }

        public static Layer1D read(Scanner reader)
        {
            int size = reader.nextInt();
            Perceptron1D[] neurons = new Perceptron1D[size];
            for (int i = 0; i < size; i++)
                neurons[i] = Perceptron1D.read(reader);
            return new Layer1D(neurons);
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
            foreach (Perceptron1D perceptron in neurons)
                perceptron.setActivationFunction(function);
        }

        public int length()
        {
            int sum = 0;
            foreach (Perceptron1D perceptron in neurons)
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
