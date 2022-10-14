using ailib.services.description;

namespace ailib.evolution.haea
{
    public class HaeaStepDescriptors<T> : Descriptors<HaeaStep<T>>
    {
        public double[] describe(HaeaStep<T> step)
        {
            return step.getOperators().descriptors.describe(step.getOperators());
        }
    }
}
