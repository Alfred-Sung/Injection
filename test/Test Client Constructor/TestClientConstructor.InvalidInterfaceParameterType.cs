using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlainDI.UnitTest.TestClientConstructor {
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
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
