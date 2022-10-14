using ailib.math.ann;
using ailib.math.ann.layer;
using ailib.utils;
using ailib.io;
using System;
using System.IO;
using ailib.math;

namespace ailib.ann.convolutional
{
    public class PoolingField : Layer<ConvolutionalLayer, double[][][], double[][][]>
    {
        public PoolingField(Dimension inputSize, Dimension window, int size) : base(ReceptiveField.generateLayers(inputSize, window, new Dimension(1, 1), size))
        {
        }

        public PoolingField(ConvolutionalLayer[] neurons) : base(neurons)
        {
        }

        public override double[][][] propagate(double[][][] input)
        {
            double[][][] result = new double[neurons.Length][][];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].propagate(input[i]);
            return result;
        }

        internal Dimension getDimension()
        {
            return new Dimension(neurons[0].width(), neurons[0].height(), neurons.Length);
        }

        public override double[][][] weighted_input(double[][][] input)
        {
            double[][][] result = new double[neurons.Length][][];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].weighted_input(input[i]);
            return result;
        }

        public override double[][][] activate(double[][][] winput)
        {
            double[][][] result = new double[neurons.Length][][];
            for (int i = 0; i < neurons.Length; i++)
                result[i] = neurons[i].activate(winput[i]);
            return result;
        }

        internal static PoolingField read(Scanner reader)
        {
            int size = reader.nextInt();
            ConvolutionalLayer[] layers = new ConvolutionalLayer[size];
            for (int i = 0; i < size; i++)
                layers[i] = ConvolutionalLayer.read(reader);
            return new PoolingField(layers);
        }

        internal void save(StreamWriter writer)
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
            foreach (ConvolutionalLayer perceptron in neurons)
                perceptron.setActivationFunction(function);
        }

        public int length()
        {
            int sum = 0;
            foreach (ConvolutionalLayer perceptron in neurons)
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
