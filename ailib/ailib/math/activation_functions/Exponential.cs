using System;

namespace ailib.math.activation_functions
{
    public class Exponential : ActivationFunction
    {
        public double activate(double x)
        {
            return Math.Exp(x);
        }

        public double derivate(double x)
        {
            return x * Math.Exp(x);
        }
    }
}
