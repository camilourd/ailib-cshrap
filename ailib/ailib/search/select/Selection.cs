using ailib.objects;

namespace ailib.search.select
{
    public interface Selection<T>
    {
        int[] apply(int n, Tagged<T>[] x);
        int choose_one(Tagged<T>[] x);
        Tagged<T>[] pick(int n, Tagged<T>[] x);
    }
}
