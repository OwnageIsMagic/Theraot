#if NETFRAMEWORK && !NET45_OR_GREATER

using System.Collections.Generic;
using System.Reflection.Emit;

namespace System.Reflection;

// TODO: TypeInfo
public static class TypeInfoTheraotExtensions
{
    public static Type AsType(this Type t) => t;

    public static MethodInfo? GetDeclaredMethod(this Type t, string name) => t.GetMethod(name, BindingFlags.DeclaredOnly);
    public static PropertyInfo? GetDeclaredProperty(this Type t, string name) => t.GetProperty(name, BindingFlags.DeclaredOnly);
    public static IEnumerable<ConstructorInfo> DeclaredConstructors(this Type t) => t.GetConstructors(BindingFlags.DeclaredOnly);
    public static IEnumerable<FieldInfo> DeclaredFields(this Type t) => t.GetFields(BindingFlags.DeclaredOnly);

    public static Type CreateTypeInfo(this TypeBuilder b) => b.CreateType();
}
#endif
