using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestClientConstructor {
    [TestClass]
    public class TestClientConstructorInvalidPrimitiveParameterType {
        /*
         * Client inject-> IA
         * 
         * A
         */

        public interface IService { }

        public class Client {
            public Client(string service) { }
        }

        [TestMethod]
        public void TestClientConstructor_InvalidPrimitiveParameterType() {
            Assert.ThrowsException<MissingMethodException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
