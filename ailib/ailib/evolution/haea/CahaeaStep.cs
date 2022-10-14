using ailib.evolution.population;
using ailib.objects;
using ailib.search;
using ailib.search.select;
using ailib.search.space;
using ailib.sort;
using System;

namespace ailib.evolution.haea
{
    public class CahaeaStep<T> : VariationReplacePopulationSearch<T, double>
    {
        PopulationDescriptors<T> populationDescriptor = new PopulationDescriptors<T>();
        CahaeaStepDescriptors<T> stepDescriptors = new CahaeaStepDescriptors<T>();

        public CahaeaStep(int populationSize, Selection<T> selection, HaeaReplacement<T> replacement)
            : base("CahaeaStep", populationSize, new CahaeaVariation<T>(selection, replacement.getOperators()), replacement) { }

        public CahaeaStep(int populationSize, Selection<T> selection, HaeaOperators<T> operators)
            : base("CahaeaStep", populationSize, new CahaeaVariation<T>(selection, operators), new HaeaReplacement<T>(operators)) { }

        public CahaeaStep(int populationSize, Selection<T> selection, HaeaOperators<T> operators, bool steady)
            : base("CahaeaStep", populationSize, new CahaeaVariation<T>(selection, operators), new HaeaReplacement<T>(operators, steady)) { }

        public CahaeaStep(int populationSize, CahaeaVariation<T> variation, HaeaReplacement<T> replacement)
            : base("CahaeaStep", populationSize, variation, replacement) { }

        public HaeaOperators<T> getOperators()
        {
            return ((CahaeaVariation<T>)variation).getOperators();
        }

        public override Goal<T, double> getGoal()
        {
            return ((GoalBased<T, double>)replace).getGoal();
        }

        public override Tagged<T>[] init(Space<T> space)
        {
            Tagged<T>[] pop = base.init(space);
            ((CahaeaVariation<T>)variation).getOperators().resize(pop.Length);
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
