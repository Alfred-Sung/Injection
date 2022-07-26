using PlainDI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlainDI.UnitTest.TestCircularDependency {
    [TestClass]
    public class TestcircularDependencySelfDependency {

        public class Client {
            public Client(Client self) { }
        }

        [TestMethod]
        public void TestcircularDependency_SelfDependency() {
            Assert.ThrowsException<CircularDependencyException>(() => Injector.Get<Client>(), "Circular dependency not detected");
        }
    }
}
