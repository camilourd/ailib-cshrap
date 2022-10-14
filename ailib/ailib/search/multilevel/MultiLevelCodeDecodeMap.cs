namespace ailib.search.multilevel
{
    public class MultiLevelCodeDecodeMap<G, I, P> : CodeDecodeMap<G, P>
    {
        protected CodeDecodeMap<G, I> lowLevel;
        protected CodeDecodeMap<I, P> highLevel;

        public MultiLevelCodeDecodeMap(CodeDecodeMap<G, I> lowLevel, CodeDecodeMap<I, P> highLevel)
        {
            this.lowLevel = lowLevel;
            this.highLevel = highLevel;
        }

        public override P decode(G genome)
        {
            return highLevel.decode(lowLevel.decode(genome));
        }

        public override G encode(P thing)
        {
            return lowLevel.encode(highLevel.encode(thing));
        }
    }
}
