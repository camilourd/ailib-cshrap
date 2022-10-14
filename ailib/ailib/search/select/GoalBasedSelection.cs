using ailib.objects;

namespace ailib.search.select
{
    public abstract class GoalBasedSelection<T, R> : BasicGoalBased<T, R>, Selection<T>
    {
        public GoalBasedSelection() { }

        public GoalBasedSelection(Goal<T, R> goal) {
            setGoal(goal);
        }

        public abstract int[] apply(int n, R[] x);
        public abstract int choose_one(R[] x);

        public R[] quality(Tagged<T>[] x) {
            return goal.setApply(x);
        }

        public int[] apply(int n, Tagged<T>[] x) {
            return apply(n, quality(x));
        }

        public int choose_one(Tagged<T>[] x) {
            return choose_one(quality(x));
        }

        public Tagged<T>[] pick(int n, Tagged<T>[] x)
        {
            Tagged<T>[] obj = new Tagged<T>[n];
            int[] idx = apply(n, x);
            for (int i = 0; i < n; i++)
                obj[i] = x[idx[i]];
            return obj;
        }
    }
}
