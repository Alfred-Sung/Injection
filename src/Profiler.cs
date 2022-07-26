using PlainDI.Attributes;
using PlainDI.Exceptions;
using PlainDI.Reflection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace PlainDI {
    /**
     * Scans a type and gets relevant information; constructor, constructor parameters, etc.
     */
    internal static class Profiler {
        internal static (Lifetime, ConstructorInfo, Type[], FieldInfo[], PropertyInfo[]) GetProfile<T>() => GetProfile(typeof(T));
        internal static (Lifetime, ConstructorInfo, Type[], FieldInfo[], PropertyInfo[]) GetProfile([NotNull] Type type) {
            var attribute = type.GetCustomAttribute<InjectableAttribute>();

            var implementedType = Linker.GetImplementationOf(type);

            var constructorParameters = GetConstructorParameters(implementedType).Select(param => param.ParameterType).ToArray();
            var typeConstructor = GetConstructor(implementedType);
            var typeFields = GetInjectFields(implementedType).ToArray();
            var typeProperties = GetInjectProperties(implementedType).ToArray();

            return (
                attribute != null ? attribute.Lifetime : Lifetime.Transient,
                typeConstructor,
                constructorParameters,
                typeFields,
                typeProperties
            );
        }

        internal static ConstructorInfo GetConstructor<T>() => GetConstructor(typeof(T));
        internal static ConstructorInfo GetConstructor([NotNull] Type type) {
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var defaultConstructors = constructors.Where(c => Attribute.IsDefined(c, typeof(DefaultAttribute)));

            if (defaultConstructors.Count() > 1)
                throw new AttributeException("Only one constructor must be decorated with [Default]");

            if (constructors.Length == 1)
                return constructors[0];
            else if (defaultConstructors.Count() > 0)
                return defaultConstructors.First();
            else {
                var emptyConstructor = type.GetConstructor(Type.EmptyTypes);
                if (emptyConstructor == null)
                    throw new MissingMethodException("Constructor missing from " + type);

                return emptyConstructor;
            }
        }

        internal static ParameterInfo[] GetConstructorParameters<T>() => GetConstructorParameters(typeof(T));
        internal static ParameterInfo[] GetConstructorParameters([NotNull] Type type) {
            var constructor = GetConstructor(type);
            return constructor.GetParameters().ToArray();
        }

        internal static FieldInfo[] GetInjectFields<T>() => GetInjectFields(typeof(T));
        internal static FieldInfo[] GetInjectFields([NotNull] Type type) {
            return AttributeReflection.GetTypeFieldsWithAttribute(type, typeof(InjectAttribute), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .ToArray();
        }

        internal static PropertyInfo[] GetInjectProperties<T>() => GetInjectProperties(typeof(T));
        internal static PropertyInfo[] GetInjectProperties([NotNull] Type type) {
            return AttributeReflection.GetTypePropertiesWithAttribute(type, typeof(InjectAttribute), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .ToArray();
        }
    }
}
