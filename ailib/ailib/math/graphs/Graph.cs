using System.Collections.Generic;

namespace ailib.math.graphs
{
    public class Graph<T>
    {
        protected List<Vertex<T>> vertices = new List<Vertex<T>>();

        public Graph(int size)
        {
            for (int i = 0; i < size; i++)
                vertices.Add(new Vertex<T>());
        }

        public void link(int source, int goal, T weight)
        {
            vertices[source].link(goal, weight);
        }

        public bool isLinked(int source, int goal)
        {
            return vertices[source].isLinked(goal);
        }

        public T getWeight(int source, int goal)
        {
            return vertices[source].getWeight(goal);
        }

        public int size()
        {
            return vertices.Count;
        }

        public int length()
        {
            int cnt = 0;
            foreach (Vertex<T> vertex in vertices)
                cnt += vertex.length();
            return cnt;
        }
    }
}
