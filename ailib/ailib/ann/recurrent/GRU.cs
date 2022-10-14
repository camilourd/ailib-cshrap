using ailib.io;
using System;
using System.IO;

namespace ailib.ann.recurrent
{
    public class GRU
    {
        GRUUnit[] strap;
        int outputSize;

        public GRU(int inputSize, int outputSize, int total)
        {
            this.outputSize = outputSize;
            strap = new GRUUnit[total];
            for (int i = 0; i < strap.Length; i++)
                strap[i] = new GRUUnit(inputSize, outputSize);
        }

        public GRU(GRUUnit[] strap, int outputSize)
        {
            this.strap = strap;
            this.outputSize = outputSize;
        }

        public double[] propagate(double[][] input)
        {
            double[] result = new double[outputSize];
            for (int i = 0; i < input.Length; i++)
                result = strap[i].propagate(input[i], result);
            return result;
        }

        public static GRU read(String stream)
        {
            Scanner reader = new Scanner(stream);
            int outputSize = reader.nextInt();
            GRUUnit[] strap = new GRUUnit[reader.nextInt()];
            for (int i = 0; i < strap.Length; i++)
                strap[i] = GRUUnit.read(reader);
            return new GRU(strap, outputSize);
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
            foreach (GRUUnit unit in strap)
                result += unit.length();
            return result;
        }

        public void update(double[] values)
        {
            int index = 0;
            foreach (GRUUnit unit in strap)
                index = unit.update(values, index);
        }

        public double[] upload()
        {
            double[] values = new double[length()];
            int index = 0;
            foreach (GRUUnit unit in strap)
                index = unit.upload(values, index);
            return values;
        }
    }
}
