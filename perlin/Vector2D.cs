using System;

namespace perlin
{
    class Vector2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Dot(Vector2D other)
        {
            return X * other.X + Y * other.Y;
        }
    }
}