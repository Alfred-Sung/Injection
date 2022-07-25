using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Injection.UnitTest.TestPerformance {
    [TestClass]
    public class TestPerformanceComplexHierarchy {
        [Injectable(typeof(A), Lifetime.Transient)] public interface IA { }
        public class A : IA { }

        [Injectable(typeof(B), Lifetime.Transient)] public interface IB : IA { }
        public class B : IB { }

        public class Client {
            [Inject] public IA A;
            [Inject] public IA AA;
            [Inject] public IB B;
            [Inject] public IB BB;
        }

        [Ignore]
        [TestMethod]
        [DataRow(100000)]
        public void TestPerformance_ComplexHierarchy(int iterations) {
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
