using ailib.objects;
using System;

namespace ailib.math.functions
{
    public abstract class Function<S, T>
    {
        public bool deterministic;
        public string name;

        public Function(string name): this(name, true)
        {
        }

        public Function(string name, bool deterministic)
        {
            this.name = name;
            this.deterministic = deterministic;
        }

        public abstract T apply(S x);

        public T[] setApply(S[] x)
        {
            T[] result = new T[x.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = apply(x[i]);
            return result;
        }

        public T[] setApply(Tagged<S>[] x)
        {
            T[] result = new T[x.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = apply(x[i]);
            return result;
        }

        public T apply(Tagged<S> x)
        {
            S value = x.unwrap();
            if (deterministic)
            {
                if (x.contains(name))
                    return (T)x.find(name);
                T result = apply(value);
                x.store(name, result);
                return result;
            }
            return apply(value);
        }

        public virtual T[] apply(Tagged<S>[] x)
        {
            T[] result = new T[x.Length];
            for (int i = 0; i < x.Length; i++)
                result[i] = apply(x[i]);
            return result;
        }
    }
}
