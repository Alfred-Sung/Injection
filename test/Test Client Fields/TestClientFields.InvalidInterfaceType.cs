using PlainDI.Attributes;
using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlainDI.UnitTest.TestClientFields {
    [TestClass]
    public class TestClientFieldsInvalidInterfaceType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public interface IService { }

        public class Client {
            [Inject] IService service;
        }

        [TestMethod]
        public void TestClientFields_InvalidInterfaceType() {
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
