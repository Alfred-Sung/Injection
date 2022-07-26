namespace PlainDI.Exceptions {
    public class NoImplementationException : System.Exception {
        public NoImplementationException() { }

        public NoImplementationException(string message) : base(message) { }
    }
}
