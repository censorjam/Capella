using System.Collections.Generic;

namespace Capella.Tests
{
    public interface ITest
    {
        string ReturnsAString(string p1, int p2);
        List<string> ReturnsGeneric();
        List<Dictionary<string, int>> ComplexGeneric();
        void VoidReturnType();
    }
}
