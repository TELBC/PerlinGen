using System;

namespace perlin
{
    class Grid
    {
        private readonly int width;
        private readonly int height;
        private readonly float[,] gradients;

        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;
            gradients = GenerateGradients();
        }

        private float[,] GenerateGradients()
        {
            Random random = new Random();
            float[,] gradients = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gradients[x, y] = (float)random.NextDouble() * 2 - 1;
                }
            }

            return gradients;
        }

        public float InterpolateNoise(float x, float y)
        {
            int x0 = (int)Math.Floor(x);
            int x1 = x0 + 1;
            int y0 = (int)Math.Floor(y);
            int y1 = y0 + 1;

            float sx = MathHelper.Fade(x - x0);
            float sy = MathHelper.Fade(y - y0);

            float n0 = DotGridGradient(MathHelper.WrapIndex(x0, width), MathHelper.WrapIndex(y0, height), x, y);
            float n1 = DotGridGradient(MathHelper.WrapIndex(x1, width), MathHelper.WrapIndex(y0, height), x, y);
            float ix0 = MathHelper.Lerp(n0, n1, sx);

            n0 = DotGridGradient(MathHelper.WrapIndex(x0, width), MathHelper.WrapIndex(y1, height), x, y);
            n1 = DotGridGradient(MathHelper.WrapIndex(x1, width), MathHelper.WrapIndex(y1, height), x, y);
            float ix1 = MathHelper.Lerp(n0, n1, sx);

            return MathHelper.Lerp(ix0, ix1, sy);
        }

        private float DotGridGradient(int ix, int iy, float x, float y)
        {
            Vector2D gradient = new Vector2D(gradients[ix, iy], gradients[ix, iy]);
            Vector2D distance = new Vector2D(x - ix, y - iy);

            return gradient.Dot(distance);
        }
    }
}