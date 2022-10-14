namespace ailib.math.logic
{
    public abstract class Predicate<T>
    {
        public abstract bool evaluate(T obj);
        public virtual void init() { }
    }
}
