using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestClientProperties {
    [TestClass]
    public class TestClientPropertiesInjectPublic {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            [Inject] public IService Service { get; set; }

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestClientProperties_InjectPublic() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.Service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
