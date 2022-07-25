using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestClientConstructor {
    [TestClass]
    public class TestClientConstructorValidClassParameterType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public class Service { }

        public class Client {
            public Service service;

            public Client(Service service) {
                Console.WriteLine("Client initialized!");
                this.service = service;
            }
        }

        [TestMethod]
        public void TestClientConstructor_ValidClassParameterType() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
