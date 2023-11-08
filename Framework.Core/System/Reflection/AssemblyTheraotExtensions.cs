#if NETFRAMEWORK && !NET45_OR_GREATER
using System.Collections.Generic;

namespace System.Reflection;

public static class AssemblyTheraotExtensions
{
    // TODO: TypeInfo
    public static IEnumerable<Type> DefinedTypes(this Assembly @this) => @this.GetTypes();
}
#endif
