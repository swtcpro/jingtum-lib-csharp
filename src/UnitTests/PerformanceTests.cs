using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    [TestCategory("Performance")]
    public class PerformanceTests
    {
        private const int DeferredWaitingTime = 10000;
        private static string ServerUrl = @"ws://123.57.219.57:5020";

        public TestContext TestContext { get; set; }

        private static TestContext _testContext;

        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        public void TestOneRemoteMultipleRequests_Sequence(int count)
        {
            var remote = new Remote(ServerUrl);
            var tc = remote.ConnectAsync();
            Assert.IsTrue(tc.Wait(DeferredWaitingTime));

            var watch = new Stopwatch();
            watch.Start();
            var results = new LedgerResponse[count];
            for (int i = 0; i < count; i++)
            {
               var task = remote.RequestLedger().SubmitAsync();
                if (task.Wait(DeferredWaitingTime))
                {
                    results[i] = task.Result.Result;
                }
            }

            watch.Stop();
            TestContext.WriteLine(watch.ElapsedMilliseconds.ToString());

            var failedCount = 0;
            foreach (var result in results)
            {
                var hash = result?.Ledger?.Hash;
                if (hash == null) failedCount++;
            }
            Assert.AreEqual(0, failedCount);
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        public void TestOneRemoteMultipleRequests_Parallel(int count)
        {
            var remote = new Remote(ServerUrl);
            var tc = remote.ConnectAsync();
            Assert.IsTrue(tc.Wait(DeferredWaitingTime));

            var watch = new Stopwatch();
            watch.Start();
            var tasks = new Task<MessageResult<LedgerResponse>>[count];
            for(int i = 0; i < count; i++)
            {
                tasks[i] = remote.RequestLedger().SubmitAsync();
            }

            var success = Task.WaitAll(tasks, DeferredWaitingTime * 100);
            watch.Stop();
            TestContext.WriteLine(watch.ElapsedMilliseconds.ToString());
            Assert.IsTrue(success);

            var failedCount = 0;
            foreach(var task in tasks)
            {
                var result = task.Result;
                var hash = result?.Result?.Ledger?.Hash;
                if (hash == null) failedCount++;
            }
            Assert.AreEqual(0, failedCount);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(50)]
        [DataRow(100)]
        public void TestMultipleRemotes_Sequence(int count)
        {
            var watch = new Stopwatch();
            watch.Start();

            var results = new LedgerResponse[count];
            var tasks = new Task[count];
            for (int i = 0; i < count; i++)
            {
                var remote = new Remote(ServerUrl);
                var tc = remote.ConnectAsync(null, DeferredWaitingTime * 10);
                var index = i;
                var task = tc.ContinueWith(_ =>
                {
                    var tr = remote.RequestLedger().SubmitAsync(null, DeferredWaitingTime);
                    if (tr.Wait(DeferredWaitingTime))
                    {
                        results[index] = tr.Result.Result;
                    }
                });
                task.Wait(DeferredWaitingTime * 2);
            }

            watch.Stop();
            TestContext.WriteLine(watch.ElapsedMilliseconds.ToString());

            var failedCount = 0;
            foreach (var result in results)
            {
                var hash = result?.Ledger?.Hash;
                if (hash == null) failedCount++;
            }
            Assert.AreEqual(0, failedCount);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(50)]
        [DataRow(100)]
        public void TestMultipleRemotes_Parallel(int count)
        {
            var watch = new Stopwatch();
            watch.Start();

            var tasks = new Task<MessageResult<ServerInfoResponse>>[count];
            for (int i = 0; i < count; i++)
            {
                var remote = new Remote(ServerUrl);
                var tc= remote.ConnectAsync(null, DeferredWaitingTime*10);
                var index = i;
                tasks[i] = tc.ContinueWith(_ =>
                {
                    var tr = remote.RequestServerInfo().SubmitAsync(null, DeferredWaitingTime * 10);
                    return tr;
                }).Result;
            }

            var success = Task.WaitAll(tasks, DeferredWaitingTime * 100);
            watch.Stop();
            TestContext.WriteLine(watch.ElapsedMilliseconds.ToString());

            Assert.IsTrue(success);

            var failedCount = 0;
            foreach (var task in tasks)
            {
                var version = task.Result?.Result?.Info?.Version;
                if (version == null) failedCount++;
            }
            Assert.AreEqual(0, failedCount);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(50)]
        [DataRow(100)]
        public void TestOneRemoteDifferentQueries_Parallel(int count)
        {
            var remote = new Remote(ServerUrl);
            var tc = remote.ConnectAsync();
            Assert.IsTrue(tc.Wait(DeferredWaitingTime));

            var watch = new Stopwatch();
            watch.Start();
            var tasks = new Task[count];
            for (int i = 0; i < count; i++)
            {
                tasks[i] = i % 2 == 0
                    ? (Task)remote.RequestLedger().SubmitAsync()
                    : remote.RequestServerInfo().SubmitAsync();
            }

            var success = Task.WaitAll(tasks, DeferredWaitingTime * 100);
            watch.Stop();
            TestContext.WriteLine(watch.ElapsedMilliseconds.ToString());
            Assert.IsTrue(success);

            var failedCount = 0;
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    var task = (Task<MessageResult<LedgerResponse>>)tasks[i];
                    var hash = task.Result?.Result?.Ledger?.Hash;
                    if (hash == null) failedCount++;
                }
                else
                {
                    var task = (Task<MessageResult<ServerInfoResponse>>)tasks[i];
                    var version = task.Result?.Result?.Info?.Version;
                    if (version == null) failedCount++;
                }
            }

            Assert.AreEqual(0, failedCount);
        }
    }
}
