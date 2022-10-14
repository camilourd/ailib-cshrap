using ailib.math.functions;
using ailib.math.logic;

namespace ailib.cluster
{
    public abstract class ClusteringFuntion<I, C, O> : Function<I, O>
    {
        protected Predicate<I> terminationCondition;

        public ClusteringFuntion(Predicate<I> terminationCondition, string name): base(name) {
            this.terminationCondition = terminationCondition;
        }

        public override O apply(I input)
        {
            C clusters = init(input);
            trace(input, clusters);
            while (terminationCondition.evaluate(input)) {
                clusters = apply(input, clusters);
                trace(input, clusters);
            }
            return filter(input, clusters);
        }

        protected abstract C init(I input);
        protected abstract void trace(I input, C clusters);
        protected abstract C apply(I input, C clusters);
        protected abstract O filter(I input, C clusters);
    }
}
