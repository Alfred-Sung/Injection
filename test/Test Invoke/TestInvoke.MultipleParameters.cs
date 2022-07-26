using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestInvoke {
    [TestClass]
    public class TestInvokeMultipleParameters {
        [Injectable(typeof(A))] public interface IA { }

        public class A : IA { }

        [Injectable(typeof(B))] public interface IB { }

        public class B : IB { }

        [Injectable(typeof(C))] public interface IC { }

        public class C : IC { }

        public class Client {
            public IA A;
            public IB B;
            public IC C;

            public void Func() {}

            public void Func(IA A, IB B, IC C) {
                Console.WriteLine("Client function called!");
                this.A = A;
                this.B = B;
                this.C = C;
            }
        }

        [TestMethod]
        public void TestInvoke_MultipleParameters() {
            Client client = new Client();
            Injector.Invoke((Action<IA, IB, IC>)client.Func);

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
