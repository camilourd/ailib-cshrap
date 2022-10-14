using System.Collections.Generic;

namespace ailib.sort
{
    public class Sorted<T>
    {
        protected Order<T> order;
        protected List<T> data = new List<T>();

        public Sorted(Order<T> order)
        {
            this.order = order;
        }

        public void add(T datum)
        {
            if (data.Count > 0)
            {
                int index = findIndex(datum);
                data.Insert(index, datum);
            }
            else
                data.Add(datum);
        }

        private int findIndex(T datum)
        {
            int li = 0, lo = data.Count - 1;
            while (li < lo)
            {
                int mid = (li + lo) / 2;
                if (order.compare(datum, data[mid]) >= 0)
                    li = mid + 1;
                else
                    lo = mid;
            }
            return (li == data.Count - 1) ? (order.compare(datum, data[data.Count - 1]) > 0 ? li + 1 : li) : li;
        }

        public T get(int i)
        {
            return data[i];
        }
    }
}
