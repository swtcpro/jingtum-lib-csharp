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
    public class TransactionTests
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
        [DataRow(true, DisplayName ="Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemotePaymentTx(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            PaymentTxResponse result = null;
            _remote.Connect(_=>
            {
                var options = new PaymentTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.To = "jBKaXuYemkAb5HytZgosAcWgWDZbBvz6KR";
                options.Amount = Amount.SWT("0.5");
                var tx = _remote.BuildPaymentTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test PaymentTx at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteOfferCreateTx_Buy(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            OfferCreateTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new OfferCreateTxOptions();
                options.Type = OfferType.Buy;
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.TakerPays = Amount.SWT("5000");
                options.TakerGets = new Amount("CNY", "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", "0.01");
                var tx = _remote.BuildOfferCreateTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test OfferCreate (buy) at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteOfferCreateTx_Sell(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            OfferCreateTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new OfferCreateTxOptions();
                options.Type = OfferType.Sell;
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.TakerGets = Amount.SWT("0.5");
                options.TakerPays = new Amount("CNY", "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", "100.01");
                var tx = _remote.BuildOfferCreateTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test OfferCreate (sell) at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteOfferCancelTx(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            OfferCancelTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new OfferCancelTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.Sequence = 1;
                var tx = _remote.BuildOfferCancelTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test OfferCancel at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteRelationTx_TrustSet(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            RelationTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new RelationTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.Type = RelationType.Trust;
                options.Limit = new Amount("CNY", "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", "100");
                var tx = _remote.BuildRelationTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test relation (trust set) at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteRelationTx_RelationSet(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            RelationTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new RelationTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.Type = RelationType.Authorize;
                options.Target = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
                options.Limit = new Amount("CNY", "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", "100");
                var tx = _remote.BuildRelationTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test relation (relation set) at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteRelationTx_RelationDel(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            RelationTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new RelationTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.Type = RelationType.Unfreeze;
                options.Target = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
                options.Limit = new Amount("CNY", "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", "100");
                var tx = _remote.BuildRelationTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test relation (relation del) at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteAccountSetTx_Property(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            AccountSetTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new AccountSetTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.Type = AccountSetType.Property;
                options.SetFlag = SetClearFlags.DisallowSWT;
                options.ClearFlag = SetClearFlags.NoFreeze;
                var tx = _remote.BuildAccountSetTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test AccountSet (property) at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Local Sign")]
        [DataRow(false, DisplayName = "Server Sign")]
        public void TestRemoteAccountSetTx_Delegate(bool localSign)
        {
            _remote = new Remote(ServerUrl, localSign);
            var resetEvent = new AutoResetEvent(false);
            AccountSetTxResponse result = null;
            _remote.Connect(_ =>
            {
                var options = new AccountSetTxOptions();
                options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
                options.Type = AccountSetType.Delegate;
                options.DelegateKey = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
                var tx = _remote.BuildAccountSetTx(options);
                tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
                tx.AddMemo("Unit Test AccountSet (delegate) at " + DateTime.Now.ToString());
                tx.Submit(r =>
                {
                    result = r.Result;
                    resetEvent.Set();
                });
            });

            Assert.IsTrue(resetEvent.WaitOne(DeferredWaitingTime));
            Assert.IsNotNull(result);
            Assert.AreEqual("tesSUCCESS", result.EngineResult);
        }
    }
}
