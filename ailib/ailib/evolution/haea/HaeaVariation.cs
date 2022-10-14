using ailib.objects;
using ailib.search.select;
using ailib.search.variate;
using System.Collections.Generic;

namespace ailib.evolution.haea
{
    public class HaeaVariation<T> : Variation<T>
    {
        protected HaeaOperators<T> operators;
        protected Selection<T> selection;

        public HaeaVariation(Selection<T> selection, HaeaOperators<T> operators)
        {
            this.selection = selection;
            this.operators = operators;
        }

        public HaeaOperators<T> getOperators() { return operators; }

        public override Tagged<T>[] apply(Tagged<T>[] population)
        {
			operators.resize(population.Length);
			List<Tagged<T>> buffer = new List<Tagged<T>>();
			for (int i = 0; i < population.Length; i++)
			{
				int operIndex = operators.select(i);
				Variation<T> oper = operators.get(i, operIndex);
                Tagged<T>[] pop = selection.pick(oper.arity() - 1, population);
				Tagged<T>[] parents = new Tagged<T>[oper.arity()];
				parents[0] = population[i];
				for (int k = 0; k < pop.Length; k++)
					parents[k + 1] = pop[k];
				Tagged<T>[] offspring = oper.apply(parents);
				operators.setSizeOffspring(i, offspring.Length);
				foreach (Tagged<T> x in offspring)
					buffer.Add(x);
			}
			return buffer.ToArray();
		}

        public override int arity()
        {
            return 0;
        }

        public override int range_arity()
        {
            return 0;
        }

        public override T[] apply(T[] population)
        {
            return null;
		}
    }
}
