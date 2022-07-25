using Injection.Attributes;
using Injection.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Injection.UnitTest.TestCircularDependency {
    [TestClass]
    public class TestcircularDependencyLoopToMiddle {
        [Injectable(typeof(A))] public interface IA { }
        public class A : IA {
            [Inject] public IB B;
        }

        [Injectable(typeof(B))] public interface IB { }
        public class B : IB {
            [Inject] public IC C;
        }

        [Injectable(typeof(C))] public interface IC { }
        public class C : IC {
            [Inject] public IB B;
        }

        public class Client {
            [Inject] public IA A;

            public Client() => Console.WriteLine("Client initialized!");
        }

        [TestMethod]
        public void TestcircularDependency_LoopToMiddle() {
            Assert.ThrowsException<CircularDependencyException>(() => Injector.Get<Client>(), "Circular dependency not detected");
        }
    }
}
