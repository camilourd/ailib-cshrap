using ailib.random;

namespace ailib.search.select
{
    public class Tournament<T, R> : GoalBasedSelection<T, R>
    {
        protected int size = 4;
        protected GoalBasedSelection<T, R> inner = null;
        RandomGenerator<double> generator;

        public Tournament(int size, RandomGenerator<double> generator)
        {
            this.inner = new Elitism<T, R>(1.0, 0.0, generator);
            this.generator = generator;
            this.size = size;
        }

        public Tournament(Goal<T, R> goal, int size, RandomGenerator<double> generator) : base(goal)
        {
            this.inner = new Elitism<T, R>(goal, 1.0, 0.0, generator);
            this.generator = generator;
            this.size = size;
        }

        public override void setGoal(Goal<T, R> goal)
        {
            base.setGoal(goal);
            if (inner != null)
                inner.setGoal(goal);
        }

        public override int[] apply(int n, R[] x)
        {
            int[] sel = new int[n];
            for (int i = 0; i < n; i++)
                sel[i] = choose_one(x);
            return sel;
        }

        public override int choose_one(R[] x)
        {
            R[] candidates = new R[size];
            int[] indices = new int[size];
            for (int i = 0; i < size; i++)
            {
                indices[i] = (int)(x.Length * generator.next());
                candidates[i] = x[indices[i]];
            }
            return indices[inner.choose_one(candidates)];
        }
    }
}
