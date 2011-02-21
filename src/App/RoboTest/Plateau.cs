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

            if (this.IsRoverAtCoordinate(rover.Coordinate))
            {
                throw new InvalidOperationException("Cannot place rover on top of another rover.");
            }

            this.rovers.Add(rover);
            rover.OnMoving(this.CheckRoverDoesntLeavePlateau);
            rover.OnMoving(this.CheckWhetherAnotherRoverExistsAtCoordinate);
        }

        private bool IsRoverAtCoordinate(Coordinate coordinate)
        {
            foreach (var existingRover in this.rovers)
            {
                if (existingRover.Coordinate == coordinate)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckWhetherAnotherRoverExistsAtCoordinate(Coordinate arg)
        {
            return this.IsRoverAtCoordinate(arg);
        }

        private bool CheckRoverDoesntLeavePlateau(Coordinate arg)
        {
            return this.size.Contains(arg);
        }
    }
}
