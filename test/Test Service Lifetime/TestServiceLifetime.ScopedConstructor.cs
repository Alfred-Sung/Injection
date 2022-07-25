using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestServiceLifetime {
    [TestClass]
    public class TestServiceLifetimeScopedConstructor {
        [Injectable(typeof(Service), Lifetime.Scoped)] public interface IService { }

        public class Service : IService {
            public Service() { }
        }

        public class Client {
            public IService service1;
            public IService service2;

            public Client(IService service1, IService service2) {
                Console.WriteLine("Client initialized!");
                this.service1 = service1;
                this.service2 = service2;
            }
        }

        [TestMethod]
        public void TestServiceLifetime_ScopedConstructor() {
            Client client1 = Injector.Get<Client>();
            Client client2 = Injector.Get<Client>();

            Assert.IsNotNull(client1, "Injected client cannot be null");
            Assert.IsInstanceOfType(client1, typeof(Client), "Incorrect instance of client object");

            Assert.IsNotNull(client2, "Injected client cannot be null");
            Assert.IsInstanceOfType(client2, typeof(Client), "Incorrect instance of client object");

            Assert.AreNotSame(client1, client2, "Injected clients cannot be the same instance");


            Service client1_service1 = (Service)client1.service1;
            Assert.IsNotNull(client1_service1, "Injected service cannot be null");

            Service client1_service2 = (Service)client1.service2;
            Assert.IsNotNull(client1_service2, "Injected service cannot be null");

            Assert.AreSame(client1_service1, client1_service2, "Injected scoped service are not the same");


            Service client2_service1 = (Service)client2.service1;
            Assert.IsNotNull(client2_service1, "Injected service cannot be null");

            Service client2_service2 = (Service)client2.service2;
            Assert.IsNotNull(client2_service2, "Injected service cannot be null");

            Assert.AreSame(client2_service1, client2_service2, "Injected scoped service are not the same");

            Assert.AreNotSame(client1_service1, client2_service1, "Injected scoped services from different scopes cannot be the same");
            Assert.AreNotSame(client1_service1, client2_service2, "Injected scoped services from different scopes cannot be the same");

            Assert.AreNotSame(client1_service2, client2_service1, "Injected scoped services from different scopes cannot be the same");
            Assert.AreNotSame(client1_service2, client2_service2, "Injected scoped services from different scopes cannot be the same");
        }
    }
}
