namespace ailib.math.graphs
{
    public class Edge<T>
    {
        public int vertex;
        public T weight;

        public Edge(int vertex, T weight)
        {
            this.vertex = vertex;
            this.weight = weight;
        }
    }
}
