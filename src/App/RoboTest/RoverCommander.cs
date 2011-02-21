using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboTest
{
    public class RoverCommander
    {
        private Plateau plateau;

        public void SendInstructions(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
            {
                throw new ArgumentNullException("instructions");
            }

            var lines = instructions.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // there must be at least the plateau size and 1 rover present in the instructions
            if (lines.Length < 3)
            {
                throw new ArgumentException("Incorrect number of instructions present.");
            }

            // rovers require 2 lines of instructions
            if ((lines.Length - 1) % 2 != 0)
            {
                throw new ArgumentException("Invalid number of instructions per rover.");
            }

            this.CreatePlateau(lines[0]);

            for (int i = 1; i < lines.Length; i = i + 2)
            {
                var rover = this.CreateRover(lines[i]);
                var roverInstructions = this.CreateRoverInstructions(lines[i + 1]);
                this.plateau.PlaceRover(rover);

                foreach (var instruction in roverInstructions)
                {
                    rover.SendInstruction(instruction);
                }
            }
        }

        public Rover[] GetRovers()
        {
            return this.plateau.GetRovers();
        }

        private void CreatePlateau(string s)
        {
            var expression = new Regex(@"(?<Width>\d+) (?<Height>\d+)");
            var matches = expression.Match(s);

            if (matches.Success)
            {
                var width = Convert.ToInt32(matches.Groups["Width"].Value);
                var height = Convert.ToInt32(matches.Groups["Height"].Value);

                this.plateau = new Plateau(new Size(width + 1, height + 1));
            }
            else
            {
                throw new InvalidOperationException("Unable to parse the plateau size.");
            }
        }

        private RoverInstruction[] CreateRoverInstructions(string s)
        {
            var expression = new Regex(@"(?<Instruction>[LMR])+");
            var matches = expression.Match(s);

            if (matches.Success)
            {
                var instructions = new List<RoverInstruction>();

                foreach (Group @group in matches.Groups)
                {
                    if (@group.Length == 1)
                    {
                        foreach (Capture capture in @group.Captures)
                        {
                            instructions.Add((RoverInstruction)Enum.Parse(typeof(RoverInstruction), capture.Value));
                        }
                    }
                }

                return instructions.ToArray();
            }
            else
            {
                throw new InvalidOperationException("Unknown instructions present.");
            }
        }

        private Rover CreateRover(string s)
        {
            var expression = new Regex(@"(?<X>\d+) (?<Y>\d+) (?<Direction>[NSEW])");
            var matches = expression.Match(s);

            if (matches.Success)
            {
                var x = Convert.ToInt32(matches.Groups["X"].Value);
                var y = Convert.ToInt32(matches.Groups["Y"].Value);
                var directionValue = matches.Groups["Direction"].Value;
                var cameraDirection = CameraDirection.North;

                if (directionValue == "N")
                {
                    cameraDirection = CameraDirection.North;
                }
                else if (directionValue == "S")
                {
                    cameraDirection = CameraDirection.South;
                }
                else if (directionValue == "E")
                {
                    cameraDirection = CameraDirection.East;
                }
                else if (directionValue == "W")
                {
                    cameraDirection = CameraDirection.West;
                }

                var coordinate = new Coordinate(x, y);

                return new Rover(coordinate, cameraDirection);
            }
            else
            {
                throw new InvalidOperationException("Unable to parse robot position");
            }
        }
    }
}