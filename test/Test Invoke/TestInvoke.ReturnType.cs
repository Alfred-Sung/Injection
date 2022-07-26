using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestInvoke {
    [TestClass]
    public class TestInvokeReturnType {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            public IService service;

            public void Func() {}

            public bool Func(IService service) {
                Console.WriteLine("Client function called!");
                this.service = service;
                return true;
            }

            public IService GetService() => service;
        }

        [TestMethod]
        public void TestInvoke_ReturnType() {
            Client client = new Client();
            var result = Injector.Invoke((Func<IService, bool>)client.Func);

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Assert.AreEqual(result, true, "Inject invoke does not return correct object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
