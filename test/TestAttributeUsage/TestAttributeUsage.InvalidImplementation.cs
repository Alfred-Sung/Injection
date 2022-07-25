using Injection.Attributes;
using Injection.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestAttributeUsage {
    [TestClass]
    public class TestAttributeUsageInvalidImplementation {
        /*
         * Client inject-> IA
         * 
         * A
         */

        [Injectable(typeof(A))] public interface IA { }
        public class A { }

        public class Client {
            [Inject] public IA A;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestAttributeUsage_InvalidImplementation() {
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "Injection does not throw exception where invalid Injectable TargetInstance exists");
        }
    }
}
