namespace ailib.math.activation_functions
{
    public class ReLU : ActivationFunction
    {
        public double activate(double x)
        {
            return (x > 0) ? x : 0;
        }

        public double derivate(double x)
        {
            return (x > 0) ? 1 : 0;
        }
    }
}
