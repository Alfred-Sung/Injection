using PlainDI.Attributes;
using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlainDI.UnitTest.TestAttributeUsage {
    [TestClass]
    public class TestAttributeUsageMultipleDefaultAttributes {
        [Injectable(typeof(Service))] public interface IService { }
        public class Service { }

        public class Client {
            [Default] public Client() { }
            [Default] public Client(IService service) { }
        }

        [TestMethod]
        public void TestAttributeUsage_MultipleDefaultAttributes() {
            Assert.ThrowsException<AttributeException>(() => Injector.Get<Client>(), "PlainDI does not throw exception where invalid multiple Default attributes exists");
        }
    }
}
