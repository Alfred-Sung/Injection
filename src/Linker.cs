using PlainDI.Attributes;
using PlainDI.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PlainDI {
    /**
     * Maps requested types to their implementation
     */
    internal static class Linker {
        // <Injectable Interface/Class, Implementation>
        private static Dictionary<Type, Type> serviceMapping = new Dictionary<Type, Type>();

        public static T Get<T>(HashSet<Type> dependencies) => (T)Get(typeof(T), dependencies);
        public static object Get([NotNull] Type type, HashSet<Type> dependencies) {
            // Track injected types so far and add current type to list
            if (dependencies.Contains(type))
                throw new CircularDependencyException("Circular dependency found");
            dependencies.Add(type);

            // Check if type has an implementation and get it
            return Factory.Get(GetImplementationOf(type), dependencies);
        }

        internal static bool HasImplementation<T>() => HasImplementation(typeof(T));
        internal static bool HasImplementation([NotNull] Type type) => serviceMapping.ContainsKey(type);

        internal static Type GetImplementationOf<T>() => GetImplementationOf(typeof(T));
        internal static Type GetImplementationOf([NotNull] Type type) {
            if (type.IsPrimitive)
                throw new NoImplementationException("Cannot get implementation of primitive " + type.Name);

            if (!HasImplementation(type))
                AddImplementation(type);

            return serviceMapping[type];
        }

        internal static void AddImplementation(Type type) {
            if (HasImplementation(type))
                return;

            // Throw error because type is an interface with no implementation
            if (type.IsInterface || type.IsAbstract) {
                // Get injectable attribute from type
                var attribute = type.GetCustomAttribute<InjectableAttribute>();

                if (attribute == null)
                    throw new NoImplementationException("Interface is not decorated with [Injectable] attribute " + type.Name);

                if (!type.IsAssignableFrom(attribute.TargetInstance))
                    throw new NoImplementationException("Cannot get implementation of interface " + type.Name);

                // Add to serviceMapping
                serviceMapping.Add(type, attribute.TargetInstance);
            } else if (type.IsClass && !type.IsAbstract) {
                var attribute = type.GetCustomAttribute<InjectableAttribute>();

                if (attribute != null)
                    throw new AttributeException("[Injectable] attribute can only be applied to interfaces or abstract classes " + type.Name);

                serviceMapping.Add(type, type);
            } else {
                throw new AttributeException("[Injectable] attribute can only be applied to interfaces or abstract classes " + type.Name);
            }
        }
    }
}
