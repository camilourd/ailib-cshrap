using ailib.objects;
using ailib.sort;

namespace ailib.search.multilevel
{
    public class MultiLevelGoal<G, P, R> : Goal<G, R>
    {
        protected Goal<P, R> goal;
        protected CodeDecodeMap<G, P> map;

        public MultiLevelGoal(Goal<P, R> goal, CodeDecodeMap<G, P> map) : base("MultiLevelGoal", goal.getOrder())
        {
            this.goal = goal;
            this.map = map;
        }

        public override R apply(G x)
        {
            return compute(x);
        }

        public override R compute(G x)
        {
            return goal.apply(map.decode(x));
        }

        public bool isDeterministic() {
            return goal.deterministic;
        }

        public override Order<R> getOrder()
        {
            return goal.getOrder();
        }

        public override void init()
        {
            goal.init();
            this.clean = goal.clean;
        }
    }
}
