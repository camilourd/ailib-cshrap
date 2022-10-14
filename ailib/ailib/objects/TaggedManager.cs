namespace ailib.objects
{
    public class TaggedManager<T>
    {
        public virtual Tagged<T> wrap(T value)
        {
            return new Tagged<T>(value);
        }

        public virtual T unwrap(Tagged<T> obj)
        {
            return obj.unwrap();
        }

        public virtual T[] unwrap(Tagged<T>[] obj)
        {
            T[] t_obj = new T[obj.Length];
            for (int i = 0; i < obj.Length; i++)
                t_obj[i] = obj[i].unwrap();
            return t_obj;
        }

        public virtual Tagged<T>[] wrap(T[] obj)
        {
            Tagged<T>[] t_obj = new Tagged<T>[obj.Length];
            for (int i = 0; i < obj.Length; i++) t_obj[i] = wrap(obj[i]);
            return t_obj;
        }
    }
}
