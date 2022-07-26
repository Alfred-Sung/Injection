using PlainDI.Attributes;
using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PlainDI.UnitTest.TestServiceProperties {
    [TestClass]
    public class TestServicePropertiesInvalidPrimitiveType {
        [Injectable(typeof(Service))] public interface IService { }

        public class Service : IService {
            [Inject] string A { get; set; }

            public Service() => Console.WriteLine("Service initialized!");
        }

        public class Client {
            [Inject] IService service;

            public Client() => Console.WriteLine("Client initialized!");

            public IService GetService() => service;
        }

        [TestMethod]
        public void TestServiceProperties_InvalidPrimitiveType() {
            Assert.ThrowsException<MissingMethodException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid Inject type exists");
        }
    }
}
