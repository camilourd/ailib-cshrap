using ailib.math.ann.perceptron;
using ailib.utils;
using System;
using ailib.io;
using System.IO;

namespace ailib.math.ann.layer
{
    public class ConvolutionalLayer : Layer<Perceptron2D[], double[][], double[][]>
    {
        protected Dimension stride;

        public ConvolutionalLayer(Dimension inputSize, Dimension window, Dimension stride) : base(ConvolutionalLayer.generateNeurons(inputSize, window, stride))
        {
            this.stride = stride;
        }

        public ConvolutionalLayer(Perceptron2D[][] neurons, Dimension stride) : base(neurons)
        {
            this.stride = stride;
        }

        private static Perceptron2D[][] generateNeurons(Dimension inputSize, Dimension window, Dimension stride)
        {
            Dimension ldim = calculateLayerDimensions(inputSize, window, stride);
            Perceptron2D[][] neurons = new Perceptron2D[ldim.height][];
            for(int i = 0; i < ldim.height; i++)
            {
                neurons[i] = new Perceptron2D[ldim.width];
                for (int j = 0; j < ldim.width; j++)
                    neurons[i][j] = new Perceptron2D(window);
            }
            return neurons;
        }

        internal int width()
        {
            return neurons[0].Length;
        }

        internal int height()
        {
            return neurons.Length;
        }

        public static Dimension calculateLayerDimensions(Dimension inputSize, Dimension window, Dimension stride)
        {
            return new Dimension(
                calculateDimension(inputSize.width, window.width, stride.width),
                calculateDimension(inputSize.height, window.height, stride.height));
        }

        public static int calculateDimension(int inputSize, int windowSize, int stride)
        {
            return (int) Math.Ceiling((inputSize - windowSize + 1) / stride * 1.0);
        }

        public override double[][] activate(double[][] winput)
        {
            double[][] result = new double[neurons.Length][];
            for(int i = 0; i < neurons.Length; i++)
            {
                result[i] = new double[neurons[i].Length];
                for (int j = 0; j < neurons[i].Length; j++)
                    result[i][j] = neurons[i][j].activate(winput[i][j]);
            }
            return result;
        }

        public override double[][] propagate(double[][] input)
        {
            double[][] result = new double[neurons.Length][];
            for (int i = 0, y = 0; i < neurons.Length; i++, y += stride.height)
            {
                result[i] = new double[neurons[i].Length];
                for (int j = 0, x = 0; j < neurons[i].Length; j++, x += stride.width)
                    result[i][j] = neurons[i][j].propagate(new Location(x, y), input);
            }
            return result;
        }

        public override double[][] weighted_input(double[][] input)
        {
            double[][] result = new double[neurons.Length][];
            for (int i = 0, y = 0; i < neurons.Length; i++, y += stride.height)
            {
                result[i] = new double[neurons[i].Length];
                for (int j = 0, x = 0; j < neurons[i].Length; j++, x += stride.width)
                    result[i][j] = neurons[i][j].weighted_input(new Location(x, y), input);
            }
            return result;
        }

        public static ConvolutionalLayer read(Scanner reader)
        {
            int height = reader.nextInt();
            int width = reader.nextInt();
            Perceptron2D[][] neurons = new Perceptron2D[height][];
            for (int i = 0; i < height; i++)
            {
                neurons[i] = new Perceptron2D[width];
                for (int j = 0; j < width; j++)
                    neurons[i][j] = Perceptron2D.read(reader);
            }
            return new ConvolutionalLayer(neurons, Dimension.read(reader));
        }

        public int update(double[] values, int index)
        {
            for (int i = 0; i < neurons.Length; i++)
                for (int j = 0; j < neurons[i].Length; j++)
                    index = neurons[i][j].update(values, index);
            return index;
        }

        public int upload(double[] values, int index)
        {
            for (int i = 0; i < neurons.Length; i++)
                for (int j = 0; j < neurons[i].Length; j++)
                    index = neurons[i][j].upload(values, index);
            return index;
        }

        public int length()
        {
            int sum = 0;
            foreach (Perceptron2D[] row in neurons)
                foreach (Perceptron2D perceptron in row)
                    sum += perceptron.length();
            return sum;
        }

        public void save(StreamWriter writer)
        {
            writer.Write(neurons.Length + " " + neurons[0].Length + " ");
            for (int i = 0; i < neurons.Length; i++)
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    neurons[i][j].save(writer);
                    writer.Write(" ");
                }
            stride.save(writer);
        }

        public void setActivationFunction(ActivationFunction function)
        {
            foreach (Perceptron2D[] row in neurons)
                foreach (Perceptron2D perceptron in row)
                    perceptron.setActivationFunction(function);
        }
    }
}