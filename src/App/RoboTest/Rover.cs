using System;
using System.Collections.Generic;

namespace RoboTest
{
    public class Rover
    {
        private readonly IList<Func<Coordinate, bool>> movingCallbacks;

        public Rover(Coordinate coordinate, CameraDirection cameraDirection)
        {
            Coordinate = coordinate;
            CameraDirection = cameraDirection;
            this.movingCallbacks = new List<Func<Coordinate, bool>>();
        }

        public Coordinate Coordinate { get; set; }
        
        public CameraDirection CameraDirection { get; set; }

        public void OnMoving(Func<Coordinate, bool> callback)
        {
            this.movingCallbacks.Add(callback);
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

                        bool canMove = true;
                        foreach (var movingCallback in this.movingCallbacks)
                        {
                            canMove = movingCallback(newCoordinate);

                            if (canMove == false)
                            {
                                break;
                            }
                        }

                        if (canMove)
                        {
                            this.Coordinate = newCoordinate;
                        }
                        else
                        {
                            throw new RoverCannotMoveException();
                        }

                        break;
                    }
            }
        }
    }
}