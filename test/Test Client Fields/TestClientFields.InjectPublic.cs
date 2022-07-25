using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestClientFields {
    [TestClass]
    public class TestClientFieldsInjectPublic {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            [Inject] public IService service;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestClientFields_InjectPublic() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
