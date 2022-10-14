using System;

namespace ailib.sort.real
{
    public class RealOrder : Order<double>
    {
        bool minimize = false;

        public RealOrder() : this(false) { }

        public RealOrder(bool minimize)
        {
            this.minimize = minimize;
        }

        public int compare(double one, double two)
        {
            if (minimize)
                return Math.Abs(one - two) < 1e-9 ? 0 : (one < two ? 1 : -1);
            return Math.Abs(one - two) < 1e-9 ? 0 : (one < two ? -1 : 1);
        }

        public bool equals(double one, double two)
        {
            return Math.Abs(one - two) < 1e-9;
        }
    }
}
