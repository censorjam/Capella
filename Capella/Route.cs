namespace Capella.Core
{
    public class Route
    {
        public string Method { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return Method + ":" + Path;
        }
    }
}