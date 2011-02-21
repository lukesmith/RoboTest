using System;
using System.Collections.Generic;

namespace RoboTest
{
    public class Plateau
    {
        private readonly Size size;
        private readonly IList<Rover> rovers;

        public Plateau(Size size)
        {
            this.size = size;
            this.rovers = new List<Rover>();
        }

        public void PlaceRover(Rover rover)
        {
            if (!this.size.Contains(rover.Coordinate))
            {
                throw new InvalidOperationException("Cannot place a rover outside of the plateau.");
            }

            foreach (var existingRover in this.rovers)
            {
                if (existingRover.Coordinate == rover.Coordinate)
                {
                    throw new InvalidOperationException("Cannot place rover on top of another rover.");
                }
            }

            this.rovers.Add(rover);
            rover.AddToPlateau(this);
        }
    }
}
