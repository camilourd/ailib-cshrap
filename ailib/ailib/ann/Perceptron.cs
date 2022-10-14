using ailib.math.activation_functions;
using ailib.utils;

namespace ailib.math.ann
{
    public abstract class Perceptron<W, I>
    {
        protected W[] weights;
        protected ActivationFunction function;
        protected double beta = 0;

        public Perceptron(W[] weights, double beta, ActivationFunction function)
        {
            this.weights = weights;
            this.beta = beta;
            this.function = function;
        }

        public Perceptron(W[] weights, double beta) : this(weights, beta, new Sigmoid())
        {
        }

        public double weighted_input(I input)
        {
            return weighted_input(Location.ZERO, input);
        }

        public abstract double weighted_input(Location start, I input);

        public double propagate(I input)
        {
            return activate(weighted_input(input));
        }

        public double propagate(Location start, I input)
        {
            return activate(weighted_input(start, input));
        }

        public double activate(double weighted_sum)
        {
            return function.activate(weighted_sum + beta);
        }

        public W this[int index]
        {
            get
            {
                return weights[index];
            }

            set
            {
                weights[index] = value;
            }
        }

        public void setActivationFunction(ActivationFunction function)
        {
            this.function = function;
        }
    }
}
