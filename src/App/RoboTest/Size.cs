using System;

namespace RoboTest
{
    public class Size
    {
        private readonly int width;
        private readonly int height;

        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;

            if (width <= 0)
            {
                throw new ArgumentException("width");
            }

            if (height <= 0)
            {
                throw new ArgumentException("height");
            }
        }

        public bool Contains(Coordinate coordinate)
        {
            if (coordinate.X >= 0 && coordinate.X < this.width)
            {
                if (coordinate.Y >= 0 && coordinate.Y < this.height)
                {
                    return true;
                }
            }

            return false;
        }
    }
}