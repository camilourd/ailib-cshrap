using System.IO;
using ailib.io;

namespace ailib.utils
{
    public class Dimension
    {
        public int width, height, length;

        public Dimension(int width, int height, int lenght)
        {
            this.width = width;
            this.height = height;
            this.length = lenght;
        }

        public Dimension(int width, int height) : this(width, height, 0)
        {
        }

        public Dimension(int width) : this(width, 0)
        {
        }

        internal static Dimension read(Scanner reader)
        {
            int width = reader.nextInt();
            int height = reader.nextInt();
            int lenght = reader.nextInt();
            return new Dimension(width, height, lenght);
        }

        internal void save(StreamWriter writer)
        {
            writer.Write(width + " " + height + " " + length);
        }
    }
}
