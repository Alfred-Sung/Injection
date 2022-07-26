using PlainDI.Attributes;
using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestAttributeUsage {
    [TestClass]
    public class TestAttributeUsageInvalidInjectableClass {
        [Injectable(typeof(Service))] public class IService { }
        public class Service : IService { }

        public class Client {
            [Inject] public IService service;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestAttributeUsage_InvalidInjectableClass() {
            Assert.ThrowsException<AttributeException>(() => Injector.Get<Client>(), "Injectable does not throw exception where invalid class type exists");
        }
    }
}
