using System;

namespace ailib.cluster.gravitational
{
    public class GravitationalPoint
    {
        public double weight;
        public double[] coordinates;

        public GravitationalPoint(double weight, double[] coordinates)
        {
            this.weight = weight;
            this.coordinates = coordinates;
        }

        public GravitationalPoint(double[] coordinates) : this(1, coordinates) { }

        public double distance(GravitationalPoint point) {
            double sum = 0;
            for (int i = 0; i < coordinates.Length; i++)
                sum += (point.coordinates[i] - coordinates[i]) * (point.coordinates[i] - coordinates[i]);
            return Math.Sqrt(sum);
        }
    }
}
