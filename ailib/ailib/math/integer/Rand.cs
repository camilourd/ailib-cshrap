using ailib.random;

namespace ailib.math.integer
{
    public interface Rand<T>: Generator<T>
    {
        void generate(int[] v, int offset, int m);
        int[] generate(int m);
    }
}
