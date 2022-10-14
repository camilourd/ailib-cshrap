namespace ailib.math.activation_functions
{
    public class LeakyReLU : ActivationFunction
    {
        double leak;

        public LeakyReLU() : this(0.01)
        {
        }

        public LeakyReLU(double leak)
        {
            this.leak = leak;
        }

        public double activate(double x)
        {
            return (x > 0) ? x : leak * x;
        }

        public double derivate(double x)
        {
            return (x > 0) ? 1 : leak;
        }
    }
}
