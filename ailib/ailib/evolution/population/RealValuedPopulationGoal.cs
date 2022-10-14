using ailib.objects;
using ailib.search;
using ailib.sort;

namespace ailib.evolution.population
{
    public class RealValuedPopulationGoal<T>
    {
		public Tagged<T> pick(Tagged<T>[] pop, RealValuedGoal<T> goal)
		{
			Order<double> order = goal.getOrder();
			int k = 0;
			double q = goal.apply(pop[0]);
			for (int i = 1; i < pop.Length; i++)
			{
				double qi = goal.apply(pop[i]);
				if (order.compare(q, qi) < 0)
				{
					k = i;
					q = qi;
				}
			}
			return pop[k];
		}
	}
}
