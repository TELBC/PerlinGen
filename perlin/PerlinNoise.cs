using System;

namespace perlin
{
    class PerlinNoise
    {
        private readonly int width;
        private readonly int height;
        private readonly int octaves;
        private readonly float persistence;

        public PerlinNoise(int width, int height, int octaves, float persistence)
        {
            this.width = width;
            this.height = height;
            this.octaves = octaves;
            this.persistence = persistence;
        }

        public float[,] GenerateNoise(Grid grid)
        {
            float[,] noise = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Coordinates coordinates = new Coordinates(x, y);
                    noise[x, y] = GeneratePerlinValue(coordinates, grid);
                }
            }

            return NormalizeAndAdjustContrast(noise);
        }

        private float GeneratePerlinValue(Coordinates coordinates, Grid grid)
        {
            float total = 0;
            float frequency = 1;
            float amplitude = 1;
            float maxValue = 0;

            for (int i = 0; i < octaves; i++)
            {
                float sampleX = coordinates.X / (float)width * frequency;
                float sampleY = coordinates.Y / (float)height * frequency;

                total += grid.InterpolateNoise(sampleX, sampleY) * amplitude;
                maxValue += amplitude;

                amplitude *= persistence;
                frequency *= 2;
            }

            return total / maxValue;
        }

        private float[,] NormalizeAndAdjustContrast(float[,] noise)
        {
            float minNoise = float.MaxValue;
            float maxNoise = float.MinValue;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (noise[x, y] < minNoise) minNoise = noise[x, y];
                    if (noise[x, y] > maxNoise) maxNoise = noise[x, y];
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    noise[x, y] = (noise[x, y] - minNoise) / (maxNoise - minNoise);
                    noise[x, y] = (float)Math.Pow(noise[x, y], 0.5f);
                }
            }

            return noise;
        }
    }
}