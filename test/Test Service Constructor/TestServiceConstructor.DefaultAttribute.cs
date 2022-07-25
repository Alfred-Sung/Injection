using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestServiceConstructor {
    [TestClass]
    public class TestServiceConstructorDefaultAttribute {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService {
            [Default] public Service() => Console.WriteLine("Service initialized!");
            public Service(int foo) => Assert.Fail();
            public Service(string bar, int foo) => Assert.Fail();
        }

        public class Client {
            [Inject] public IService service;
        }

        [TestMethod]
        public void TestServiceConstructor_DefaultAttribute() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
