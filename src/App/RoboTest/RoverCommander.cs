using System;

namespace RoboTest
{
    public class RoverCommander
    {
        public void SendInstructions(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
            {
                throw new ArgumentNullException("instructions");
            }
        }
    }
}