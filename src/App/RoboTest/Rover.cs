using System;

namespace RoboTest
{
    public class Rover
    {
        private Plateau plateau;

        public Rover(Coordinate coordinate, CameraDirection cameraDirection)
        {
            Coordinate = coordinate;
            CameraDirection = cameraDirection;
        }

        public Coordinate Coordinate { get; set; }
        
        public CameraDirection CameraDirection { get; set; }

        public void AddToPlateau(Plateau plateau)
        {
            this.plateau = plateau;
        }

        public override string ToString()
        {
            char direction;

            switch (this.CameraDirection)
            {
                case RoboTest.CameraDirection.West:
                    direction = 'W';
                    break;
                case RoboTest.CameraDirection.East:
                    direction = 'E';
                    break;
                case RoboTest.CameraDirection.South:
                    direction = 'S';
                    break;
                case RoboTest.CameraDirection.North:
                    direction = 'N';
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return string.Format("{0} {1} {2}", this.Coordinate.X, this.Coordinate.Y, direction);
        }

        public void SendInstruction(RoverInstruction instruction)
        {
            switch (instruction)
            {
                case RoverInstruction.L:
                    if (this.CameraDirection == CameraDirection.North)
                    {
                        this.CameraDirection = CameraDirection.West;
                    }
                    else if (this.CameraDirection == CameraDirection.East)
                    {
                        this.CameraDirection = CameraDirection.North;
                    }
                    else if (this.CameraDirection == CameraDirection.South)
                    {
                        this.CameraDirection = CameraDirection.East;
                    }
                    else if (this.CameraDirection == CameraDirection.West)
                    {
                        this.CameraDirection = CameraDirection.South;
                    }

                    break;
                case RoverInstruction.R:
                    if (this.CameraDirection == CameraDirection.North)
                    {
                        this.CameraDirection = CameraDirection.East;
                    }
                    else if (this.CameraDirection == CameraDirection.East)
                    {
                        this.CameraDirection = CameraDirection.South;
                    }
                    else if (this.CameraDirection == CameraDirection.South)
                    {
                        this.CameraDirection = CameraDirection.West;
                    }
                    else if (this.CameraDirection == CameraDirection.West)
                    {
                        this.CameraDirection = CameraDirection.North;
                    }

                    break;

                case RoverInstruction.M:
                    {
                        Coordinate newCoordinate;

                        if (this.CameraDirection == CameraDirection.North)
                        {
                            newCoordinate = new Coordinate(this.Coordinate.X, this.Coordinate.Y + 1);
                        }
                        else if (this.CameraDirection == CameraDirection.East)
                        {
                            newCoordinate = new Coordinate(this.Coordinate.X + 1, this.Coordinate.Y);
                        }
                        else if (this.CameraDirection == CameraDirection.South)
                        {
                            newCoordinate = new Coordinate(this.Coordinate.X, this.Coordinate.Y - 1);
                        }
                        else if (this.CameraDirection == CameraDirection.West)
                        {
                            newCoordinate = new Coordinate(this.Coordinate.X - 1, this.Coordinate.Y);
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }

                        this.Coordinate = newCoordinate;
                        break;
                    }
            }
        }
    }
}