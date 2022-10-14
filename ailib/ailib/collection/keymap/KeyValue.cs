namespace ailib.collection.keymap
{
    public class KeyValue<K, V>
    {
        protected K key;
        protected V value;

		public KeyValue(K key, V value)
		{
			this.value = value;
			this.key = key;
		}

		public K getKey() { return key; }
		public V getValue() { return value; }

		public void setKey(K key) { this.key = key; }
		public void setValue(V value) { this.value = value; }

		public override string ToString() { return key.ToString() + ":" + value.ToString(); }
	}
}
