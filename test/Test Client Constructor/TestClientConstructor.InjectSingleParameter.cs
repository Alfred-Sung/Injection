using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestClientConstructor {
    [TestClass]
    public class TestClientConstructorInjectSingleParameter {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            public IService service;

            public Client(IService service) {
                Console.WriteLine("Client initialized!");
                this.service = service;
            }
        }

        [TestMethod]
        public void TestClientConstructor_InjectSingleParameter() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
