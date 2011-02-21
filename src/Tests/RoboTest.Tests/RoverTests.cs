using NUnit.Framework;

namespace RoboTest.Tests
{
    [TestFixture]
    public class RoverTests
    {
        [Test]
        public void gets_rover_location()
        {
            var rover = new Rover(new Coordinate(2, 4), CameraDirection.West);

            Assert.AreEqual("2 4 W", rover.ToString());
        }

        [TestCase(RoverInstruction.L, CameraDirection.North, CameraDirection.West)]
        [TestCase(RoverInstruction.L, CameraDirection.East, CameraDirection.North)]
        [TestCase(RoverInstruction.L, CameraDirection.South, CameraDirection.East)]
        [TestCase(RoverInstruction.L, CameraDirection.West, CameraDirection.South)]
        [TestCase(RoverInstruction.R, CameraDirection.North, CameraDirection.East)]
        [TestCase(RoverInstruction.R, CameraDirection.East, CameraDirection.South)]
        [TestCase(RoverInstruction.R, CameraDirection.South, CameraDirection.West)]
        [TestCase(RoverInstruction.R, CameraDirection.West, CameraDirection.North)]
        public void changes_direction(RoverInstruction instruction, CameraDirection initialDirection, CameraDirection expectedDirection)
        {
            var rover = new Rover(new Coordinate(0, 0), initialDirection);

            rover.SendInstruction(instruction);

            Assert.AreEqual(expectedDirection, rover.CameraDirection);
        }

        [Test]
        public void moves_north()
        {
            var rover = new Rover(new Coordinate(0, 0), CameraDirection.North);

            rover.SendInstruction(RoverInstruction.M);

            Assert.AreEqual(new Coordinate(0, 1), rover.Coordinate);
        }

        [Test]
        public void moves_south()
        {
            var rover = new Rover(new Coordinate(0, 1), CameraDirection.South);

            rover.SendInstruction(RoverInstruction.M);

            Assert.AreEqual(new Coordinate(0, 0), rover.Coordinate);
        }

        [Test]
        public void moves_west()
        {
            var rover = new Rover(new Coordinate(1, 0), CameraDirection.West);

            rover.SendInstruction(RoverInstruction.M);

            Assert.AreEqual(new Coordinate(0, 0), rover.Coordinate);
        }

        [Test]
        public void moves_east()
        {
            var rover = new Rover(new Coordinate(0, 0), CameraDirection.East);

            rover.SendInstruction(RoverInstruction.M);

            Assert.AreEqual(new Coordinate(1, 0), rover.Coordinate);
        }
    }
}