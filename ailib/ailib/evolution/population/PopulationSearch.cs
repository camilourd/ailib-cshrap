using ailib.objects;
using ailib.search;
using ailib.search.space;

namespace ailib.evolution.population
{
    public abstract class PopulationSearch<T, R>: SearchFunction<T, R>
    {
        public PopulationSearch(string name) : base(name) { }

        public PopulationSearch(string name, bool deterministic) : base(name, deterministic) { }

        public abstract Tagged<T>[] init(Space<T> space);
        public abstract Tagged<T> pick(Tagged<T>[] pop);
        public abstract Tagged<T>[] apply(Tagged<T>[] pop, Space<T> space);
        public abstract void trace(Tagged<T>[] pop, Space<T> space);

        public override Tagged<T> solve(Space<T> space) {
            return pick(apply(init(space), space));
        }
    }
}
