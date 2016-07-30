using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Capella.Core
{
    public class TypeCreation
    {
        //todo add support for out parameters
        public Type Create(string name, ParameterInfo[] pis)
        {
            AssemblyName assemblyName = new AssemblyName("Capella.Runtime");
            AssemblyBuilder assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder module = assembly.DefineDynamicModule("RuntimeTypes");
            TypeBuilder type = module.DefineType("Capella.Runtime.Types." + name);

            foreach (var parameterInfo in pis)
            {
                type.DefineField(parameterInfo.Name, parameterInfo.ParameterType, FieldAttributes.Public);
            }
            return type.CreateType();
        }
    }
}
