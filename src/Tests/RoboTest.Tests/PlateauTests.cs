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
    }
}
