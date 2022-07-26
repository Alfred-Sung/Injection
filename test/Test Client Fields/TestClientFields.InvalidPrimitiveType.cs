using PlainDI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestClientFields {
    [TestClass]
    public class TestClientFieldsInvalidPrimitiveType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public interface IService { }

        public class Client {
            [Inject] string service;
        }

        [TestMethod]
        public void TestClientFields_InvalidPrimitiveType() {
            Assert.ThrowsException<MissingMethodException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
