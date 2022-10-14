using ailib.objects;

namespace ailib.search.space
{
    public abstract class Space<T>
    {
        public static string FEASIBLE = "feasible";
	    public static string FEASIBILITY = "feasibility";

        public abstract bool feasible(T x);
        public abstract double feasibility(T x);
        public abstract T repair(T x);
        public abstract T pick();

        public virtual T[] pick(int n)
        {
            T[] v = new T[n];
            for (int i = 0; i < n; i++)
                v[i] = pick();
            return v;
        }

        public virtual T[] repair(T[] pop )
        {
            T[] v = new T[pop.Length];
            for (int i = 0; i < pop.Length; i++)
                v[i] = repair(pop[i]);
            return v;
        }

        public virtual Tagged<T>[] repair(Tagged<T>[] pop )
        {
            Tagged<T>[] v = new Tagged<T>[pop.Length];
            for (int i = 0; i < pop.Length; i++)
                v[i] = new Tagged<T>(repair(pop[i].unwrap()));
            return v;
        }
    }
}
