using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RoboTest.Tests
{
    [TestFixture]
    public class send_rover_instructions
    {
        [Test]
        public void send_instructions()
        {
            var instructions = @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM";
            var roverCommander = new RoverCommander();
            roverCommander.SendInstructions(instructions);

            var robots = roverCommander.GetRovers();

            Assert.AreEqual("1 3 N", robots[0].ToString());
            Assert.AreEqual("5 1 E", robots[1].ToString());
        }

        [Test]
        public void send_instructions_for_3_rovers()
        {
            var instructions = @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
3 1 N
ML";
            var roverCommander = new RoverCommander();
            roverCommander.SendInstructions(instructions);

            var robots = roverCommander.GetRovers();

            Assert.AreEqual("1 3 N", robots[0].ToString());
            Assert.AreEqual("5 1 E", robots[1].ToString());
            Assert.AreEqual("3 2 W", robots[2].ToString());
        }

        [Test]
        public void sends_incorrect_instructions_for_rover()
        {
            var instructions = @"5 5
1 2 N";

            Assert.Throws<ArgumentException>(() => new RoverCommander().SendInstructions(instructions));
        }

        [Test]
        public void sends_only_plateau_size()
        {
            var instructions = @"5 5";

            Assert.Throws<ArgumentException>(() => new RoverCommander().SendInstructions(instructions));
        }

        [Test]
        public void parses_instructions()
        {
            var instructions = @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM";

            Assert.DoesNotThrow(() => new RoverCommander().SendInstructions(instructions));
        }

        [Test]
        public void fails_parsing_plateau_size()
        {
            var instructions = @"x x
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM";

            Assert.Throws<InvalidOperationException>(() => new RoverCommander().SendInstructions(instructions));
        }

        [Test]
        public void fails_parsing_robot_position()
        {
            var instructions = @"1 1
1 2 x
LMLMLMLMM
3 3 E
MMRMMRMRRM";

            Assert.Throws<InvalidOperationException>(() => new RoverCommander().SendInstructions(instructions));
        }

        [TestCase("")]
        [TestCase(null)]
        public void fails_when_no_instructions_sent(string instructions)
        {
            Assert.Throws<ArgumentNullException>(() => new RoverCommander().SendInstructions(instructions));
        }
    }
}
