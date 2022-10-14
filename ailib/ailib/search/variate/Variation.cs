using ailib.objects;
using ailib.search.space;

namespace ailib.search.variate
{
    public abstract class Variation<T> : TaggedManager<T>
    {
        public abstract int arity();
        public abstract int range_arity();
        public abstract T[] apply(T[] pop);

        public virtual T[] apply(Space<T> space, T[] pop)
        {
            return space.repair(apply(pop));
        }

        public virtual Tagged<T>[] apply(Tagged<T>[] pop)
        {
            return wrap(apply(unwrap(pop)));
        }

        public virtual Tagged<T>[] apply(Space<T> space, Tagged<T>[] pop)
        {
            return space.repair(apply(pop));
        }
    }
}
