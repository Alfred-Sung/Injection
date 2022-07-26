using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestInvoke {
    [TestClass]
    public class TestInvokeServiceInvoke {
        [Injectable(typeof(Service))] public interface IService {
            public IService Func();
        }

        public class Service : IService {
            public IService Func() {
                Console.WriteLine("Service function called!");
                return this;
            }
        }

        public class Client {
            public IService result;

            public Client(IService service) {
                Console.WriteLine("Client initialized!");
                this.result = (IService)Injector.Invoke((Func<IService>)service.Func);
            }
        }

        [TestMethod]
        public void TestInvoke_ServiceInvoke() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.result;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
