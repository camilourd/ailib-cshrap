using ailib.objects;
using ailib.random;
using ailib.search.select;
using ailib.search.space;
using System;

namespace ailib.evolution.haea
{
    public class HaeaExtinctionStep<T> : HaeaStep<T>
    {
        private RandomGenerator<double> generator;
        private double probability = 0;
        private decimal force = 1;

        public HaeaExtinctionStep(int populationSize, Selection<T> selection, HaeaReplacement<T> replacement, RandomGenerator<double> generator)
            : base(populationSize, new HaeaVariation<T>(selection, replacement.getOperators()), replacement)
        {
            this.generator = generator;
        }

        public HaeaExtinctionStep(int populationSize, Selection<T> selection, HaeaOperators<T> operators, RandomGenerator<double> generator)
            : base(populationSize, new HaeaVariation<T>(selection, operators), new HaeaReplacement<T>(operators))
        {
            this.generator = generator;
        }

        public HaeaExtinctionStep(int populationSize, Selection<T> selection, HaeaOperators<T> operators, bool steady, RandomGenerator<double> generator)
            : base(populationSize, new HaeaVariation<T>(selection, operators), new HaeaReplacement<T>(operators, steady))
        {
            this.generator = generator;
        }

        public HaeaExtinctionStep(int populationSize, HaeaVariation<T> variation, HaeaReplacement<T> replacement, RandomGenerator<double> generator)
            : base(populationSize, variation, replacement)
        {
            this.generator = generator;
        }

        public override Tagged<T>[] apply(Tagged<T>[] pop, Space<T> space)
        {
            Tagged<T>[] next = base.apply(pop, space);
            int index = findBest(next);
            int parent = findBest(pop);
            this.probability = Math.Min(1.0, this.probability + (1e-16 * (double)force) + Math.Abs((getGoal().apply(next[index]) / getGoal().apply(pop[parent])) - 1));
            if (this.generator.next() < this.probability)
            {
                int total = (int)Math.Ceiling(this.probability * next.Length), cnt = 0;
                for (int i = 0; i < next.Length; i++)
                    if (i != index && cnt < total && this.generator.next() < this.probability)
                    {
                        next[i] = new Tagged<T>(space.pick());
                        getOperators().restart(i);
                        cnt++;
                    }
                Console.WriteLine("> Applying extinction, extincted: " + cnt + "/" + total + " (" + this.probability + ")");
                this.probability -= (1.0 * cnt) / next.Length;
                this.force = (cnt > 0) ? 1 : this.force;
            }
            else
                force *= force + (decimal)1e-25;
            Console.WriteLine("> Extinction probability: " + this.probability + " (force: " + force + ")");
            return next;
        }

        public int findBest(Tagged<T>[] buffer)
        {
            int best = 0;
            double qs = getGoal().apply(buffer[best]);
            for (int i = 1; i < buffer.Length; i++)
            {
                double qk = getGoal().apply(buffer[i]);
                if (getGoal().getOrder().compare(qk, qs) > 0)
                {
                    best = i;
                    qs = qk;
                }
            }
            return best;
        }
    }
}
