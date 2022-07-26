using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestClientFields {
    [TestClass]
    public class TestClientFieldsInjectPrivate {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            [Inject] private IService service;

            public Client() => Console.WriteLine("Client initialized!");

            public IService GetService() => service;
        }

        [TestMethod]
        public void TestClientFields_InjectPrivate() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.GetService();
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
