using ailib.io;
using System;
using System.IO;

namespace ailib.ann.recurrent
{
    public class LSTM : ANN<double[][], double[]>
    {
        LSTMCell[] strap;
        int outputSize;

        public static int RESULT = 0;
        public static int STATE = 1;

        public LSTM(int inputSize, int outputSize, int total)
        {
            this.outputSize = outputSize;
            strap = new LSTMCell[total];
            for (int i = 0; i < strap.Length; i++)
                strap[i] = new LSTMCell(inputSize, outputSize);
        }

        public LSTM(LSTMCell[] strap, int outputSize)
        {
            this.strap = strap;
            this.outputSize = outputSize;
        }

        public double[] propagate(double[][] input)
        {
            double[][] result = new double[][] {
                new double[outputSize],
                new double[outputSize]
            };
            for (int i = 0; i < input.Length; i++)
                result = strap[i].propagate(input[i], result[RESULT], result[STATE]);
            return result[RESULT];
        }

        public static LSTM read(String stream)
        {
            Scanner reader = new Scanner(stream);
            int outputSize = reader.nextInt();
            LSTMCell[] strap = new LSTMCell[reader.nextInt()];
            for (int i = 0; i < strap.Length; i++)
                strap[i] = LSTMCell.read(reader);
            return new LSTM(strap, outputSize);
        }

        public void save(String path)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.Write(outputSize + " " + strap.Length);
            for (int i = 0; i < strap.Length; i++)
            {
                writer.Write(" ");
                strap[i].save(writer);
            }
            writer.Close();
        }

        public int length()
        {
            int result = 0;
            foreach (LSTMCell cell in strap)
                result += cell.length();
            return result;
        }

        public void update(double[] values)
        {
            int index = 0;
            foreach (LSTMCell cell in strap)
                index = cell.update(values, index);
        }

        public double[] upload()
        {
            double[] values = new double[length()];
            int index = 0;
            foreach (LSTMCell cell in strap)
                index = cell.upload(values, index);
            return values;
        }
    }
}
