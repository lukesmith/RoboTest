using System;
using NUnit.Framework;

namespace RoboTest.Tests
{
    [TestFixture]
    public class PlateauTests
    {
        [TestCase(10, 9)]
        [TestCase(9, 10)]
        public void rover_must_be_placeable(int x, int y)
        {
            var mars = new Plateau(new Size(10, 10));

            Assert.Throws<InvalidOperationException>(() => mars.PlaceRover(new Rover(new Coordinate(x, y), CameraDirection.North)));
        }

        [Test]
        public void cannot_place_rover_where_another_rover_is()
        {
            var mars = new Plateau(new Size(10, 10));
            mars.PlaceRover(new Rover(new Coordinate(2, 2), CameraDirection.North));

            Assert.Throws<InvalidOperationException>(() => mars.PlaceRover(new Rover(new Coordinate(2, 2), CameraDirection.North)));
        }

        [TestCase(0, 0, CameraDirection.West)]
        [TestCase(0, 0, CameraDirection.East)]
        [TestCase(0, 0, CameraDirection.South)]
        [TestCase(0, 0, CameraDirection.North)]
        public void cannot_move_rover_outside_of_plateau(int x, int y, CameraDirection cameraDirection)
        {
            var mars = new Plateau(new Size(1, 1));
            var rover = new Rover(new Coordinate(x, y), cameraDirection);
            mars.PlaceRover(rover);

            Assert.Throws<RoverCannotMoveException>(() => rover.SendInstruction(RoverInstruction.M));
        }

        [Test]
        public void cannot_move_rover_if_it_will_collide_with_another_rover()
        {
            var mars = new Plateau(new Size(3, 3));
            var rover = new Rover(new Coordinate(1, 1), CameraDirection.North);
            mars.PlaceRover(rover);

            var rover2 = new Rover(new Coordinate(1, 2), CameraDirection.South);
            mars.PlaceRover(rover2);

            Assert.Throws<RoverCannotMoveException>(() => rover2.SendInstruction(RoverInstruction.M));
        }
    }
}
