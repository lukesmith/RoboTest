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
            var instructions = @"0 0
----
----
----";
            new RoverCommander().SendInstructions(instructions);
        }

        [TestCase("")]
        [TestCase(null)]
        public void fails_when_no_instructions_sent(string instructions)
        {
            Assert.Throws<ArgumentNullException>(() => new RoverCommander().SendInstructions(instructions));
        }
    }
}
