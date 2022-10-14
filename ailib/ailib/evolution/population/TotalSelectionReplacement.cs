using ailib.objects;
using ailib.random;
using ailib.search;
using ailib.search.select;

namespace ailib.evolution.population
{
    public class TotalSelectionReplacement<T> : BasicGoalBased<T, double>, PopulationReplacement<T>
    {
        Selection<T> selection = null;
        RandomGenerator<double> generator;

        public TotalSelectionReplacement(RandomGenerator<double> generator) {
            this.generator = generator;
        }

        public TotalSelectionReplacement(Selection<T> selection)
        {
            this.selection = selection;
        }

        public Tagged<T>[] apply(Tagged<T>[] current, Tagged<T>[] next)
        {
            if (selection == null)
                selection = new Elitism<T, double>(goal, 1.0, 0.0, generator);
            
            int n = current.Length;
            int m = next.Length;

            Tagged<T>[] parent = new Tagged<T>[n + m];
            for (int i = 0; i < parent.Length; i++)
                parent[i] = (i < n) ? current[i] : next[i - n];
            return selection.pick(n, parent);
        }

        public void init() { }
    }
}
