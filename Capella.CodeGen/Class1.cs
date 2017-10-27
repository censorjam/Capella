using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Capella.CodeGen
{
    public class ClientGenerator
    {
        public string GetCSharpTypeName(Type t)
        {
            return t.ToString()
                .Replace("System.Void", "void")
                .Replace("]", ">")
                .Replace("`1[", "<")
                .Replace("`1[", "<")
                .Replace("`2[", "<")
                .Replace("`3[", "<")
                .Replace("`4[", "<")
                .Replace("`5[", "<")
                .Replace("`6[", "<")
                .Replace("`7[", "<")
                .Replace("`8[", "<");
        }

        public IEnumerable<string> GetUsings(Type t)
        {
            HashSet<string> namespaces = new HashSet<string>();

            foreach (var mb in t.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (mb.MemberType == MemberTypes.Method && ((MethodBase)mb).GetMethodBody() != null)
                {
                    foreach (var p in ((MethodInfo)mb).GetMethodBody().LocalVariables)
                    {
                        if (!namespaces.Contains(p.LocalType.Namespace))
                        {
                            namespaces.Add(p.LocalType.Namespace);
                            Console.WriteLine(p.LocalType.Namespace);
                        }
                    }
                }

                if (mb.MemberType == MemberTypes.Method)
                {
                    var methodInfo = (MethodInfo)mb;
                    namespaces.Add(methodInfo.ReturnParameter.ParameterType.Namespace);
                    foreach (var item in methodInfo.GetParameters())
                    {
                        namespaces.Add(item.ParameterType.Namespace);
                    }
                }

                else if (mb.MemberType == MemberTypes.Field)
                {
                    string ns = ((FieldInfo)mb).FieldType.Namespace;
                    if (!namespaces.Contains(ns))
                    {
                        namespaces.Add(ns);
                        Console.WriteLine(ns);
                    }
                }
            }

            return namespaces;
        }

        public string GenerateUsings(Type t)
        {
            //return string.Join("", GetUsings(t).Select(u => $"using {u};" + Environment.NewLine));
            return $"using {t.Namespace};" + Environment.NewLine;
        }

        public string GenerateMethod(MethodInfo mi, bool async)
        {
            var sb = new StringBuilder();
            var methodParameters = mi.GetParameters().Length > 0 ? string.Join(", ", mi.GetParameters().Select(p => $"{GetCSharpTypeName(p.ParameterType)} {p.Name}")) : "";
            var parameters = string.Join(", ", mi.GetParameters().Select(p => p.Name));

            sb.AppendLine($"\t\tpublic {(async ? "async " : "")}{GetCSharpTypeName(mi.ReturnType)} {mi.Name}{(async ? "Async" : "")}({methodParameters})");
            sb.AppendLine("\t\t{");
            sb.AppendLine($"\t\t\treturn {(async ? "Task< " : "")}({GetCSharpTypeName(mi.ReturnType)}{(async ? ">" : "")})this.Execute{(async ? "Async" : "")}(\"{mi.Name}\", new object[] {{{parameters}}});");
            sb.AppendLine("\t\t}");
            return sb.ToString();
        }

        public string GenerateClient<T>(string baseClass = null)
        {
            var t = typeof(T);
            var sb = new StringBuilder();
            sb.AppendLine(GenerateUsings(t));
            sb.AppendLine("namespace test");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic class Client : ClientBase, " + t.Name);
            sb.AppendLine("\t{");

            foreach (var m in t.GetMethods())
            {
                sb.Append(GenerateMethod(m, false));
                sb.AppendLine();
                //sb.Append(GenerateMethod(m, true));
                //sb.AppendLine();
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
