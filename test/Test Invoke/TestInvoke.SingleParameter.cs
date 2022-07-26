using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestInvoke {
    [TestClass]
    public class TestInvokeSingleParameter {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService { }

        public class Client {
            public IService service;

            public void Func() {}

            public void Func(IService service) {
                Console.WriteLine("Client function called!");
                this.service = service;
            }

            public IService GetService() => service;
        }

        [TestMethod]
        public void TestInvoke_SingleParameter() {
            Client client = new Client();
            Injector.Invoke((Action<IService>)client.Func);

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            Service service = (Service)client.service;
            Assert.IsNotNull(service, "Injected service cannot be null");
        }
    }
}
