using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Injection.UnitTest.TestClientFields {
    [TestClass]
    public class TestClientFieldsValidClassType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public class Service { }

        public class Client {
            [Inject] public Service service;
        }

        [TestMethod]
        public void TestClientFields_ValidClassType() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
