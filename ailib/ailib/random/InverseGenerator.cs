namespace ailib.random
{
    public abstract class InverseGenerator<T> : Generator<T>
    {
        RandomGenerator<double> generator;

        public InverseGenerator(RandomGenerator<double> generator) {
            this.generator = generator;
        }

        public abstract T[] raw(T[] v, int offset, int m);
        public abstract T[] raw(int m);
        public abstract T next(double x);

        public T next() {
            return next(generator.next());
        }
    }
}
