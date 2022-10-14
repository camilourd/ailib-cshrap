using ailib.io;
using ailib.math;
using ailib.math.ann.layer;
using ailib.utils;
using System;
using System.IO;

namespace ailib.ann.convolutional
{
    public class ConvolutionalNetwork : ANN<double[][], double[]>
    {
        protected ReceptiveField receptive;
        protected PoolingField pooling;
        protected IntegrationLayer integration;
        protected Layer1D outputLayer;

        public ConvolutionalNetwork(Dimension inputSize, Dimension window, int size, int outputSize)
        {
            this.receptive = new ReceptiveField(inputSize, window, new Dimension(1, 1), size);
            this.pooling = new PoolingField(
                    this.receptive.getDimension(),
                    new Dimension(2, 2),
                    size
                );
            this.integration = new IntegrationLayer(this.pooling.getDimension(), 20);
            this.outputLayer = new Layer1D(20, outputSize);
        }

        public ConvolutionalNetwork(ReceptiveField receptive, PoolingField pooling, IntegrationLayer integration, Layer1D outputLayer)
        {
            this.receptive = receptive;
            this.pooling = pooling;
            this.integration = integration;
            this.outputLayer = outputLayer;
        }

        public double[] propagate(double[][] input)
        {
            return outputLayer.propagate(integration.propagate(pooling.propagate(receptive.propagate(input))));
        }

        public static ConvolutionalNetwork read(string stream)
        {
            Scanner reader = new Scanner(stream);
            ReceptiveField receptive = ReceptiveField.read(reader);
            PoolingField pooling = PoolingField.read(reader);
            IntegrationLayer integration = IntegrationLayer.read(reader);
            Layer1D outputLayer = Layer1D.read(reader);
            return new ConvolutionalNetwork(receptive, pooling, integration, outputLayer);
        }

        public void save(string path)
        {
            StreamWriter writer = new StreamWriter(path);
            receptive.save(writer);
            writer.Write(" ");
            pooling.save(writer);
            writer.Write(" ");
            integration.save(writer);
            writer.Write(" ");
            outputLayer.save(writer);
            writer.Close();
        }

        public void update(double[] values)
        {
            int index = receptive.update(values, 0);
            index = pooling.update(values, index);
            index = integration.update(values, index);
            index = outputLayer.update(values, index);
        }

        public void setActivationFunction(ActivationFunction function)
        {
            receptive.setActivationFunction(function);
            pooling.setActivationFunction(function);
            integration.setActivationFunction(function);
            outputLayer.setActivationFunction(function);
        }

        public int length()
        {
            return receptive.length() + pooling.length() + integration.length() + outputLayer.length();
        }

        public double[] upload()
        {
            double[] values = new double[length()];
            int index = receptive.upload(values, 0);
            index = pooling.upload(values, index);
            index = integration.upload(values, index);
            index = outputLayer.upload(values, index);
            return values;
        }
    }
}
