using ailib.random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ailib.search.select.real
{
    public class RealTournament<T> : GoalBasedSelection<T, double>
    {
        int size;
        RandomGenerator<double> generator;

        public RealTournament(Goal<T, double> goal, int size, RandomGenerator<double> generator) : base(goal)
        {
            this.size = size;
            this.generator = generator;
        }

        public override int[] apply(int n, double[] performance)
        {
            int[] sel = new int[n];
            for (int i = 0; i < n; i++)
                sel[i] = choose_one(performance);
            return sel;
        }

        public override int choose_one(double[] performance)
        {
            int[] rivals = new int[performance.Length];
            for (int i = 0; i < rivals.Length; i++)
                rivals[i] = (int)(generator.next() * rivals.Length);
            while (rivals.Length > 1)
                rivals = compete(rivals, performance);
            return rivals[0];
        }

        private int[] compete(int[] rivals, double[] performance)
        {
            int[] winners = new int[(int)Math.Ceiling(rivals.Length / 2.0)];
            for (int i = 0; i < rivals.Length; i += 2)
                winners[i / 2] = (i + 1 < rivals.Length) ? fight(rivals[i], rivals[i + 1], performance) : rivals[i];
            return winners;
        }

        private int fight(int rival1, int rival2, double[] performance)
        {
            double probability = (goal.getOrder().compare(0, 1) > 1) ?
                1 - (performance[rival1] / (performance[rival1] + performance[rival2])) :
                (performance[rival1] / (performance[rival1] + performance[rival2]));
            return (generator.next() < probability) ? rival1 : rival2;
        }
    }
}
