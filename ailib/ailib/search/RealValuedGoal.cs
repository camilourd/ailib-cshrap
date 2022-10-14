using ailib.sort;

namespace ailib.search
{
    public abstract class RealValuedGoal<T>: Goal<T, double>
    {
        public RealValuedGoal(string name, Order<double> order) : base(name, order) { }

        public abstract bool minimizing();
        public abstract void minimize(bool minimize);
    }
}
