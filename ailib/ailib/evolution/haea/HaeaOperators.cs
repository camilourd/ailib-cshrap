using ailib.math.integer;
using ailib.random;
using ailib.search.variate;
using ailib.services.description;
using ailib.utils;
using System;
using System.Collections.Generic;

namespace ailib.evolution.haea
{
    public abstract class HaeaOperators<T>
    {
        RandomGenerator<double> generator;
        protected Roulette roulette;
        protected List<double[]> rates = new List<double[]>();
        protected List<int> sel_oper = new List<int>();
        protected List<int> size_offspring_sel_oper = new List<int>();
        public Descriptors<HaeaOperators<T>> descriptors;

        public HaeaOperators(RandomGenerator<double> generator, Descriptors<HaeaOperators<T>> descriptors)
        {
            this.generator = generator;
            this.descriptors = descriptors;
            this.roulette = new Roulette(new double[] { }, generator);
        }

        public abstract int numberOfOperatorsPerIndividual();
        public abstract int numberOfOperators();
        public abstract Variation<T> get(int indIndex, int operIndex);

        protected int select(double[] x)
        {
            roulette.setDensity(x);
            return roulette.next();
        }

        public void setSizeOffspring(int indIndex, int n)
        {
            size_offspring_sel_oper[indIndex] = n;
        }

        public int getSizeOffspring(int indIndex)
        {
            return size_offspring_sel_oper[indIndex];
        }

        public int select(int indIndex)
        {
            sel_oper[indIndex] = select(rates[indIndex]);
            return sel_oper[indIndex];
        }

        public void unselect(int indIndex) {
            sel_oper[indIndex] = -1;
        }

        public double[] reward(double[] individualRates, int operIndex)
        {
            if (operIndex >= 0)
            {
                individualRates[operIndex] *= 1 + generator.next();
                return ArrayTools.normalize(individualRates);
            }
            return individualRates;
        }

        public double[] punish(double[] individualRates, int operIndex)
        {
            if (operIndex >= 0)
            {
                individualRates[operIndex] *= 1 - generator.next();
                return ArrayTools.normalize(individualRates);
            }
            return individualRates;
        }

        public void reward(int indIndex)
        {
            rates[indIndex] = reward(rates[indIndex], sel_oper[indIndex]);
        }

        public void restart(int indIndex)
        {
            double[] r = new double[numberOfOperatorsPerIndividual()];
            for (int i = 0; i < r.Length; i++)
                r[i] = 1.0 / r.Length;
            rates[indIndex] = r;
        }

        public void punish(int indIndex)
        {
            rates[indIndex] = punish(rates[indIndex], sel_oper[indIndex]);
        }

        public void resize(int newSize)
        {
            if (rates.Count < newSize)
            {
                while (rates.Count < newSize)
                {
                    double[] r = new double[numberOfOperatorsPerIndividual()];
                    for (int j = 0; j < r.Length; j++)
                        r[j] = 1.0 / r.Length;
                    rates.Add(r);
                    sel_oper.Add(-1);
                    size_offspring_sel_oper.Add(1);
                }
            }
            else
            {
                while (newSize < rates.Count)
                {
                    int index = rates.Count - 1;
                    rates.RemoveAt(index);
                    sel_oper.RemoveAt(index);
                    size_offspring_sel_oper.RemoveAt(index);
                }
            }
        }

        public double[] getRates(int indIndex) {
            return rates[indIndex];
        }

        public List<double[]> getRates() {
            return rates;
        }
    }
}