using System;
using NUnit.Framework;

namespace RoboTest.Tests
{
    [TestFixture]
    public class SizeTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        public void cannot_have_width(int width)
        {
            Assert.Throws<ArgumentException>(() => new Size(width, 1));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void cannot_have_height(int height)
        {
            Assert.Throws<ArgumentException>(() => new Size(1, height));
        }

        [TestCase(0, 0)]
        [TestCase(4, 4)]
        [TestCase(2, 2)]
        public void contains(int x, int y)
        {
            var region = new Size(5, 5);

            Assert.IsTrue(region.Contains(new Coordinate(x, y)));
        }

        [TestCase(5, 4)]
        [TestCase(4, 5)]
        public void does_not_contain(int x, int y)
        {
            var region = new Size(5, 5);

            Assert.IsFalse(region.Contains(new Coordinate(x, y)));
        }
    }
}