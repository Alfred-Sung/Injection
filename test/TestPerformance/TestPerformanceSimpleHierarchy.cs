using Injection.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Injection.UnitTest.TestPerformance {
    [TestClass]
    public class TestMemoryUsageSimpleHierarchy {
        [Injectable(typeof(Service), Lifetime.Transient)] public interface IService { }

        public class Service : IService { }

        public class Client {
            [Inject] public IService service;
        }

        [Ignore]
        [TestMethod]
        [DataRow(100000)]
        public void TestPerformance_SimpleHierarchy(int iterations) {
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
