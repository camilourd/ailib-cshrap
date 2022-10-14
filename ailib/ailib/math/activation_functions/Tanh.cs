using System;

namespace ailib.math.activation_functions
{
    class Tanh : ActivationFunction
    {
        public double activate(double x)
        {
            return Math.Tanh(x);
        }

        public double derivate(double x)
        {
            return Math.Tanh(x);
        }
    }
}
