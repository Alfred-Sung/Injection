using System;

namespace PlainDI.Exceptions {
    public class CircularDependencyException : Exception {
        public CircularDependencyException(string message) : base(message) { }
    }
}
