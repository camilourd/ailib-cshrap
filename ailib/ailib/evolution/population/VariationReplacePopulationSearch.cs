using ailib.objects;
using ailib.search;
using ailib.search.space;
using ailib.search.variate;
using System;

namespace ailib.evolution.population
{
    public abstract class VariationReplacePopulationSearch<T, R> : PopulationSearch<T, R>
    {
        protected int populationSize;
        protected Variation<T> variation;
        protected PopulationReplacement<T> replace;

        public VariationReplacePopulationSearch(string name, int populationSize, Variation<T> variation, PopulationReplacement<T> replace) : base(name)
        {
            this.populationSize = populationSize;
            this.variation = variation;
            this.replace = replace;
        }

        public override Tagged<T>[] init(Space<T> space)
        {
            T[] sol = space.pick(populationSize);
            Tagged<T>[] pop = new Tagged<T>[sol.Length];
            for (int i = 0; i < pop.Length; i++)
                pop[i] = new Tagged<T>(sol[i]);
            return pop;
        }

        public override Tagged<T>[] apply(Tagged<T>[] pop, Space<T> space)
        {
            Goal<T, R> goal = getGoal();
            goal.init();
            if (goal.clean)
                clean(pop, goal.name);
            return replace.apply(pop, variation.apply(space, pop));
        }

        private void clean(Tagged<T>[] pop, string name)
        {
            Console.WriteLine("> cleaning performance");
            foreach (Tagged<T> individual in pop)
                if (individual.contains(name))
                    individual.remove(name);
        }
    }
}
