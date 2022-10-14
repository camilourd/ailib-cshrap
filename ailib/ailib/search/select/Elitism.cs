using ailib.collection.keymap;
using ailib.math.integer;
using ailib.random;
using ailib.sort;

namespace ailib.search.select
{
    public class Elitism<T, R> : GoalBasedSelection<T, R>
    {
        protected double elite_percentage = 0.1;
        protected double cull_percentage = 0.1;
        RandomGenerator<double> random;

        public Elitism(double _elite_percentage, double _cull_percentage, RandomGenerator<double> generator)
        {
            elite_percentage = _elite_percentage;
            cull_percentage = _cull_percentage;
            this.random = generator;
        }

        public Elitism(Goal<T, R> goal, double _elite_percentage, double _cull_percentage, RandomGenerator<double> generator) : base(goal)
        {
            elite_percentage = _elite_percentage;
            cull_percentage = _cull_percentage;
            this.random = generator;
        }

        public override int[] apply(int n, R[] x)
        {
            Order<R> order = goal.getOrder();
            int[] sel = new int[n];
            int s = x.Length;
            Sorted<KeyValue<int, R>> indexq = new Sorted<KeyValue<int, R>>(
                new Reversed<KeyValue<int, R>>(new ValueOrder<int, R>(order)));
            for (int i = 0; i < s; i++) indexq.add(new KeyValue<int, R>(i, x[i]));

            int m = (int)(s * elite_percentage);
            for (int i = 0; i < n && i < m; i++)
                sel[i] = indexq.get(i).getKey();

            if (m < n)
            {
                int k = (int)(s * (1.0 - cull_percentage));
                Roulette generator = new Roulette(k, random);
                n -= m;
                int[] index = generator.next(n);
                for (int i = 0; i < n; i++)
                {
                    sel[m] = indexq.get(index[i]).getKey();
                    m++;
                }
            }
            return sel;
        }

        public override int choose_one(R[] x)
        {
            Order<R> order = goal.getOrder();
            int k = 0;
            R q = x[k];
            for (int i = 1; i < x.Length; i++)
            {
                R q2 = x[i];
                if (order.compare(q, q2) < 0)
                {
                    k = i;
                    q = q2;
                }
            }
            return k;
        }
    }
}
