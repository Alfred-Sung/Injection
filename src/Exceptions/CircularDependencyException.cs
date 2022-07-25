using System;

namespace Injection.Exceptions {
    public class CircularDependencyException : Exception {
        public CircularDependencyException(string message) : base(message) { }
    }
}
