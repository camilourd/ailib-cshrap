namespace ailib.ann
{
    public interface ANN<I, O>
    {
        O propagate(I input);
    }
}
