namespace ailib.search.variate
{
    public abstract class Variation_4_m<T> : Variation<T>
    {
        public override T[] apply(T[] pop)
        {
            T[] result = new T[(pop.Length / 4) * range_arity()];
            for (int i = 0; i < pop.Length; i += 4)
            {
                T[] next = apply(pop[i], pop[i + 1], pop[i + 2], pop[i + 3]);
                for (int j = 0; j < next.Length; j++)
                    result[(i * range_arity()) + j] = next[j];
            }
            return result;
        }

        public abstract T[] apply(T one, T two, T three, T four);

        public override int arity()
        {
            return 4;
        }
    }
}
