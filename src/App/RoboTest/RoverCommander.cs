using System;
using System.Collections.Generic;
using System.Linq;
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

            var lines = instructions.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if (lines.Length != 5)
            {
                throw new InvalidOperationException("Incorrect number of instructions");
            }

            this.CreatePlateau(lines[0]);
            var roverA = this.CreateRover(lines[1]);
            var roverB = this.CreateRover(lines[3]);

            var roverAInstructions = this.CreateRoverInstructions(lines[2]);
            var roverBInstructions = this.CreateRoverInstructions(lines[4]);

            this.plateau.PlaceRover(roverA);
            this.plateau.PlaceRover(roverB);

            foreach (var instruction in roverAInstructions)
            {
                roverA.SendInstruction(instruction);
            }

            foreach (var instruction in roverBInstructions)
            {
                roverB.SendInstruction(instruction);
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
                CameraDirection cameraDirection = CameraDirection.North;

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