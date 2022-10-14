using ailib.evolution.population;
using ailib.objects;
using ailib.search;
using ailib.sort;

namespace ailib.evolution.haea
{
    public class HaeaReplacement<T> : BasicGoalBased<T, double>, PopulationReplacement<T>
    {
        protected HaeaOperators<T> operators;
        protected bool steady;

        public HaeaReplacement(HaeaOperators<T> operators) : this(operators, true) { }

        public HaeaReplacement(HaeaOperators<T> operators, bool steady)
        {
            this.operators = operators;
            this.steady = steady;
        }

        public HaeaOperators<T> getOperators() {
            return operators;
        }

        public virtual Tagged<T>[] apply(Tagged<T>[] current, Tagged<T>[] next)
        {
            Order<double> order = goal.getOrder();
            Tagged<T>[] buffer = new Tagged<T>[current.Length];
            int index = 0;
            for (int i = 0; i < current.Length; i++)
            {
                int best = index;
                double qs = goal.apply(next[index++]);
                for (int j = 1; j < operators.getSizeOffspring(i); j++) {
                    double qk = goal.apply(next[index]);
                    if (order.compare(qk, qs) > 0)
                    {
                        best = index;
                        qs = qk;
                    }
                    index++;
                }
                double qi = goal.apply(current[i]);
                if (order.compare(qi, qs) < 0)
                    operators.reward(i);
                else
                    operators.punish(i);

                buffer[i] = (!steady || order.compare(qi, qs) <= 0) ? next[best] : current[i];
            }
            return buffer;
        }

        public void init() { }
    }
}
