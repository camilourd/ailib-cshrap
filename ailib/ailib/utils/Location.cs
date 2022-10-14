namespace ailib.utils
{
    public class Location
    {
        public int x, y, z;

        public static Location ZERO = new Location(0);

        public Location(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Location(int x, int y) : this(x, y, 0)
        {
        }

        public Location(int x) : this(x, 0)
        {
        }

    }
}
