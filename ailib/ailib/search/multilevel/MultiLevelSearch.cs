using ailib.objects;
using ailib.search.space;

namespace ailib.search.multilevel
{
    public class MultiLevelSearch<G, P, R> : SearchFunction<P, R>
    {
        protected Goal<P, R> goal;
        protected SearchFunction<G, R> lowLevelSearch;
        protected CodeDecodeMap<G, P> map;
        protected TaggedManager<P> manager;

        public MultiLevelSearch(SearchFunction<G, R> lowLevelSearch, CodeDecodeMap<G, P> map) : base("MultiLevelSearch")
        {
            this.lowLevelSearch = lowLevelSearch;
            this.map = map;
            this.manager = new TaggedManager<P>() { };
        }

        public override Goal<P, R> getGoal()
        {
            return goal;
        }

        public override void setGoal(Goal<P, R> goal)
        {
            this.goal = goal;
        }

        public override Tagged<P> solve(Space<P> space)
        {
            MultiLevelGoal<G, P, R> lowLevelGoal = new MultiLevelGoal<G, P, R>(getGoal(), map);
            MultiLevelSpace<G, P> lowLevelSpace = new MultiLevelSpace<G, P>(space, map);
            lowLevelSearch.setGoal(lowLevelGoal);
            Tagged<G> sol = lowLevelSearch.solve(lowLevelSpace);
            Tagged<P> h_sol = manager.wrap(map.decode(sol.unwrap()));
            return h_sol;
        }
    }
}
