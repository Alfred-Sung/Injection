using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestServiceProperties {
    [TestClass]
    public class TestServicePropertiesInjectProtected {
        [Injectable(typeof(A))] public interface IA { }

        public class A : IA { }

        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService {
            [Inject] protected IA A { get; set; }

            public Service() => Console.WriteLine("Service initialized!");

            public IA GetA() => A;
        }

        public class Client {
            [Inject] IService service;

            public Client() => Console.WriteLine("Client initialized!");

            public IService GetService() => service;
        }

        [TestMethod]
        public void TestServiceProperties_InjectProtected() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.GetService();
            Assert.IsNotNull(service, "Injected service cannot be null");

            A A = (A)service.GetA();
            Assert.IsNotNull(A, "Injected service cannot be null");
        }
    }
}
