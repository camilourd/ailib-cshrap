using ailib.objects;

namespace ailib.evolution.population
{
    public interface PopulationReplacement<T>
    {
        Tagged<T>[] apply(Tagged<T>[] current, Tagged<T>[] next);
        void init();
    }
}
