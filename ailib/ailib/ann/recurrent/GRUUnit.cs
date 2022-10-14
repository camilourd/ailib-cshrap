using ailib.io;
using ailib.math.activation_functions;
using ailib.math.ann.layer;
using System.IO;

namespace ailib.ann.recurrent
{
    public class GRUUnit : RecurentUnit
    {
        Layer1D updateGate;
        Layer1D resetGate;
        Layer1D memoryGate;

        public GRUUnit(int inputSize, int outputSize)
        {
            this.updateGate = new Layer1D(inputSize + outputSize, outputSize);
            this.resetGate = new Layer1D(inputSize + outputSize, outputSize);
            this.memoryGate = new Layer1D(inputSize + outputSize, outputSize);
            memoryGate.setActivationFunction(new Tanh());
        }

        public GRUUnit(Layer1D updateGate, Layer1D resetGate, Layer1D memoryGate)
        {
            this.updateGate = updateGate;
            this.resetGate = resetGate;
            this.memoryGate = memoryGate;
            memoryGate.setActivationFunction(new Tanh());
        }

        public double[] propagate(double[] input, double[] output)
        {
            double[] data = join(input, output);
            double[] z_t = updateGate.propagate(data);
            double[] r_t = resetGate.propagate(data);
            double[] hp_t = memoryGate.propagate(join(input, multiply(r_t, output)));
            double[] h_t = new double[output.Length];
            for (int i = 0; i < h_t.Length; i++)
                h_t[i] = (z_t[i] * output[i]) + ((1 - z_t[i]) * hp_t[i]);
            return h_t;
        }

        public static GRUUnit read(Scanner reader)
        {
            Layer1D updateGate = Layer1D.read(reader);
            Layer1D resetGate = Layer1D.read(reader);
            Layer1D memoryGate = Layer1D.read(reader);
            return new GRUUnit(updateGate, resetGate, memoryGate);
        }

        public override int length()
        {
            return updateGate.length() + resetGate.length() + memoryGate.length();
        }

        public override void save(StreamWriter writer)
        {
            updateGate.save(writer);
            writer.Write(" ");
            resetGate.save(writer);
            writer.Write(" ");
            memoryGate.save(writer);
        }

        public override int update(double[] values, int index)
        {
            return memoryGate.update(values, resetGate.update(values, updateGate.update(values, index)));
        }

        public override int upload(double[] values, int index)
        {
            return memoryGate.upload(values, resetGate.upload(values, updateGate.upload(values, index)));
        }
    }
}
