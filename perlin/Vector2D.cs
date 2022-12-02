using System;

namespace perlin
{
    public class Vector2D
    {
        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public float Magnitude() //returns length of vector by calculating pythagoras
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public float Direction()
        {
            return (float)Math.Tanh(Y / X);
        }
    }
}