using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PlainDI.Exceptions;

namespace PlainDI {
    /**
     * Actual interface of the library; the rest of the classes are protected by internal keyword
     */
    public sealed class Injector {
        /// <summary>
        /// Create and return an object of type T with injected services. 
        /// </summary>
        /// <typeparam name="T">The type of the object to create.</typeparam>
        /// <returns>The instantiated object.</returns>
        /// <exception cref="AttributeException">Thrown if `[Injectable]` does not decorate either an interface or an abstract class.</exception>
        /// <exception cref="AttributeException">Thrown if there are multiple constructors with `[Default]` attributes.</exception>
        /// <exception cref="NoImplementationException">Thrown if `[Injectable]` targetInstance does not implement T.</exception>
        /// <exception cref="CircularDependencyException">Thrown if a dependency is found referencing an earlier service.</exception>
        /// <exception cref="MissingMethodException">Thrown if DI library cannot find a suitable constructor for T.</exception>
        public static T Get<T>() => (T)Get(typeof(T));

        /// <summary>
        /// Create and return an object of type with injected services. 
        /// </summary>
        /// <param name="type">The type of the object to create.</param>
        /// <returns>The instantiated object.</returns>
        /// <exception cref="AttributeException">Thrown if `[Injectable]` does not decorate either an interface or an abstract class.</exception>
        /// <exception cref="AttributeException">Thrown if there are multiple constructors with `[Default]` attributes.</exception>
        /// <exception cref="NoImplementationException">Thrown if `[Injectable]` targetInstance does not implement T.</exception>
        /// <exception cref="CircularDependencyException">Thrown if a dependency is found referencing an earlier service.</exception>
        /// <exception cref="MissingMethodException">Thrown if DI library cannot find a suitable constructor for T.</exception>
        public static object Get([NotNull] Type type) => Linker.Get(type, new HashSet<Type>());

        /// <summary>
        /// Inject services into an existing object. Only injects to its fields and properties.
        /// </summary>
        /// <param name="obj">The object to inject fields and properties.</param>
        /// <exception cref="AttributeException">Thrown if `[Injectable]` does not decorate either an interface or an abstract class.</exception>
        /// <exception cref="AttributeException">Thrown if there are multiple constructors with `[Default]` attributes.</exception>
        /// <exception cref="NoImplementationException">Thrown if `[Injectable]` targetInstance does not implement T.</exception>
        /// <exception cref="CircularDependencyException">Thrown if a dependency is found referencing an earlier service.</exception>
        /// <exception cref="MissingMethodException">Thrown if DI library cannot find a suitable constructor for T.</exception>
        public static void InjectExisting([NotNull] object obj) => Factory.InjectFieldProperty(new Dictionary<Type, object>(), obj.GetType(), obj, new HashSet<Type>() { obj.GetType() });
    }
}
