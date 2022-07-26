using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestServiceFields {
    [TestClass]
    public class TestServiceFieldsInjectStatic {
        [Injectable(typeof(A))] public interface IA { }

        public class A : IA { }

        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService {
            [Inject] static IA A;

            public Service() => Console.WriteLine("Service initialized!");

            public IA GetA() => A;
        }

        public class Client {
            [Inject] IService service;

            public Client() => Console.WriteLine("Client initialized!");

            public IService GetService() => service;
        }

        [TestMethod]
        public void TestServiceFields_InjectStatic() {
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
