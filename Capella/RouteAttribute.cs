using System;

namespace Capella.Core
{
    public class RouteAttribute : Attribute
    {
        public string Method { get; set; }
        public string Route { get; private set; }

        public RouteAttribute(string route)
        {
            Method = "POST";
            Route = route;
        }
    }
}
