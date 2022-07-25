using Injection.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Injection.UnitTest.TestClientConstructor {
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
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "Injection does not throw exception where invalid Inject type exists");
        }
    }
}
