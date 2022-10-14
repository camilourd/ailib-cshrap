using ailib.random;
using ailib.search.variate;
using System;

namespace ailib.evolution.haea
{
    public class SimpleHaeaOperators<T> : HaeaOperators<T>
    {
        protected Variation<T>[] opers;

        public SimpleHaeaOperators(Variation<T>[] opers, RandomGenerator<double> generator) : base(generator, new SimpleHaeaOperatorsDescriptors<T>())
        {
            this.opers = opers;
        }

        public override Variation<T> get(int indIndex, int operIndex)
        {
            return opers[operIndex];
        }

        public override int numberOfOperators()
        {
            return opers.Length;
        }

        public override int numberOfOperatorsPerIndividual()
        {
            return opers.Length;
        }
    }
}
