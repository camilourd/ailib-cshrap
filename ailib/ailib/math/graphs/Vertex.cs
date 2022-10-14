using System;
using System.Collections.Generic;

namespace ailib.math.graphs
{
    public class Vertex<T>
    {
        public List<Edge<T>> edges = new List<Edge<T>>();

        public void link(int vertex, T weight)
        {
            if (edges.Count == 0)
                edges.Add(new Edge<T>(vertex, weight));
            else
            {
                int index = find(vertex);
                if (edges[index].vertex == vertex)
                    edges[index].weight = weight;
                else
                    edges.Insert(index, new Edge<T>(vertex, weight));
            }
        }

        public bool isLinked(int goal)
        {
            if (edges.Count == 0)
                return false;
            return edges[find(goal)].vertex == goal;
        }

        public T getWeight(int vertex) {
            return edges[find(vertex)].weight;
        }

        private int find(int vertex)
        {
            int li = 0, lo = edges.Count - 1;
            while (li < lo)
            {
                int mid = (li + lo) / 2;
                if (edges[mid].vertex < vertex)
                    li = mid + 1;
                else
                    lo = mid;
            }
            return li;
        }

        public int length()
        {
            return edges.Count;
        }
    }
}
