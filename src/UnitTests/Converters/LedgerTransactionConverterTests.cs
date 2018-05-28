using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTests.Converters
{
    [TestClass]
    public class LedgerTransactionConverterTests
    {
        [TestMethod]
        public void TestSimpleLedgerTx()
        {
            var json = "{transactions:[\"hash1\", \"hash2\"]}";
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Transactions);
            Assert.AreEqual(2, model.Transactions.Length);

            var tx1 = model.Transactions[0];
            Assert.IsFalse(tx1.IsExpanded);
            Assert.AreEqual("hash1", tx1.Hash);

            var tx2 = model.Transactions[1];
            Assert.IsFalse(tx2.IsExpanded);
            Assert.AreEqual("hash2", tx2.Hash);
        }

        [TestMethod]
        public void TestExpandedLedgerTx()
        {
            var json = "{transactions:[{hash:\"hash1\", Account:\"account1\"}, {hash:\"hash2\", Account:\"account2\"}]}";
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Transactions);
            Assert.AreEqual(2, model.Transactions.Length);

            var tx1 = model.Transactions[0];
            Assert.IsTrue(tx1.IsExpanded);
            Assert.AreEqual("hash1", tx1.Hash);
            Assert.AreEqual("account1", tx1.Account);

            var tx2 = model.Transactions[1];
            Assert.IsTrue(tx2.IsExpanded);
            Assert.AreEqual("hash2", tx2.Hash);
            Assert.AreEqual("account2", tx2.Account);
        }

        private class MockModel
        {
            [JsonProperty("transactions")]
            public TransactionState[] Transactions { get; internal set; }
        }
    }
}
