using ailib.math.functions;
using ailib.objects;
using ailib.search.space;

namespace ailib.search
{
    public abstract class SearchFunction<T, R> : Function<Space<T>, T>, GoalBased<T, R>
    {
        public SearchFunction(string name) : base(name, true) { }

        public SearchFunction(string name, bool deterministic) : base(name, deterministic) { }

        public abstract Tagged<T> solve(Space<T> space);

        public override T apply(Space<T> space) {
            return solve(space).unwrap();
        }

        public abstract Goal<T, R> getGoal();
        public abstract void setGoal(Goal<T, R> goal);
    }
}
