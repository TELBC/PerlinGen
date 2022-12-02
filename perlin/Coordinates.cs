namespace perlin
{
    public class Coordinates
    {
        public Vector2D Vector;

        public Coordinates(int x, int y, Vector2D vec)
        {
            X = x;
            Y = y;
            Vector = vec;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}