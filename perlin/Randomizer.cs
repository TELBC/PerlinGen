using System;

namespace perlin
{
    public class Randomizer
    {
        public float nextFlt()
        {
            var seed = (uint)DateTime.Now.Millisecond;
            seed ^= seed << 13;
            seed ^= seed >> 17;
            seed ^= seed << 5;
            return (float)seed / (uint.MaxValue / 10);
        }
    }
}