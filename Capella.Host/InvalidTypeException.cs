using System;

namespace Capella.Host.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException()
        {
        }

        public InvalidTypeException(string message)
            : base(message)
        {
        }
    }
}
