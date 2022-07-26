using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestServiceInheritence {
    [TestClass]
    public class TestServiceInheritenceSameImplementation {
        /*
         * Client inject-> IA <-inherit IB 
         *                  |           |
         *               B <------------ 
         */

        [Injectable(typeof(B))] public interface IA { }

        [Injectable(typeof(B))] public interface IB : IA { }
        public class B : IB { }

        public class Client {
            [Inject] public IA A;
            [Inject] public IB B;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestServiceInheritence_SameImplementation() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            IA A = client.A;
            Assert.IsNotNull(A, "Injected service cannot be null");
            Assert.IsInstanceOfType(A, typeof(B));

            IB B = client.B;
            Assert.IsNotNull(B, "Injected service cannot be null");
            Assert.IsInstanceOfType(B, typeof(B));
        }
    }
}
