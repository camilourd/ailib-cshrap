namespace ailib.random
{
    public class Shuffle<T>
    {
        private RandomGenerator<double> generator;

        public Shuffle(RandomGenerator<double> generator)
        {
            this.generator = generator;
        }

        public void apply(T[] values) {
            for (int i = 0; i < values.Length; i++)
            {
                int next = (int)(values.Length * this.generator.next());
                T temp = values[i];
                values[i] = values[next];
                values[next] = temp;
            }
        }
    }
}
