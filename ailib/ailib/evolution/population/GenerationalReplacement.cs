using ailib.objects;

namespace ailib.evolution.population
{
    public class GenerationalReplacement<T> : PopulationReplacement<T>
    {
        public Tagged<T>[] apply(Tagged<T>[] current, Tagged<T>[] next)
        {
            return next;
        }

        public void init() {}
    }
}
