using ailib.search.space;

namespace ailib.search.multilevel
{
    public class MultiLevelSpace<G, P> : Space<G>
    {
        protected Space<P> space;
        protected CodeDecodeMap<G, P> map;

        public MultiLevelSpace(Space<P> space, CodeDecodeMap<G, P> map)
        {
            this.space = space;
            this.map = map;
        }

        public override double feasibility(G x)
        {
            return space.feasibility(map.decode(x));
        }

        public override bool feasible(G x)
        {
            return space.feasible(map.decode(x));
        }

        public override G pick()
        {
            return map.encode(space.pick());
        }

        public override G repair(G x)
        {
            return map.encode(space.repair(map.decode(x)));
        }
    }
}
