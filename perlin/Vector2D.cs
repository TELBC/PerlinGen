using System;

namespace perlin
{
    public class Vector2D
        {
            public float axisX { get; set; }
            public float axisY { get; set; }

            //grayscale is more intricate and populated
            //to implement add px,py in Program.cs
            public Vector2D(float px, float py, float qx, float qy)
            {
                axisX = qx-px;
                axisY = qy-py;
            }
            //not as crowded
            //default option
            //change in Program.cs
            public Vector2D(float qx, float qy)
            {
                axisX = qx;
                axisY = qy;
            }

            public double Magnitude()
            {
                return Math.Sqrt(axisX*axisX + axisY*axisY);
            }

            public double Direction()
            {
                return Math.Tanh(axisY / axisX);
            }
        }
}