namespace ailib.math
{
    public interface ActivationFunction
    {
        double activate(double x);
        double derivate(double x);
    }
}
