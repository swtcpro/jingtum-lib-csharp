using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    [TestCategory("Multithreads")]
    public class MultithreadTests
    {
        private const int DeferredWaitingTime = 10000;
        private static string ServerUrl = @"ws://123.57.219.57:5020";

        [TestMethod]
        public void TestRequestTasks()
        {
            var deferred = new Task(() => { });
            var remote = new Remote(ServerUrl);
            object[] results = new object[3];
            remote.Connect(r =>
            {
                if (r.Exception != null)
                {
                    deferred.Start();
                }
                else
                {
                    var t1 = new Task(() => { });
                    remote.RequestServerInfo().Submit(r1 =>
                    {
                        results[0] = r1.Result;
                        t1.Start();
                    });

                    var t2 = new Task(() => { });
                    remote.RequestLedgerClosed().Submit(r2 =>
                    {
                        results[1] = r2.Result;
                        t2.Start();
                    });

                    var t3 = new Task(() => { });
                    remote.RequestLedger().Submit(r3 =>
                    {
                        results[2] = r3.Result;
                        t3.Start();
                    });

                    Task.WhenAll(t1, t2, t3).ContinueWith(p => { deferred.Start(); });
                }
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime * 3));

            Assert.IsInstanceOfType(results[0], typeof(ServerInfoResponse));
            Assert.IsInstanceOfType(results[1], typeof(LedgerClosedResponse));
            Assert.IsInstanceOfType(results[2], typeof(LedgerResponse));
        }

        [TestMethod]
        public void TestRequestTasksAsync()
        {
            var deferred = new Task(() => { });
            var remote = new Remote(ServerUrl);
            object[] results = new object[3];
            remote.Connect(r =>
            {
                if (r.Exception != null)
                {
                    deferred.Start();
                }
                else
                {
                    var t1 = remote.RequestServerInfo().SubmitAsync(r1 =>
                    {
                        results[0] = r1.Result;
                    });

                    var t2 = remote.RequestLedgerClosed().SubmitAsync(r2 =>
                    {
                        results[1] = r2.Result;
                    });

                    var t3 = remote.RequestLedger().SubmitAsync(r3 =>
                    {
                        results[2] = r3.Result;
                    });

                    Task.WhenAll(t1, t2, t3).ContinueWith(p => { deferred.Start(); });
                }
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime * 3));

            Assert.IsInstanceOfType(results[0], typeof(ServerInfoResponse));
            Assert.IsInstanceOfType(results[1], typeof(LedgerClosedResponse));
            Assert.IsInstanceOfType(results[2], typeof(LedgerResponse));
        }

        [TestMethod]
        public void TestTransactionTasksAsync()
        {
            var deferred = new Task(() => { });
            var remote = new Remote(ServerUrl);

            object result = null;
            var resetEvent = new System.Threading.AutoResetEvent(false);
            remote.Connect(r =>
            {
                var options = new PaymentTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.To = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
                options.Amount = Amount.SWT("0.5");
                var tx = remote.BuildPaymentTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                var task = tx.SubmitAsync(tr =>
                {
                    result = tr.Result;
                });
                task.ContinueWith(_ => { resetEvent.Set(); });
            });
            resetEvent.WaitOne();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestTransactionTasksAsync_LocalSign()
        {
            var deferred = new Task(() => { });
            var remote = new Remote(ServerUrl, true);

            object result = null;
            var resetEvent = new System.Threading.AutoResetEvent(false);
            remote.Connect(r =>
            {
                var options = new PaymentTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.To = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
                options.Amount = Amount.SWT("0.5");
                var tx = remote.BuildPaymentTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                var task = tx.SubmitAsync(tr =>
                {
                    result = tr.Result;
                });
                task.ContinueWith(_ => { resetEvent.Set(); });
            });
            resetEvent.WaitOne();

            Assert.IsNotNull(result);
        }
    }
}
