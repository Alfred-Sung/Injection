using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestDeepHierarchy {
    [TestClass]
    public class TestServiceInheritence {
        /*
         * Client inject-> IA inject-> IB inject-> IC
         *                |           |           |
         *               v           v           v
         *              A           B           C
         */

        [Injectable(typeof(A))] public interface IA { }
        public class A : IA {
            [Inject] public IB B;
        }

        [Injectable(typeof(B))] public interface IB { }
        public class B : IB {
            [Inject] public IC C;
        }

        [Injectable(typeof(C))] public interface IC { }
        public class C : IC { }

        public class Client {
            [Inject] public IA A;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestDeepHierarchy_InjectFields() {
            Client client = Injector.Get<Client>();

            Assert.IsNotNull(client, "Injected client cannot be null");
            Assert.IsInstanceOfType(client, typeof(Client), "Incorrect instance of client object");

            A A = (A)client.A;
            Assert.IsNotNull(A, "Injected service cannot be null");

            B B = (B)A.B;
            Assert.IsNotNull(B, "Injected service cannot be null");

            C C = (C)B.C;
            Assert.IsNotNull(C, "Injected service cannot be null");
        }
    }
}
