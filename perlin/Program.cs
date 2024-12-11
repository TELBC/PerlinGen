using System;
using System.Drawing;
using System.Drawing.Imaging;
using perlin;

namespace Perlin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the width/height of the Perlin noise image:");
            int width = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            Console.WriteLine("OCTAVES \nLayers of detail in Perlin noise. More octaves increase complexity but reduce randomness \nEnter the number of octaves:");
            int octaves = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            Console.WriteLine("PERSISTENCE \nAmplitude scaling factor. Higher persistence emphasizes fine details, lower persistence smooths the noise \nEnter the persistence value (0.0 - 1.0):");
            float persistence = float.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            PerlinNoise perlinNoise = new PerlinNoise(width, width, octaves, persistence);
            Grid grid = new Grid(width, width);
            float[,] noise = perlinNoise.GenerateNoise(grid);

            SaveImage(noise, width, width);

            Console.WriteLine("Perlin noise image generated and saved as perlin_noise.jpg.");
        }

        static void SaveImage(float[,] noise, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int colorValue = MathHelper.Clamp((int)(noise[x, y] * 255), 0, 255);
                    bitmap.SetPixel(x, y, Color.FromArgb(colorValue, colorValue, colorValue));
                }
            }

            bitmap.Save("perlin_noise.jpg", ImageFormat.Jpeg);
        }
    }
}