using Injection.Attributes;
using Injection.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Injection.UnitTest.TestClientProperties {
    [TestClass]
    public class TestClientPropertiesInvalidInterfaceType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public interface IService { }

        public class Client {
            [Inject] IService service { get; set; }
        }

        [TestMethod]
        public void TestClientProperties_InvalidInterfaceType() {
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "Injection does not throw exception where invalid Inject type exists");
        }
    }
}
