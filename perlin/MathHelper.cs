namespace perlin
{
    class MathHelper
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + t * (b - a);
        }

        public static float Fade(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        public static int Clamp(int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static int WrapIndex(int value, int max)
        {
            return (value % max + max) % max;
        }
    }
}