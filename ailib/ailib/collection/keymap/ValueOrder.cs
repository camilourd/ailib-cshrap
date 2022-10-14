using ailib.sort;

namespace ailib.collection.keymap
{
    public class ValueOrder<K, V> : Order<KeyValue<K, V>>
    {
        protected Order<V> order;

        public ValueOrder(Order<V> values_order) {
            this.order = values_order;
        }

        public int compare(KeyValue<K, V> one, KeyValue<K, V> two)
        {
            return order.compare(one.getValue(), two.getValue());
        }

        public bool equals(KeyValue<K, V> one, KeyValue<K, V> two)
        {
            return compare(one, two) == 0;
        }
    }
}
