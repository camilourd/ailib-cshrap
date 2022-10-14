using ailib.evolution.population;
using ailib.math.functions.iterative;
using ailib.math.logic;
using ailib.objects;
using ailib.random;
using ailib.search.select;

namespace ailib.evolution.haea
{
    public class HaeaFactory<T>
    {
        public PopulationSearch<T, double> HAEA(HaeaStep<T> step, Predicate<Tagged<T>[]> tC)
        {
            return new IterativePopulationSearch<T, double>(tC, step);
        }

        public PopulationSearch<T, double> HAEA(int mu, HaeaOperators<T> operators, Selection<T> selection, Predicate<Tagged<T>[]> tC)
        {
            return HAEA(new HaeaStep<T>(mu, selection, operators), tC);
        }

        public PopulationSearch<T, double> CAHAEA(CahaeaStep<T> step, Predicate<Tagged<T>[]> tC)
        {
            return new IterativePopulationSearch<T, double>(tC, step);
        }

        public PopulationSearch<T, double> CAHAEA(int mu, HaeaOperators<T> operators, Selection<T> selection, Predicate<Tagged<T>[]> tC)
        {
            return CAHAEA(new CahaeaStep<T>(mu, selection, operators), tC);
        }

        public PopulationSearch<T, double> HAEA_Extinction(int mu, HaeaOperators<T> operators, Selection<T> selection, Predicate<Tagged<T>[]> tC, RandomGenerator<double> generator)
        {
            return HAEA(new HaeaExtinctionStep<T>(mu, selection, operators, generator), tC);
        }

        public PopulationSearch<T, double> CAHAEA_Extinction(int mu, HaeaOperators<T> operators, Selection<T> selection, Predicate<Tagged<T>[]> tC, RandomGenerator<double> generator)
        {
            return CAHAEA(new CahaeaExtinctionStep<T>(mu, selection, operators, generator), tC);
        }

        public PopulationSearch<T, double> HAEA(int mu, HaeaOperators<T> operators, Selection<T> selection, int MAXITERS)
        {
            return HAEA(mu, operators, selection, new ForLoopCondition<Tagged<T>[]>(MAXITERS));
        }

        public PopulationSearch<T, double> CAHAEA(int mu, HaeaOperators<T> operators, Selection<T> selection, int MAXITERS)
        {
            return CAHAEA(mu, operators, selection, new ForLoopCondition<Tagged<T>[]>(MAXITERS));
        }

        public PopulationSearch<T, double> HAEA_Extinction(int mu, HaeaOperators<T> operators, Selection<T> selection, int MAXITERS, RandomGenerator<double> generator)
        {
            return HAEA_Extinction(mu, operators, selection, new ForLoopCondition<Tagged<T>[]>(MAXITERS), generator);
        }

        public PopulationSearch<T, double> CAHAEA_Extinction(int mu, HaeaOperators<T> operators, Selection<T> selection, int MAXITERS, RandomGenerator<double> generator)
        {
            return CAHAEA_Extinction(mu, operators, selection, new ForLoopCondition<Tagged<T>[]>(MAXITERS), generator);
        }

        public PopulationSearch<T, double> HAEA(HaeaStep<T> step, int MAXITERS)
        {
            return HAEA(step, new ForLoopCondition<Tagged<T>[]>(MAXITERS));
        }

        public PopulationSearch<T, double> CAHAEA(CahaeaStep<T> step, int MAXITERS)
        {
            return CAHAEA(step, new ForLoopCondition<Tagged<T>[]>(MAXITERS));
        }
    }
}
