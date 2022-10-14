using ailib.evolution.population;
using ailib.objects;
using ailib.search;
using ailib.search.select;
using ailib.search.space;
using ailib.sort;
using System;

namespace ailib.evolution.haea
{
    public class HaeaStep<T> : VariationReplacePopulationSearch<T, double>
    {
        PopulationDescriptors<T> populationDescriptor = new PopulationDescriptors<T>();
        HaeaStepDescriptors<T> stepDescriptors = new HaeaStepDescriptors<T>();

        public HaeaStep(int populationSize, Selection<T> selection, HaeaReplacement<T> replacement)
            : base("HaeaStep", populationSize, new HaeaVariation<T>(selection, replacement.getOperators()), replacement) { }

        public HaeaStep(int populationSize, Selection<T> selection, HaeaOperators<T> operators)
            : base("HaeaStep", populationSize, new HaeaVariation<T>(selection, operators), new HaeaReplacement<T>(operators)) { }

        public HaeaStep(int populationSize, Selection<T> selection, HaeaOperators<T> operators, bool steady)
            : base("HaeaStep", populationSize, new HaeaVariation<T>(selection, operators), new HaeaReplacement<T>(operators, steady)) { }

        public HaeaStep(int populationSize, HaeaVariation<T> variation, HaeaReplacement<T> replacement)
            : base("HaeaStep", populationSize, variation, replacement) { }

        public HaeaOperators<T> getOperators()
        {
            return ((HaeaVariation<T>)variation).getOperators();
        }

        public override Goal<T, double> getGoal()
        {
            return ((GoalBased<T, double>)replace).getGoal();
        }

        public override Tagged<T>[] init(Space<T> space)
        {
            Tagged<T>[] pop = base.init(space);
            ((HaeaVariation<T>)variation).getOperators().resize(pop.Length);
            return pop;
        }

        public override Tagged<T> pick(Tagged<T>[] pop)
        {
            int best = 0;
            Order<double> order = getGoal().getOrder();
            double quality = getGoal().apply(pop[0]);
            for (int i = 1; i < pop.Length; i++)
            {
                double temp = getGoal().apply(pop[i]);
                if (order.compare(temp, quality) > 0)
                {
                    best = i;
                    quality = temp;
                }
            }
            return pop[best];
        }

        public override void setGoal(Goal<T, double> goal)
        {
            ((GoalBased<T, double>)replace).setGoal(goal);
        }

        public override void trace(Tagged<T>[] pop, Space<T> space)
        {
            populationDescriptor.setGoal(getGoal());
            print(populationDescriptor.describe(pop));
            print(stepDescriptors.describe(this));
        }

        private void print(double[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Console.Write(" " + values[i]);
        }
    }
}
