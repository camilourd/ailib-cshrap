using ailib.services.description;

namespace ailib.evolution.haea
{
    public class CahaeaStepDescriptors<T> : Descriptors<CahaeaStep<T>>
    {
        public double[] describe(CahaeaStep<T> step)
        {
            return step.getOperators().descriptors.describe(step.getOperators());
        }
    }
}
