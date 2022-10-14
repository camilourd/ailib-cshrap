using ailib.io;
using ailib.math.activation_functions;
using ailib.math.ann.layer;
using System;
using System.IO;

namespace ailib.ann.recurrent
{
    public class LSTMCell : RecurentUnit
    {
        Layer1D F;
        Layer1D I;
        Layer1D C;
        Layer1D O;

        public LSTMCell(int inputSize, int outputSize)
        {
            this.F = new Layer1D(inputSize + outputSize, outputSize);
            this.I = new Layer1D(inputSize + outputSize, outputSize);
            this.C = new Layer1D(inputSize + outputSize, outputSize);
            this.O = new Layer1D(inputSize + outputSize, outputSize);
            C.setActivationFunction(new Tanh());
        }

        public LSTMCell(Layer1D F, Layer1D I, Layer1D C, Layer1D O)
        {
            this.F = F;
            this.I = I;
            this.C = C;
            this.O = O;
            C.setActivationFunction(new Tanh());
        }

        public double[][] propagate(double[] input, double[] output, double[] previous)
        {
            double[] result = new double[output.Length];
            double[] data = join(input, output);
            double[] f_t = F.propagate(data);
            double[] i_t = I.propagate(data);
            double[] c_t = C.propagate(data);
            double[] o_t = O.propagate(data);
            for (int i = 0; i < output.Length; i++)
            {
                c_t[i] = (f_t[i] * previous[i]) + (i_t[i] * c_t[i]);
                result[i] = o_t[i] * Math.Tanh(c_t[i]);
            }
            return new double[][] { result, c_t };
        }

        public static LSTMCell read(Scanner reader)
        {
            Layer1D F = Layer1D.read(reader);
            Layer1D I = Layer1D.read(reader);
            Layer1D C = Layer1D.read(reader);
            Layer1D O = Layer1D.read(reader);
            return new LSTMCell(F, I, C, O);
        }

        public override int length()
        {
            return F.length() + I.length() + C.length() + O.length();
        }

        public override void save(StreamWriter writer)
        {
            F.save(writer);
            writer.Write(" ");
            I.save(writer);
            writer.Write(" ");
            C.save(writer);
            writer.Write(" ");
            O.save(writer);
        }

        public override int update(double[] values, int index)
        {
            return O.update(values, C.update(values, I.update(values, F.update(values, index))));
        }

        public override int upload(double[] values, int index)
        {
            return O.upload(values, C.upload(values, I.upload(values, F.upload(values, index))));
        }
    }
}
