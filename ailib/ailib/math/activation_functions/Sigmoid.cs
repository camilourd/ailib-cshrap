using System;

namespace ailib.math.activation_functions
{
    public class Sigmoid : ActivationFunction
    {
        double ActivationFunction.activate(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        double ActivationFunction.derivate(double x)
        {
            double val = Math.Exp(-x);
            return val / Math.Pow(1.0 + val, 2.0) ;
        }
    }
}
