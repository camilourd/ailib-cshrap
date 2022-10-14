namespace ailib.search.variate
{
    public abstract class Variation_2_m<T> : Variation<T>
    {
        public override T[] apply(T[] pop)
        {
            T[] result = new T[(pop.Length / 2) * range_arity()];
            for (int i = 0; i < pop.Length; i += 2)
            {
                T[] next = apply(pop[i], pop[i + 1]);
                for (int j = 0; j < next.Length; j++)
                    result[(i * range_arity()) + j] = next[j];
            }
            return result;
        }

        public abstract T[] apply(T one, T two);

        public override int arity() {
            return 2;
        }
    }
}
