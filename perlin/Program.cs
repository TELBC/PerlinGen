using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace perlin
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            var image =  PerlinNoise(512, 120);
            image.Save(@"C:\Users\Trist\RiderProjects\perlin\perlin\files\pic.jpg");
            sw.Stop();
            Console.WriteLine(image.Height + "-bit | " + sw.ElapsedMilliseconds + "ms");
        }
        //fractal noise attempt 
        // private static Bitmap FractalNoise(int size, float period, float oct, float att)
        // {
        //     for (int octave = 0; octave < oct; octave++)
        //     {
        //         var periodV = period * Math.Pow(1 / 2, octave);
        //         var accentuation = Math.Pow(att, octave);
        //         var list = new Vector2D[size, size];
        //         var rand = new Random();
        //         for (var y = 0; y < size; y++)
        //         for (var x = 0; x < size; x++)
        //             list[x, y] = new Vector2D((float)rand.NextDouble(), (float)rand.NextDouble());
        //
        //         var grid = new Grid(size, list);
        //         var vectorList = grid.Matrix;
        //     }
        //     return B2Btm(size, arr);
        // }
        private static Vector2D[,] RandomGen(int size)
        {
            var list = new Vector2D[size, size];
            var rand = new Random();
            for (var y = 0; y < size; y++)
            for (var x = 0; x < size; x++)
                list[x, y] = new Vector2D((float)rand.NextDouble(), (float)rand.NextDouble());

            return new Grid(size, list).Matrix;
        }
        private static Bitmap PerlinNoise(int size, float period)
        {
            var vectorList = RandomGen(size);
            var arr = new float[size, size];
            for (var y = 0; y < size; y++)
            for (var x = 0; x < size; x++)
            {
                arr[x, y] = 128+Noise(x, y, vectorList, period)*128;
            }
            return B2Btm(size, arr);
        }
        //interpolate2d(grid[,],method) method=linear,cubic,nearest,spline,makima
        // public float[,] bilinearInterpolation(float[,] arr, int size)
        // {
        //     for (var y = 0; y < size; y++)
        //     for (var x = 0; x < size; x++)
        //         if (x >= size - 1 && y >= size - 1)
        //             arr[x, y] = (arr[x, y] + arr[x, y - 1] + arr[x - 1, y] + arr[x - 1, y - 1]) / 4;
        //         else
        //         {
        //             if (x >= size - 1) arr[x, y] = (arr[x, y] + arr[x, y + 1] + arr[x - 1, y] + arr[x - 1, y + 1]) / 4;
        //             else if (y >= size - 1)
        //                 arr[x, y] = (arr[x, y] + arr[x, y - 1] + arr[x + 1, y] + arr[x + 1, y - 1]) / 4;
        //             else arr[x, y] = (arr[x, y] + arr[x, y + 1] + arr[x + 1, y] + arr[x + 1, y + 1]) / 4;
        //         }
        //
        //
        //     return arr;
        // }
        private static float Noise(int x, int y, Vector2D[,] gradVectors, float period)
        {
            var cellX = (int)Math.Floor(x / period);
            var cellY = (int)Math.Floor(y / period);
            var relativeX = Fade((x - cellX * period) / period);
            var relativeY = Fade((y - cellY * period) / period);
            
            var topLeftGradient = gradVectors[cellX,cellY];
            var topRightGradient = gradVectors[cellX+1,cellY];
            var bottomLeftGradient = gradVectors[cellX, cellY+1];
            var bottomRightGradient= gradVectors[cellX+1, cellY+1];

            var topLeft = DotProduct(topLeftGradient, relativeX, relativeY);
            var topRight = DotProduct(topRightGradient,relativeX-1,relativeY);
            var bottomLeft = DotProduct(bottomLeftGradient,relativeX,relativeY-1);
            var bottomRight = DotProduct(bottomRightGradient, relativeX - 1, relativeY - 1);

            var top = Lerp(topLeft, topRight, relativeX);
            var bottom = Lerp(bottomLeft, bottomRight, relativeX);

            return (float)(Lerp(top, bottom, relativeY) / (Math.Sqrt(2) / 2));
        }
        private static float DotProduct(Vector2D vec1, float x,float y)
        {
            return vec1.X * x + vec1.Y * y;
        }
        private static float Fade(float x)
        {
            return (float)(6 * Math.Pow(x, 5) - 15 * Math.Pow(x, 4) + 10 * Math.Pow(x, 3));
        }
        private static float Lerp(float p0, float p1, float t)
        {
            var max = Math.Max(p0, p1);
            var min = Math.Min(p0, p1);
            if (p0 > p1) t = 1 - t;
            return (max - min) * t + min;
        }
        private static Bitmap B2Btm(int size, float[,] data)
        {
            var bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
            int val;
            Color pixel;
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
            {
                val = (int)data[x, y];
                pixel = Color.FromArgb(255, val, val, val);
                bitmap.SetPixel(x, y, pixel);
            }

            return bitmap;
        }
    }
}