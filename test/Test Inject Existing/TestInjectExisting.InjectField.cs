using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestInjectExisting {
    [TestClass]
    public class TestInjectExistingInjectField {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            [Inject] public IService service;

            public Client() => Console.WriteLine("Client initialized!");

            public IService GetService() => service;
        }

        [TestMethod]
        public void TestInjectExisting_InjectPrivate() {
            Client client = new Client();
            Injector.InjectExisting(client);

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.GetService();
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
