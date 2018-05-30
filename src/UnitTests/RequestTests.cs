using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    [TestCategory("Remote: Request")]
    public class RequestTests
    {
        private const int DeferredWaitingTime = 10000;
        private static string ServerUrl = @"ws://123.57.219.57:5020";
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
        public void TestRequestServerInfo()
        {
            var deferred = new Task(() => { });
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

        [TestMethod]
        public void TestRequestAccountInfo()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<AccountInfoResponse> response = null;
            _remote.Connect(r =>
            {
                var options = new AccountInfoOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                var req = _remote.RequestAccountInfo(options);
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            var accountData = response.Result.AccountData;
            Assert.IsNotNull(accountData);
            Assert.AreEqual("j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", accountData.Account);
        }

        [TestMethod]
        public void TestRequestAccountTums()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<AccountTumsResponse> response = null;
            _remote.Connect(r =>
            {
                var options = new AccountTumsOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                var req = _remote.RequestAccountTums(options);
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.SendCurrencies);
            Assert.IsNotNull(result.ReceiveCurrencies);
        }

        [TestMethod]
        public void TestRequestAccountRelations()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<AccountRelationsResponse> response = null;
            _remote.Connect(r =>
            {
                var options = new AccountRelationsOptions();
                options.Limit = 1;
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                var req = _remote.RequestAccountRelations(options);
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", result.Account);
            Assert.IsNotNull(result.Lines);
        }

        [TestMethod]
        public void TestRequestAccountOffers()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<AccountOffersResponse> response = null;
            _remote.Connect(r =>
            {
                var options = new AccountOffersOptions();
                options.Limit = 1;
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                var req = _remote.RequestAccountOffers(options);
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", result.Account);
            Assert.IsNotNull(result.Offers);
        }

        [TestMethod]
        public void TestRequestAccountTx()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<AccountTxResponse> response = null;
            _remote.Connect(r =>
            {
                var options = new AccountTxOptions();
                options.Limit = 1;
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                var req = _remote.RequestAccountTx(options);
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", result.Account);
            Assert.IsNotNull(result.Transactions);
            Assert.IsNotNull(result.Marker);
        }

        [TestMethod]
        public void TestRequestOrderBook()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<OrderBookResponse> response = null;
            _remote.Connect(r =>
            {
                var options = new OrderBookOptions();
                options.Limit = 1;
                options.Gets = Amount.SWT();
                options.Pays = new Amount("CNY", "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS");
                var req = _remote.RequestOrderBook(options);
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Offers);
        }

        [TestMethod]
        public void TestRequestPathFind()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<PathFindResponse> response = null;
            _remote.Connect(r =>
            {
                var options = new PathFindOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.Destination = "jBKaXuYemkAb5HytZgosAcWgWDZbBvz6KR";
                options.Amount = new Amount("CNY", "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", "0.1");
                var req = _remote.RequestPathFind(options);
                req.Submit(r1 =>
                {
                    response = r1;
                    deferred.Start();
                });
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", result.Source);
            Assert.AreEqual("jBKaXuYemkAb5HytZgosAcWgWDZbBvz6KR", result.Destination);
        }
    }
}
