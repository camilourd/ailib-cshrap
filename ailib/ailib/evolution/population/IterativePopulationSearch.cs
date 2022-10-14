using ailib.math.logic;
using ailib.objects;
using ailib.search;
using ailib.search.space;

namespace ailib.evolution.population
{
    public class IterativePopulationSearch<T, R> : PopulationSearch<T, R>
    {
        protected Predicate<Tagged<T>[]> terminationCondition;
        protected PopulationSearch<T, R> step;
        protected int iter;

        public IterativePopulationSearch(Predicate<Tagged<T>[]> terminationCondition, PopulationSearch<T, R> step) : base("IterativePopulationSearch")
        {
            this.terminationCondition = terminationCondition;
            this.step = step;
        }

        public override Tagged<T>[] apply(Tagged<T>[] pop, Space<T> space)
        {
            trace(pop, space);
            while (terminationCondition.evaluate(pop))
            {
                pop = step.apply(pop, space);
                trace(pop, space);
            }
            return pop;
        }

        public override Goal<T, R> getGoal()
        {
            return step.getGoal();
        }

        public override Tagged<T>[] init(Space<T> space)
        {
            terminationCondition.init();
            return step.init(space);
        }

        public override Tagged<T> pick(Tagged<T>[] pop)
        {
            return step.pick(pop);
        }

        public override void setGoal(Goal<T, R> goal)
        {
            step.setGoal(goal);
        }

        public override void trace(Tagged<T>[] pop, Space<T> space)
        {
            System.Console.Write("> " + (iter++) + ":");
            step.trace(pop, space);
            System.Console.WriteLine();
        }
    }
}
