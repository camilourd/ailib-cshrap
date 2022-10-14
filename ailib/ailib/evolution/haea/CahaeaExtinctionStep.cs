using ailib.objects;
using ailib.random;
using ailib.random.real;
using ailib.search.select;
using ailib.search.space;
using System;

namespace ailib.evolution.haea
{
    public class CahaeaExtinctionStep<T> : CahaeaStep<T>
    {
        RandomGenerator<double> generator;
        RandomGenerator<double> extinctionGenerator;
        double accumulator = 0;

        public CahaeaExtinctionStep(int populationSize, Selection<T> selection, HaeaReplacement<T> replacement, RandomGenerator<double> generator)
            : base(populationSize, new CahaeaVariation<T>(selection, replacement.getOperators()), replacement)
        {
            this.generator = generator;
            this.extinctionGenerator = new StandardPowerLaw(generator, 0.991);
        }

        public CahaeaExtinctionStep(int populationSize, Selection<T> selection, HaeaOperators<T> operators, RandomGenerator<double> generator)
            : base(populationSize, new CahaeaVariation<T>(selection, operators), new HaeaReplacement<T>(operators))
        {
            this.generator = generator;
            this.extinctionGenerator = new StandardPowerLaw(generator, 0.99);
        }

        public CahaeaExtinctionStep(int populationSize, Selection<T> selection, HaeaOperators<T> operators, bool steady, RandomGenerator<double> generator)
            : base(populationSize, new CahaeaVariation<T>(selection, operators), new HaeaReplacement<T>(operators, steady))
        {
            this.generator = generator;
            this.extinctionGenerator = new StandardPowerLaw(generator, 0.99);
        }

        public CahaeaExtinctionStep(int populationSize, CahaeaVariation<T> variation, HaeaReplacement<T> replacement, RandomGenerator<double> generator)
            : base(populationSize, variation, replacement)
        {
            this.generator = generator;
            this.extinctionGenerator = new StandardPowerLaw(generator, 0.99);
        }

        public override Tagged<T>[] apply(Tagged<T>[] pop, Space<T> space)
        {
            Tagged<T>[] next = base.apply(pop, space);
            accumulator = Math.Min(accumulator + 0.5, 100);
            int index = findBest(next);
            int total = (int)(extinctionGenerator.next() * next.Length), cnt = 0;
            int extinct = Math.Max(0, Math.Min(total, (int)accumulator));
            accumulator = Math.Max(accumulator - total, -25);
            double probability = total / (1.0 * next.Length);
            for (int i = 0; i < next.Length; i++)
                if (i != index && cnt < extinct && generator.next() < probability)
                {
                    next[i] = new Tagged<T>(space.pick());
                    getOperators().restart(i);
                    cnt++;
                }
            Console.WriteLine("> Extinct: " + extinct + "/" + total + " (" + accumulator + ")");
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
