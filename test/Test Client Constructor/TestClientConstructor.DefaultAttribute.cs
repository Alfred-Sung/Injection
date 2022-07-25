using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestClientConstructor {
    [TestClass]
    public class TestClientConstructorDefaultAttribute {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            [Inject] public IService service;

            [Default] public Client() => Console.WriteLine("Client initialized!");
            public Client(int foo) => Assert.Fail();
            public Client(string bar, int foo) => Assert.Fail();
        }

        [TestMethod]
        public void TestClientConstructor_DefaultAttribute() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
