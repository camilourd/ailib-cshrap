namespace ailib.structures
{
    public class DisjointSetUnion
    {
        private int[] parents;

        public DisjointSetUnion(int size)
        {
            this.restart(size);
        }

        public void restart(int size)
        {
            parents = new int[size];
            for (int i = 0; i < parents.Length; i++)
                parents[i] = i;
        }

        public int find(int v)
        {
            if (v == parents[v])
                return v;
            return find(parents[v]);
        }

        public void join(int a, int b)
        {
            int pa = find(a);
            int pb = find(b);
            if (pa != pb)
                parents[pa] = pb;
        }
    }
}
