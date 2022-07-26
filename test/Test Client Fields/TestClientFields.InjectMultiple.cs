using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestClientFields {
    [TestClass]
    public class TestClientFieldsInjectMultiple {
        [Injectable(typeof(A))] public interface IA { }

        public class A : IA { }

        [Injectable(typeof(B))] public interface IB { }

        public class B : IB { }

        [Injectable(typeof(C))] public interface IC { }

        public class C : IC { }

        public class Client {
            [Inject] public IA A;
            [Inject] public IB B;
            [Inject] public IC C;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestClientFields_InjectMultiple() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            A A = (A)client.A;
            Assert.IsNotNull(A, "Injected service cannot be null");

            B B = (B)client.B;
            Assert.IsNotNull(B, "Injected service cannot be null");

            C C = (C)client.C;
            Assert.IsNotNull(C, "Injected service cannot be null");
        }
    }
}
