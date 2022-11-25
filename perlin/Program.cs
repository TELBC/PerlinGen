using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace perlin
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Program prog = new Program();
            prog.runner(512,sw);
        }

        public void runner(int size,Stopwatch sw)
        {
            sw.Start();
            List<Vector2D> list = new List<Vector2D>();
            var rand = new Random();
            for (int i = 0; i < size*size; i++)
            {
                list.Add(new Vector2D((float)rand.NextDouble(),(float)rand.NextDouble()));
            }

            Grid grid = new Grid(size, list);
            byte[] arr = grid.createGrid();
            byte[] interpolated = interpolation(arr);
            Program prog = new Program();
            Bitmap image = prog.BytearrtobtBitmapm(size, size, interpolated);
            image.Save(@"C:\Users\Trist\RiderProjects\perlin\perlin\files\pic.jpg");
            sw.Stop();
            Console.WriteLine(size+"-bit | "+sw.ElapsedMilliseconds+"ms");
            sw.Reset();
        }

        public byte[] interpolation(byte[] arr)
        {
            var max = arr.Length;
            byte[] interpolated = new byte[max];
            for (int i = 0; i < max-1; i++)
            {
                var mu2 = (1-Math.Cos(arr[i]*Math.PI))/2;
                float point = (float)(i*(1-mu2)+(i+1)*mu2);
                    interpolated[i] = (byte)(point*255);
            }
            return interpolated;
        }
        public Bitmap BytearrtobtBitmapm(int width, int height, byte[] data)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var val = data[y * width + x];
                    Color pixel = Color.FromArgb(255,val,val,val);
                    bitmap.SetPixel(x, y, pixel);
                }
            }

            return bitmap;
        } 
    }
}