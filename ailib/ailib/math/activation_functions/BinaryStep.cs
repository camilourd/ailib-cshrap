namespace ailib.math.activation_functions
{
    public class BinaryStep : ActivationFunction
    {
        double ActivationFunction.activate(double x)
        {
            return (x < 0.0) ? 0.0 : 1.0;
        }

        double ActivationFunction.derivate(double x)
        {
            return (x == 0) ? 1.0 : 0.0;
        }
    }
}
