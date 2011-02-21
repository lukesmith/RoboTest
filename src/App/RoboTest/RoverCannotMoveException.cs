using System;
using System.Runtime.Serialization;

namespace RoboTest
{
    [Serializable]
    public class RoverCannotMoveException : Exception
    {
        public RoverCannotMoveException()
        {
        }

        public RoverCannotMoveException(string message)
            : base(message)
        {
        }

        public RoverCannotMoveException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RoverCannotMoveException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}