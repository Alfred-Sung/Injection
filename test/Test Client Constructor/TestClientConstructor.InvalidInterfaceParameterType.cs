using Injection.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Injection.UnitTest.TestClientConstructor {
    [TestClass]
    public class TestClientConstructorInvalidInterfaceParameterType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public interface IService { }

        public class Client {
            public Client(IService service) { }
        }

        [TestMethod]
        public void TestClientConstructor_InvalidInterfaceParameterType() {
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "Injection does not throw exception where invalid Inject type exists");
        }
    }
}
