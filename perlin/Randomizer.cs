using System;

namespace perlin
{
    public class Randomizer
    {
        public float nextFlt()
        {
            UInt32 seed = (UInt32)DateTime.Now.Millisecond;
            seed ^= seed << 13;
            seed ^= seed >> 17;
            seed ^= seed << 5;
            return (float)seed/UInt32.MaxValue;
        }
    }
}