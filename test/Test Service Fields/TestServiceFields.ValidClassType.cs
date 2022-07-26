using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestServiceFields {
    [TestClass]
    public class TestServiceFieldsValidClassType {
        public class A { }

        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService {
            [Inject] public A A;

            public Service() => Console.WriteLine("Service initialized!");
        }

        public class Client {
            [Inject] public Service service;
        }

        [TestMethod]
        public void TestServiceFields_ValidClassType() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");

            A A = service.A;
            Assert.IsNotNull(A, "Injected service cannot be null");
        }
    }
}
