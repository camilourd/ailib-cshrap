namespace ailib.random
{
    public interface RandomGenerator<T>
    {
        T next();
        T[] next(int v);
    }
}
