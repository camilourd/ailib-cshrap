namespace ailib.sort
{
    public interface Order<T>
    {
        int compare(T one, T two);
        bool equals(T one, T two);
    }
}
