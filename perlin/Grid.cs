using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace perlin
{
    public class Grid
    {
        public List<Coordinates> Matrix { get; set; }
        public int Size { get; set; }

        public Grid(int size, List<Vector2D> vectorlist)
        {
            Size = size;
            List<Coordinates> list = new List<Coordinates>();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    list.Add(new Coordinates(x,y,vectorlist[x+size*y]));
                }
            }

            Matrix = list;
        }

        public byte[] createGrid()
        {
            int f = Size * Size;
            byte[] arr = new byte[f];
            for (int i=0;i<f;i++)
            {
                arr[i]=(byte)Math.Round(Matrix[i].Vector.Magnitude());
            }

            return arr;
        }
    }

}