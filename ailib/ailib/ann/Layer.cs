namespace ailib.math.ann
{
    public abstract class Layer<N, I, O>
    {
        protected N[] neurons;

        public Layer(N[] neurons)
        {
            this.neurons = neurons;
        }

        public abstract O propagate(I input);
        public abstract O weighted_input(I input);
        public abstract O activate(O winput);

        public int size()
        {
            return neurons.Length;
        }

        public N this[int index]
        {
            get
            {
                return neurons[index];
            }

            set
            {
                neurons[index] = value;
            }
        }
    }
}
