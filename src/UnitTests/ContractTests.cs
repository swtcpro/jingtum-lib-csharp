using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    [TestCategory("Remote: Transaction")]
    public class ContractTests
    {
        private const int DeferredWaitingTime = 10000;
        private static string ServerUrl = @"ws://139.129.194.175:5020";
        private Remote _remote = null;

        [TestCleanup()]
        public void Cleanup()
        {
            if (_remote != null)
            {
                _remote.Dispose();
                _remote = null;
            }
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteDeployContractTx(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            DeployContractTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new DeployContractTxOptions();
                options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
                options.Amount = 0.5;
                options.Payload = "result={}; function Init(t) result=scGetAccountInfo(t) return result end; function foo(t) a={} result=scGetAccountInfo(t) return result end;";
                options.Params = new[] { "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr" };
                var tx = _remote.DeployContractTx(options);
                tx.SetSecret("snhevp2qBEyPf3n8ZRDMuZGSa3BZ5");
                tx.AddMemo("Unit Test DeployContract at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tecNO_DST_INSUF_SWT", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteCallContractTx(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            CallContractTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new CallContractTxOptions();
                options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
                options.Destination = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn";
                options.Foo = "foo";
                options.Params = new[] { "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr" };
                var tx = _remote.CallContractTx(options);
                tx.SetSecret("snhevp2qBEyPf3n8ZRDMuZGSa3BZ5");
                tx.AddMemo("Unit Test DeployContract at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            Assert.IsNotNull(result.ContractState);
        }
    }
}
