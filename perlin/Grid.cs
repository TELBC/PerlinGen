namespace perlin
{
    public class Grid
    {
        public Grid(int size, Vector2D[,] vectorlist)
        {
            Size = size;
            // var list = new float[size, size];
            // for (var x = 0; x < size; x++)
            // for (var y = 0; y < size; y++)
            //     list[x, y] = vectorlist[x + size * y].Magnitude();
            Matrix = vectorlist;
        }

        // public float[,] Matrix { get; set; }
        public Vector2D[,] Matrix { get; set; }
        public int Size { get; set; }
    }
}