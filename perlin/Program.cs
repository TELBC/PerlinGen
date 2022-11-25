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
            Random rand = new Random();
            for (int i = 0; i < size*size; i++)
            {
                list.Add(new Vector2D((float)rand.NextDouble(),(float)rand.NextDouble()));
            }

            Grid grid = new Grid(size, list);
            float[,] arr = grid.Matrix;
            float[,] interpolated = bilinearInterpolation(arr,size);
            Program prog = new Program();
            Bitmap image = prog.BytearrtobtBitmapm(size, interpolated);
            image.Save(@"C:\Users\Trist\RiderProjects\perlin\perlin\files\pic.jpg");
            sw.Stop();
            Console.WriteLine(size+"-bit | "+sw.ElapsedMilliseconds+"ms");
            sw.Reset();
        }

        public float[,] bilinearInterpolation(float[,] arr,int size)
        {
            float[,] list=new float[size,size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x+1 < size && y+1 < size)
                    {
                        list[x, y] = (arr[x, y] + arr[x, y + 1] + arr[x + 1, y] + arr[x + 1, y + 1]) / 4;
                    }
                    else break;
                }
            }

            return list;
        }
        
        public Bitmap BytearrtobtBitmapm(int size, float[,] data)
        {
            Bitmap bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
            int val;
            Color pixel;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    val = (int)(data[x,y]*255);
                    if (val > 255) val = 255;
                    pixel = Color.FromArgb(255,val,val,val);
                    bitmap.SetPixel(x, y, pixel);
                }
            }

            return bitmap;
        } 
    }
}