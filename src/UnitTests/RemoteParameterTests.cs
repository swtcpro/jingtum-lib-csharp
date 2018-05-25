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
    public class RemoteParameterTests
    {
        private const int DeferredWaitingTime = 10000;
        private Remote _remote;

        [TestInitialize]
        public void Initialize()
        {
            _remote = new Remote("");
            _remote.SetMockServer(new MockServer(_remote, "Error.json"));
        }

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
        public void TestRequestLedger_Invalid()
        {
            var deferred = new Task(() => { });
            MessageResult<LedgerResponse> response = null;
            var options = new LedgerOptions();
            options.LedgerHash = "abcxyz";
            _remote.RequestLedger(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidHashException));
        }

        [TestMethod]
        public void TestRequestTx_Invalid()
        {
            var deferred = new Task(() => { });
            MessageResult<TxResponse> response = null;
            var options = new TxOptions();
            options.Hash = "abcxyz";
            _remote.RequestTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidHashException));
        }

        [TestMethod]
        public void TestRequestAccountInfo_Invalid()
        {
            var deferred = new Task(() => { });
            MessageResult<AccountInfoResponse> response = null;
            var options = new AccountInfoOptions();
            options.Account = "abcxyz";
            _remote.RequestAccountInfo(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAddressException));

            deferred = new Task(() => { });
            response = null;
            options = new AccountInfoOptions();
            options.Ledger = new LedgerSettings();
            options.Ledger.LedgerHash = "abcxyz";
            _remote.RequestAccountInfo(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidHashException));
        }

        [TestMethod]
        public void TestRequestAccountTums_Invalid()
        {
            var deferred = new Task(() => { });
            MessageResult<AccountTumsResponse> response = null;
            var options = new AccountTumsOptions();
            options.Account = "abcxyz";
            _remote.RequestAccountTums(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAddressException));

            deferred = new Task(() => { });
            response = null;
            options = new AccountTumsOptions();
            options.Ledger = new LedgerSettings();
            options.Ledger.LedgerHash = "abcxyz";
            _remote.RequestAccountTums(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidHashException));
        }

        [TestMethod]
        public void TestRequestAccountRelations_Invalid()
        {
            var deferred = new Task(() => { });
            MessageResult<AccountRelationsResponse> response = null;
            var options = new AccountRelationsOptions();
            options.Account = "abcxyz";
            _remote.RequestAccountRelations(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAddressException));

            deferred = new Task(() => { });
            response = null;
            options = new AccountRelationsOptions();
            options.Ledger = new LedgerSettings();
            options.Ledger.LedgerHash = "abcxyz";
            _remote.RequestAccountRelations(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidHashException));

            deferred = new Task(() => { });
            response = null;
            options = new AccountRelationsOptions();
            options.Type = RelationType.Authorize;
            options.Peer = "abcxyz";
            _remote.RequestAccountRelations(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAddressException));
            var ex = response.Exception as InvalidAddressException;
            Assert.AreEqual("Peer", ex.Name);
        }

        [TestMethod]
        public void TestRequestAccountOffers_Invalid()
        {
            var deferred = new Task(() => { });
            MessageResult<AccountOffersResponse> response = null;
            var options = new AccountOffersOptions();
            options.Account = "abcxyz";
            _remote.RequestAccountOffers(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAddressException));

            deferred = new Task(() => { });
            response = null;
            options = new AccountOffersOptions();
            options.Ledger = new LedgerSettings();
            options.Ledger.LedgerHash = "abcxyz";
            _remote.RequestAccountOffers(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidHashException));
        }

        [TestMethod]
        public void TestRequestOrderBook_Invalid()
        {
            var deferred = new Task(() => { });
            MessageResult<OrderBookResponse> response = null;
            var options = new OrderBookOptions();
            options.Gets = new Amount("SWT", "", "1");
            options.Pays = new Amount("abc", "xyz", "def");
            _remote.RequestOrderBook(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAmountException));
            var ex = response.Exception as InvalidAmountException;
            Assert.AreEqual("Pays", ex.Name);

            deferred = new Task(() => { });
            response = null;
            options = new OrderBookOptions();
            options.Pays = new Amount { Currency = "SWT", Issuer = "", Value = "1" };
            options.Gets = new Amount { Currency = "abc", Issuer = "xyz", Value = "def" };
            _remote.RequestOrderBook(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAmountException));
            ex = response.Exception as InvalidAmountException;
            Assert.AreEqual("Gets", ex.Name);
        }

        [TestMethod]
        public void TestRequestPathFind_Invalid()
        {
            var deferred = new Task(() => { });
            MessageResult<PathFindResponse> response = null;
            var options = new PathFindOptions();
            options.Account = "abcxyz";
            options.Destination = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Amount = new Amount { Currency = "ABC", Issuer = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", Value = "1" };
            _remote.RequestPathFind(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAddressException));
            var ex = response.Exception as InvalidAddressException;
            Assert.AreEqual("Account", ex.Name);

            deferred = new Task(() => { });
            response = null;
            options = new PathFindOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Destination = "abcxyz";
            options.Amount = new Amount { Currency = "ABC", Issuer = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", Value = "1" };
            _remote.RequestPathFind(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAddressException));
            ex = response.Exception as InvalidAddressException;
            Assert.AreEqual("Destination", ex.Name);

            deferred = new Task(() => { });
            response = null;
            options = new PathFindOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Destination = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Amount = new Amount { Currency = "ABC", Issuer = "abcxyz", Value = "1" };
            _remote.RequestPathFind(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Exception, typeof(InvalidAmountException));
            var ex1 = response.Exception as InvalidAmountException;
            Assert.AreEqual("Amount", ex1.Name);
        }
    }
}
