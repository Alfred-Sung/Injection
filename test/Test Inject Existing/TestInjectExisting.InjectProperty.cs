using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestInjectExisting {
    [TestClass]
    public class TestInjectExistingInjectProperty {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            [Inject] public IService Service { get; set; }

            public Client() => Console.WriteLine("Client initialized!");

            public IService GetService() => Service;
        }

        [TestMethod]
        public void TestInjectExisting_InjectProperty() {
            Client client = new Client();
            Injector.InjectExisting(client);

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.GetService();
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
