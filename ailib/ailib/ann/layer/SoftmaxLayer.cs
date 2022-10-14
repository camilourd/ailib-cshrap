using ailib.io;
using ailib.math;
using ailib.math.activation_functions;
using ailib.math.ann;
using ailib.math.ann.layer;
using ailib.math.ann.perceptron;
using System;
using System.IO;

namespace ailib.ann.layer
{
    public class SoftmaxLayer : Layer<Perceptron1D, double[], double[]>
    {
        public SoftmaxLayer(int inputSize, int layerSize) : this(Layer1D.generateNeurons(inputSize, layerSize))
        {
        }

        public SoftmaxLayer(Perceptron1D[] neurons) : base(neurons)
        {
            ActivationFunction function = new Exponential();
            foreach (Perceptron1D neuron in neurons)
                neuron.setActivationFunction(function);
        }

        public override double[] activate(double[] winput)
        {
            double[] result = new double[neurons.Length];
            double sum = 0;
            for (int i = 0; i < neurons.Length; i++)
            {
                result[i] = neurons[i].activate(winput[i]);
                sum += result[i];
            }
            for (int i = 0; i < neurons.Length; i++)
                result[i] /= sum;
            return result;
        }

        public override double[] propagate(double[] input)
        {
            double[] result = new double[neurons.Length];
            double sum = 0;
            for (int i = 0; i < neurons.Length; i++)
            {
                result[i] = neurons[i].propagate(input);
                sum += result[i];
            }
            for (int i = 0; i < neurons.Length; i++)
                result[i] /= sum;
            return result;
        }

        public override double[] weighted_input(double[] input)
        {
            double[] result = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].weighted_input(input);
            return result;
        }

        public static SoftmaxLayer read(Scanner reader)
        {
            int size = reader.nextInt();
            Perceptron1D[] neurons = new Perceptron1D[size];
            for (int i = 0; i < size; i++)
                neurons[i] = Perceptron1D.read(reader);
            return new SoftmaxLayer(neurons);
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

        public int length()
        {
            int sum = 0;
            foreach (Perceptron1D perceptron in neurons)
                sum += perceptron.length();
            return sum;
        }
    }
}
