using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlainDI.UnitTest.TestClientConstructor {
    [TestClass]
    public class TestClientConstructorInvalidAbstractParameterType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public abstract class Service { }

        public class Client {
            public Client(Service service) { }
        }

        [TestMethod]
        public void TestClientConstructor_InvalidAbstractParameterType() {
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
