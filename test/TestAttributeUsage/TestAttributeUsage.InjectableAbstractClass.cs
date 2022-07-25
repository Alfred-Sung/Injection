using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestAttributeUsage {
    [TestClass]
    public class TestAttributeUsageInjectableAbstractClass {
        [Injectable(typeof(Service))] public abstract class IService { }
        public class Service : IService { }

        public class Client {
            [Inject] public IService service;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestAttributeUsage_InjectableAbstractClass() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
