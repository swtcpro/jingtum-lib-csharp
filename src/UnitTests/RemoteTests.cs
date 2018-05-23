using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class RemoteTests
    {
        private const int DeferredWaitingTime = 10000;
        private static string ServerUrl = @"ws://123.57.219.57:5020";
        private static string Secret = @"ss2A7yahPhoduQjmG7z9BHu3uReDk";
        private Remote _remote = null;

        [TestCleanup()]
        public void Cleanup()
        {
            if(_remote != null)
            {
                _remote.Dispose();
                _remote = null;
            }
        }

        [TestMethod]
        public void TestConnect()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<ConnectResponse> response = null;
            _remote.Connect(r =>
            {
                response = r;
                deferred.Start();
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsTrue(_remote.IsConnected);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.IsNotNull(response.Message);
            Assert.IsNull(response.Exception);
        }

        [TestMethod]
        public void TestDisconnect()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<ConnectResponse> response = null;
            _remote.Connect(r =>
            {
                response = r;
                deferred.Start();
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsTrue(_remote.IsConnected);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);

            _remote.Disconnect();
            Assert.IsFalse(_remote.IsConnected);
        }

        [TestMethod]
        public void TestFailedConnect()
        {
            var deferred = new Task(() => { });
            var fakeUrl = @"ws://123.456.789.1:5020";
            _remote = new Remote(fakeUrl);
            MessageResult<ConnectResponse> response = null;
            _remote.Connect(r =>
            {
                response = r;
                deferred.Start();
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsFalse(_remote.IsConnected);
            Assert.IsNull(response.Result);
            Assert.IsNull(response.Message);
            Assert.IsInstanceOfType(response.Exception, typeof(SocketException));
        }

        [TestMethod]
        public void TestRequestServerInfo()
        {
            var deferred = new Task(()=> { });
            _remote = new Remote(ServerUrl);
            MessageResult<ServerInfoResponse> response = null;
            _remote.Connect(r =>
            {
                var req = _remote.RequestServerInfo();
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var serverInfo = response.Result.Info;
            Assert.IsNotNull(serverInfo);
            Assert.IsNotNull(serverInfo.Ledgers);
            Assert.IsNotNull(serverInfo.Node);
            Assert.IsNotNull(serverInfo.State);
            Assert.IsNotNull(serverInfo.Version);
        }

        [TestMethod]
        public void TestRequestLedgerClosed()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<LedgerClosedResponse> response = null;
            _remote.Connect(r =>
            {
                var req = _remote.RequestLedgerClosed();
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var ledger = response.Result;
            Assert.IsNotNull(ledger);
            Assert.IsTrue(ledger.LedgerIndex > 0);
            Assert.IsNotNull(ledger.LedgerHash);
        }

        [TestMethod]
        public void TestRequestLedger()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<LedgerResponse> response = null;
            _remote.Connect(r =>
            {
                // closed ledger
                var req = _remote.RequestLedger();
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var ledger = response.Result.Ledger;
            Assert.IsNotNull(ledger);
            Assert.IsTrue(ledger.LedgerIndex > 0);
            Assert.IsNotNull(ledger.LedgerHash);

            var ledgerIndex = ledger.LedgerIndex;
            var ledgerHash = ledger.LedgerHash;

            deferred = new Task(() => { });
            response = null;
            var options = new LedgerOptions();
            options.LedgerIndex = ledgerIndex;
            _remote.RequestLedger(options).Submit(r=>
            {
                response = r;
                deferred.Start();
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime * 2));

            Assert.IsNotNull(response);
            ledger = response.Result.Ledger;
            Assert.IsNotNull(ledger);
            Assert.AreEqual(ledgerIndex, ledger.LedgerIndex);
            Assert.AreEqual(ledgerHash, ledger.LedgerHash);

            deferred = new Task(() => { });
            response = null;
            options.Accounts = true;
            options.Transactions = true;
            _remote.RequestLedger(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime * 2));

            Assert.IsNotNull(response);
            ledger = response.Result.Ledger;
            Assert.IsNotNull(ledger);
            Assert.AreEqual(ledgerIndex, ledger.LedgerIndex);
            Assert.AreEqual(ledgerHash, ledger.LedgerHash);
            Assert.IsNotNull(ledger.Transactions); // has transactions data
            Assert.IsNotNull(ledger.AccountStates); // has accountStates data
        }

        [TestMethod]
        public void TestRequestTx()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<TxResponse> response = null;
            _remote.Connect(r =>
            {
                var options = new TxOptions();
                // tx for active account "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr" in test server
                options.Hash = "3B92FD6E5A950BEFC7DB6F220CF162236148F01F8FA61C3CFA11CD1ECCC195CC";
                var req = _remote.RequestTx(options);
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var tx = response.Result;
            Assert.IsNotNull(tx);
            Assert.AreEqual("3B92FD6E5A950BEFC7DB6F220CF162236148F01F8FA61C3CFA11CD1ECCC195CC", tx.Hash);
            Assert.AreEqual("jHb9CJAWyB4jr91VRWn96DkukG4bwdtyTh", tx.Account);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", tx.Destination);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "10000" }, tx.Amount);
            Assert.AreEqual(TransactionType.Payment, tx.TransactionType);

            var txr = tx.TxResult as SentTxResult;
            Assert.IsNotNull(txr);
            Assert.AreEqual("3B92FD6E5A950BEFC7DB6F220CF162236148F01F8FA61C3CFA11CD1ECCC195CC", txr.Hash);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txr.CounterParty);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "10000" }, txr.Amount);
            Assert.AreEqual(TxResultType.Sent, txr.Type);
        }
    }
}
