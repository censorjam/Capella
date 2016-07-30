using System;

namespace Capella.Host.Exceptions
{
    public class InvalidRouteException : Exception
    {
        public InvalidRouteException()
        {
        }

        public InvalidRouteException(string message)
            : base(message)
        {
        }
    }
}