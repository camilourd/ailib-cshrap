namespace ailib.random
{
    public  interface Generator<T>
    {
        T next();
        T[] raw(T[] v, int offset, int m);
        T[] raw(int m);
    }
}
