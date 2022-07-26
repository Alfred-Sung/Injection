using PlainDI.Attributes;
using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestAttributeUsage {
    [TestClass]
    public class TestAttributeUsageInvalidInjectType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public interface IA { }

        public class Client {
            [Inject] public IA A;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestAttributeUsage_InvalidInjectType() {
            Assert.ThrowsException<NoImplementationException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
