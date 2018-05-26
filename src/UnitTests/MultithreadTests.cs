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
    public class MultithreadTests
    {
        private const int DeferredWaitingTime = 10000;
        private static string ServerUrl = @"ws://123.57.219.57:5020";

        [TestMethod]
        public void TestTwoTasks()
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

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime * 2));

            Assert.IsInstanceOfType(results[0], typeof(ServerInfoResponse));
            Assert.IsInstanceOfType(results[1], typeof(LedgerClosedResponse));
            Assert.IsInstanceOfType(results[2], typeof(LedgerResponse));
        }
    }
}
