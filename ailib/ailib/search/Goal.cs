using ailib.math.functions;
using ailib.objects;
using ailib.sort;

namespace ailib.search
{
    public abstract class Goal<T, R>: Function<T, R>
    {
        protected Order<R> order;
        public bool clean = false;

        public Goal(string name, Order<R> order): base(name)
        {
            this.order = order;
        }

        public virtual Order<R> getOrder()
        {
            return order;
        }

        public abstract R compute(T x);

        public int compare(T x, T y)
        {
            return order.compare(apply(x), apply(y));
        }

        public int compare(Tagged<T> x, Tagged<T> y) {
            return order.compare(apply(x), apply(y));
        }
        public abstract void init();
    }
}
