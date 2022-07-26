using PlainDI.Attributes;
using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlainDI.UnitTest.TestClientProperties {
    [TestClass]
    public class TestClientPropertiesInvalidAbstractType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public abstract class Service { }

        public class Client {
            [Inject] Service service { get; set; }
        }

        [TestMethod]
        public void TestClientProperties_InvalidAbstractType() {
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
