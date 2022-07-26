using PlainDI.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace PlainDI {
    /**
     * Creates and stores instances of services
     */
    internal static class Factory {
        //(Lifetime, Constructor, Constructor Parameters, Fields, Properties)
        private static Dictionary<Type, (Lifetime, ConstructorInfo, Type[], FieldInfo[], PropertyInfo[])> objProfiles = new Dictionary<Type, (Lifetime, ConstructorInfo, Type[], FieldInfo[], PropertyInfo[])>();
        private static Dictionary<Type, object> singletons = new Dictionary<Type, object>();

        internal static T Get<T>(HashSet<Type> dependencies) => (T)Get(typeof(T), dependencies);

        internal static object Get([NotNull] Type type, HashSet<Type> dependencies) {
            if (singletons.ContainsKey(type))
                return singletons[type];

            // Get profile of current type
            if (!objProfiles.ContainsKey(type))
                objProfiles.Add(type, Profiler.GetProfile(type));
            var profile = objProfiles[type];

            // Track Lifetime.Scoped objects of current type
            var scoped = new Dictionary<Type, object>();
            var parameterObjs = profile.Item3
                .Select(paramType => {
                    // Transform constructor parameter types to their implemented type from linker mapping
                    // Types here can either be interface, abstract, or class
                    var implementedType = Linker.GetImplementationOf(paramType);

                    if (!objProfiles.ContainsKey(implementedType))
                        objProfiles.Add(implementedType, Profiler.GetProfile(paramType));

                    // Should be all valid classes here
                    return implementedType;
                })
                .Select(implementedType => {
                    // Create objects based on Injectable.Lifetime value; default is transient
                    switch (objProfiles[implementedType].Item1) {
                        case Lifetime.Scoped:
                            if (!scoped.ContainsKey(implementedType))
                                scoped.Add(implementedType, Linker.Get(implementedType, new HashSet<Type>(dependencies)));
                            return scoped[implementedType];
                        case Lifetime.Transient:
                        default:
                            return Linker.Get(implementedType, new HashSet<Type>(dependencies));
                        case Lifetime.Singleton:
                            return GetSingleton(implementedType, dependencies);
                    }
                })
                .ToArray();

            var obj = profile.Item2.Invoke(parameterObjs);
            // Field Property injection is separated due to PlainDI.InjectExisting() functionality
            InjectFieldProperty(scoped, type, obj, dependencies);

            return obj;
        }

        internal static void InjectFieldProperty(Dictionary<Type, object> scoped, Type type, object obj, HashSet<Type> dependencies) {
            // Get profile of current type
            if (!objProfiles.ContainsKey(type))
                objProfiles.Add(type, Profiler.GetProfile(type));
            var profile = objProfiles[type];

            foreach (FieldInfo field in profile.Item4) {
                var fieldType = field.FieldType;

                // Transform constructor parameter types to their implemented type from linker mapping
                // Types here can either be interface, abstract, or class
                var implementedType = Linker.GetImplementationOf(fieldType);

                if (!objProfiles.ContainsKey(implementedType))
                    objProfiles.Add(implementedType, Profiler.GetProfile(fieldType));

                // Should be all valid classes here

                // Create objects based on Injectable.Lifetime value; default is transient
                switch (objProfiles[implementedType].Item1) {
                    case Lifetime.Scoped:
                        if (!scoped.ContainsKey(implementedType))
                            scoped.Add(implementedType, Linker.Get(implementedType, new HashSet<Type>(dependencies)));
                        field.SetValue(obj, scoped[implementedType]);
                        break;
                    case Lifetime.Transient:
                    default:
                        field.SetValue(obj, Linker.Get(implementedType, new HashSet<Type>(dependencies)));
                        break;
                    case Lifetime.Singleton:
                        field.SetValue(obj, GetSingleton(implementedType, dependencies));
                        break;
                }
            }

            foreach (PropertyInfo property in profile.Item5) {
                var propertyType = property.PropertyType;

                // Transform constructor parameter types to their implemented type from linker mapping
                // Types here can either be interface, abstract, or class
                var implementedType = Linker.GetImplementationOf(propertyType);

                if (!objProfiles.ContainsKey(implementedType))
                    objProfiles.Add(implementedType, Profiler.GetProfile(propertyType));

                // Should be all valid classes here

                // Create objects based on Injectable.Lifetime value; default is transient
                switch (objProfiles[implementedType].Item1) {
                    case Lifetime.Scoped:
                        if (!scoped.ContainsKey(implementedType))
                            scoped.Add(implementedType, Linker.Get(implementedType, new HashSet<Type>(dependencies)));
                        property.SetValue(obj, scoped[implementedType]);
                        break;
                    case Lifetime.Transient:
                    default:
                        property.SetValue(obj, Linker.Get(implementedType, new HashSet<Type>(dependencies)));
                        break;
                    case Lifetime.Singleton:
                        property.SetValue(obj, GetSingleton(implementedType, dependencies));
                        break;
                }
            }
        }

        internal static object GetSingleton(Type type, HashSet<Type> dependencies) {
            // Get existing singleton, if not create
            if (!singletons.ContainsKey(type))
                singletons.Add(type, Linker.Get(type, new HashSet<Type>(dependencies)));
            return singletons[type];
        }
    }
}
