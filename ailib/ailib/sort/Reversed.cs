namespace ailib.sort
{
    public class Reversed<T> : Order<T>
    {
        protected Order<T> original_order;

        public Reversed(Order<T> _original_order) {
            original_order = _original_order;
        }

        public int compare(T one, T two)
        {
            return original_order.compare(two, one);
        }

        public bool equals(T one, T two)
        {
            return original_order.equals(one, two);
        }
    }
}
