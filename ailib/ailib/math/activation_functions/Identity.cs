namespace ailib.math.activation_functions
{
    public class Identity : ActivationFunction
    {
        double ActivationFunction.activate(double x)
        {
            return x;
        }

        double ActivationFunction.derivate(double x)
        {
            return x;
        }
    }
}
