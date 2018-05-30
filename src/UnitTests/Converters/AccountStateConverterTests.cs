using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTests.Converters
{
    [TestClass]
    [TestCategory("Json: Converters")]
    public class AccountStateConverterTests
    {
        [TestMethod]
        public void TestSimpleAccountState()
        {
            var json = "{accountStates:[\"hash1\", \"hash2\"]}";
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.AccountStates);
            Assert.AreEqual(2, model.AccountStates.Length);

            var state1 = model.AccountStates[0];
            Assert.IsFalse(state1.IsExpanded);
            Assert.AreEqual("hash1", state1.Index);

            var state2 = model.AccountStates[1];
            Assert.IsFalse(state2.IsExpanded);
            Assert.AreEqual("hash2", state2.Index);
        }

        [TestMethod]
        public void TestExpandedAccountState()
        {
            var json = "{accountStates:[{index: \"hash1\", LedgerEntryType:\"LedgerHashes\"}, {index: \"hash2\", \"LedgerEntryType\": \"AccountRoot\"}]}";
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.AccountStates);
            Assert.AreEqual(2, model.AccountStates.Length);

            var state1 = model.AccountStates[0];
            Assert.IsTrue(state1.IsExpanded);
            Assert.AreEqual("hash1", state1.Index);
            Assert.IsInstanceOfType(state1, typeof(LedgerHashesAccountState));

            var state2 = model.AccountStates[1];
            Assert.IsTrue(state2.IsExpanded);
            Assert.AreEqual("hash2", state2.Index);
            Assert.IsInstanceOfType(state2, typeof(AccountRootAccountState));
        }

        private class MockModel
        {
            [JsonProperty("accountStates")]
            public AccountState[] AccountStates { get; set; }
        }
    }
}
