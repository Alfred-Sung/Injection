using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Injection.Reflection {
    internal static partial class AttributeReflection {
        static readonly HashSet<string> ignoredAssemblies = new HashSet<string>() {
            "UnityEditor",
            "UnityEngine",
            "System",
            "mscorlib",
            "Microsoft",
        };

        private static IEnumerable<Type> FilteredAssemblies() {
            return AppDomain.CurrentDomain.GetAssemblies()
                .AsParallel()
                .Where(assembly => {
                    var assemblyName = assembly.GetName().Name;
                    int index = assemblyName.IndexOf('.');
                    if (index != -1) assemblyName = assemblyName.Substring(0, index);
                    return !ignoredAssemblies.Contains(assemblyName);
                })
                .SelectMany(assembly => assembly.GetTypes());
        }

        public static IEnumerable<Type> GetTypesWithAttribute<T>() => GetTypesWithAttribute(typeof(T));

        public static IEnumerable<Type> GetTypesWithAttribute(Type attribute, Type[] inherits = null) {
            return FilteredAssemblies().AsParallel()
                .Where(type => Attribute.IsDefined(type, attribute))
                .Where(type => inherits.All(inherit => type.IsAssignableFrom(inherit)));
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(BindingFlags bindingAttr = BindingFlags.Default) => GetPropertiesWithAttribute(typeof(T), bindingAttr);

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute(Type attribute, BindingFlags bindingAttr = BindingFlags.Default) {
            return FilteredAssemblies().AsParallel()
                .SelectMany(type => type.GetProperties(bindingAttr))
                .Where(type => Attribute.IsDefined(type, attribute));
        }

        public static IEnumerable<FieldInfo> GetFieldsWithAttribute<T>(BindingFlags bindingAttr = BindingFlags.Default) => GetFieldsWithAttribute(typeof(T), bindingAttr);

        public static IEnumerable<FieldInfo> GetFieldsWithAttribute(Type attribute, BindingFlags bindingAttr = BindingFlags.Default) {
            return FilteredAssemblies().AsParallel()
                .SelectMany(type => type.GetFields(bindingAttr))
                .Where(type => Attribute.IsDefined(type, attribute));
        }

        public static IEnumerable<ConstructorInfo> GetConstructorsWithAttribute<T>(BindingFlags bindingAttr = BindingFlags.Default) => GetConstructorsWithAttribute(typeof(T), bindingAttr);

        public static IEnumerable<ConstructorInfo> GetConstructorsWithAttribute(Type attribute, BindingFlags bindingAttr = BindingFlags.Default) {
            return FilteredAssemblies().AsParallel()
                .SelectMany(type => type.GetConstructors(bindingAttr))
                .Where(constructor => Attribute.IsDefined(constructor, attribute));
        }

        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(BindingFlags bindingAttr = BindingFlags.Default) => GetMethodsWithAttribute(typeof(T), bindingAttr);

        public static IEnumerable<MethodInfo> GetMethodsWithAttribute(Type attribute, BindingFlags bindingAttr = BindingFlags.Default) {
            return FilteredAssemblies().AsParallel()
                .SelectMany(type => type.GetMethods(bindingAttr))
                .Where(method => Attribute.IsDefined(method, attribute));
        }

        public static IEnumerable<MethodInfo> GetMethodsWithSignature(this IEnumerable<MethodInfo> list, Type returnType, params Type[] parameterTypes) {
            return list.AsParallel()
                .Where(method => method.ReturnType == returnType)
                .Where(method => {
                    var parameters = method
                        .GetParameters()
                        .Select(parameter => parameter.ParameterType);

                    return parameters.SequenceEqual(parameterTypes);
                });
        }
    }
}
