using PlainDI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PlainDI {
    internal class Invoker {
        private static Dictionary<MethodInfo, Type[]> funcProfiles = new Dictionary<MethodInfo, Type[]>();

        internal static object? Invoke(Delegate function, HashSet<Type> dependencies) {
            var info = function.Method;

            if (!funcProfiles.ContainsKey(info))
                funcProfiles.Add(info, info.GetParameters().Select(param => param.ParameterType).ToArray());

            var parameters = funcProfiles[info];

            var scoped = new Dictionary<Type, object>();
            var parameterObjs = parameters
                .Select(paramType => {
                    // Transform constructor parameter types to their implemented type from linker mapping
                    // Types here can either be interface, abstract, or class
                    var implementedType = Linker.GetImplementationOf(paramType);

                    Profiler.AddProfile(implementedType, paramType);

                    // Should be all valid classes here
                    return implementedType;
                })
                .Select(implementedType => {
                    // Create objects based on Injectable.Lifetime value; default is transient
                    switch (Profiler.GetProfile(implementedType).Item1) {
                        case Lifetime.Scoped:
                            if (!scoped.ContainsKey(implementedType))
                                scoped.Add(implementedType, Linker.Get(implementedType, new HashSet<Type>(dependencies)));
                            return scoped[implementedType];
                        case Lifetime.Transient:
                        default:
                            return Linker.Get(implementedType, new HashSet<Type>(dependencies));
                        case Lifetime.Singleton:
                            return Factory.GetSingleton(implementedType, dependencies);
                    }
                })
                .ToArray();

            return function.DynamicInvoke(parameterObjs);
        }
    }
}
