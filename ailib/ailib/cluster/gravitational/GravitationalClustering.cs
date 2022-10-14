using ailib.math.functions.iterative;
using ailib.random;
using ailib.structures;
using System;
using System.Collections.Generic;

namespace ailib.cluster.gravitational
{
    public class GravitationalClustering : ClusteringFuntion<GravitationalPoint[], DisjointSetUnion, Dictionary<int, List<int>>>
    {
        private RandomGenerator<double> generator;
        private double gravityConstant;
        private double alpha;
        private double decay;
        private double epsilon;
        private double currentGravityConstant;
        private int iteration;

        public GravitationalClustering(double gravityConstant, double alpha, double decay, double epsilon, int iterations, RandomGenerator<double> generator) : base(new ForLoopCondition<GravitationalPoint[]>(iterations), "GravitationalClustering") {
            this.gravityConstant = gravityConstant;
            this.decay = decay;
            this.alpha = alpha;
            this.epsilon = epsilon;
            this.generator = generator;
        }

        public GravitationalClustering(double gravityConstant, RandomGenerator<double> generator) : this(gravityConstant, 0.03, 0.01, 1e-4, 500, generator) { }

        public GravitationalClustering(RandomGenerator<double> generator) : this(7e-6, generator) { }//1e-4

        protected override DisjointSetUnion init(GravitationalPoint[] points)
        {
            this.iteration = 0;
            this.currentGravityConstant = gravityConstant;
            this.terminationCondition.init();
            DisjointSetUnion union = new DisjointSetUnion(points.Length);
            for (int i = 0; i < points.Length; i++)
                for (int j = 0; j < points.Length; j++) {
                    double dist = points[i].distance(points[j]);
                    if (dist * dist < epsilon)
                        union.join(i, j);
                }
            return union;
        }

        protected override DisjointSetUnion apply(GravitationalPoint[] points, DisjointSetUnion clusters)
        {
            for (int i = 0; i < points.Length; i++) {
                int k = next(points.Length, i);
                move(points[i], points[k]);
                double dist = points[i].distance(points[k]);
                if (dist * dist < epsilon)
                    clusters.join(i, k);
            }
            currentGravityConstant = (1 - decay) * currentGravityConstant;
            return clusters;
        }

        protected virtual void move(GravitationalPoint point1, GravitationalPoint point2)
        {
            double dist = point1.distance(point2);
            double gravity = dist > 1e-9 ? currentGravityConstant / Math.Pow(dist, 3) : 0;
            gravity = Math.Max(-1, Math.Min(1, gravity)) / 2;
            for (int i = 0; i < point1.coordinates.Length; i++)
            {
                double movement = gravity * (point2.coordinates[i] - point1.coordinates[i]);
                point1.coordinates[i] += movement * point2.weight;
                point2.coordinates[i] -= movement * point1.weight;
            }
        }

        private int next(int size, int ignore)
        {
            int k;
            do
            {
                k = (int)(generator.next() * size);
            } while (k == ignore);
            return k;
        }

        protected override Dictionary<int, List<int>> filter(GravitationalPoint[] input, DisjointSetUnion clusters)
        {
            Dictionary<int, List<int>> filter = new Dictionary<int, List<int>>();
            for (int i = 0; i < input.Length; i++) {
                int parent = clusters.find(i);
                if (!filter.ContainsKey(parent))
                    filter.Add(parent, new List<int>());
                filter[parent].Add(i);
            }
            int minPoints = (int)Math.Ceiling(alpha * input.Length);
            foreach (int key in filter.Keys)
                if (filter[key].Count < minPoints)
                    filter.Remove(key);
            return filter;
        }

        protected override void trace(GravitationalPoint[] points, DisjointSetUnion clusters)
        {
            Dictionary<int, bool> dict = new Dictionary<int, bool>();
            for (int i = 0; i < points.Length; i++)
                if (!dict.ContainsKey(clusters.find(i)))
                    dict.Add(clusters.find(i), true);
            Console.WriteLine("> " + iteration + ": " + currentGravityConstant + " -> " + dict.Keys.Count + " clusters");
            //Console.WriteLine(">>> (" + points[0].coordinates[0] + ", " + points[0].coordinates[1] + ")");
            iteration++;
        }
    }
}
