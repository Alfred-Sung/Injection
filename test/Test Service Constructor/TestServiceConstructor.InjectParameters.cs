using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestServiceConstructor {
    [TestClass]
    public class TestServiceConstructorInjectParameters {
        [Injectable(typeof(A))] public interface IA { }
        public class A : IA { }

        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService {
            public IA a;

            public Service(A a) {
                Console.WriteLine("Service initialized!");
                this.a = a;
            }
        }

        public class Client {
            [Inject] public IService service;
        }

        [TestMethod]
        public void TestServiceConstructor_InjectParameters() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");

            A a = (A)service.a;
            Assert.IsNotNull(a, "Injected service cannot be null");
        }
    }
}
