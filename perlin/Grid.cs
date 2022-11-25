using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace perlin
{
    public class Grid
    {
        public float[,] Matrix { get; set; }
        public int Size { get; set; }

        public Grid(int size, List<Vector2D> vectorlist)
        {
            Size = size;
            float[,] list =new float[size,size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    list[x,y]=vectorlist[x+size*y].Magnitude();
                }
            }
            Matrix = list;
        }
    }

}