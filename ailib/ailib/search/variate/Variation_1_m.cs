namespace ailib.search.variate
{
    public abstract class Variation_1_m<T> : Variation<T>
    {
        public override T[] apply(T[] pop)
        {
            T[] result = new T[pop.Length * range_arity()];
            for (int i = 0; i < pop.Length; i ++)
            {
                T[] next = apply(pop[i]);
                for (int j = 0; j < next.Length; j++)
                    result[(i * range_arity()) + j] = next[j];
            }
            return result;
        }

        public abstract T[] apply(T one);

        public override int arity()
        {
            return 1;
        }
    }
}
