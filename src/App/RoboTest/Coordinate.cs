using System;

namespace RoboTest
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public static bool operator ==(Coordinate a, Coordinate y)
        {
            return a.Equals(y);
        }

        public static bool operator !=(Coordinate a, Coordinate y)
        {
            return !(a == y);
        }

        public bool Equals(Coordinate other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.X.Equals(other.X) && this.Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate)
            {
                return this.Equals(obj as Coordinate);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            // Should implement using the X and Y properties
            return base.GetHashCode();
        }
    }
}