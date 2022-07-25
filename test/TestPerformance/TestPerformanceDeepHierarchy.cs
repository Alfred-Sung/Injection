using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Injection.UnitTest.TestPerformance {
    [TestClass]
    public class TestPerformanceDeepHierarchy {
        [Injectable(typeof(A), Lifetime.Transient)] public interface IA { }
        public class A : IA { [Inject] IB B; }

        [Injectable(typeof(B), Lifetime.Transient)] public interface IB { }
        public class B : IB { [Inject] IC C; }

        [Injectable(typeof(C), Lifetime.Transient)] public interface IC { }
        public class C : IC { [Inject] ID D; }

        [Injectable(typeof(D), Lifetime.Transient)] public interface ID { }
        public class D : ID { [Inject] IE E; }

        [Injectable(typeof(E), Lifetime.Transient)] public interface IE { }
        public class E : IE { }

        public class Client {
            [Inject] public IA A;
        }

        [Ignore]
        [TestMethod]
        [DataRow(100000)]
        public void TestPerformance_DeepHierarchy(int iterations) {
            var stopwatch = new Stopwatch();
            float min = float.MaxValue;
            float max = float.MinValue;
            float total = 0;

            for (int i = 0; i < iterations; i++) {
                stopwatch.Start();
                Injector.Get<Client>();
                stopwatch.Stop();

                min = Math.Min(min, stopwatch.ElapsedMilliseconds);
                max = Math.Max(max, stopwatch.ElapsedMilliseconds);
                total += stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
            }

            Console.WriteLine($"Total execution {total} ms");
            Console.WriteLine($"Average execution {total / iterations} ms");
            Console.WriteLine($"Min execution {min} ms");
            Console.WriteLine($"Max execution {max} ms");
        }
    }
}
