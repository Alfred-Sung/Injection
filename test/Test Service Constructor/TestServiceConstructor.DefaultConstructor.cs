using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestServiceConstructor {
    [TestClass]
    public class TestServiceConstructorDefaultConstructor {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService {
            public Service() => Console.WriteLine("Service initialized!");
            public Service(int foo) => Assert.Fail();
            public Service(string bar, int foo) => Assert.Fail();
        }

        public class Client {
            [Inject] public IService service;
        }

        [TestMethod]
        public void TestServiceConstructor_DefaultConstructor() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
