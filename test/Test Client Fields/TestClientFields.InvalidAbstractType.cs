using PlainDI.Attributes;
using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlainDI.UnitTest.TestClientFields {
    [TestClass]
    public class TestClientFieldsInvalidAbstractType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public abstract class Service { }

        public class Client {
            [Inject] Service service;
        }

        [TestMethod]
        public void TestClientFields_InvalidAbstractType() {
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
