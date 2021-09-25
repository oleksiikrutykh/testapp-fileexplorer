using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading.Tests.PriorityHandling
{
    [TestFixture]
    public class PriorityQueueTest
    {
        [SetUp]
        public static void Setup()
        {
        }

        [TearDown]
        public static void Teardown()
        {
        }

        [Test]
        public static async Task TestEnqueueWithUniformPriority()
        {
            var queue = new PriorityQueue<int, int>(async (id) =>
                                                            {
                                                                await Task.Delay(100);
                                                                return id;
                                                            },
                                                     1);


            var results = new List<int>();
            var loadingTasks = new List<Task<int>>();
            for (int i = 1; i <= 10; i++)
            {
                var item = new QueueItem<int, int>(10, i);
                queue.Enqueue(item);
                loadingTasks.Add(item.WhenCompleted());
            }

            while (loadingTasks.Count > 0)
            {
                var t = await Task.WhenAny(loadingTasks);
                loadingTasks.Remove(t);
                results.Add(t.Result);
            }

            await Task.WhenAll<int>(loadingTasks);

            var expected = Enumerable.Range(1, 10).ToArray();
            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public static async Task TestEnqueueWithVariousPriorities()
        {
            var queue = new PriorityQueue<int, int>(async (id) =>
                                                            {
                                                                await Task.Delay(100);
                                                                return id;
                                                            },
                                                   1);

            queue.Enqueue(-1, 1);

            var result = new List<int>();
            var loadingTasks = new List<Task<int>>();
            for (int i = 0; i < 10; i++)
            {
                var item = new QueueItem<int, int>(i % 2, i);
                 queue.Enqueue(item);
                loadingTasks.Add(item.WhenCompleted());
            }

            while (loadingTasks.Count > 0)
            {
                var t = await Task.WhenAny(loadingTasks);
                loadingTasks.Remove(t);
                result.Add(t.Result);
            }

            await Task.WhenAll<int>(loadingTasks);

            var expectedResult = new int[] { 1, 3, 5, 7, 9, 0, 2, 4, 6, 8 };
            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}
