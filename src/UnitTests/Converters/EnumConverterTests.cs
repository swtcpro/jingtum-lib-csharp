using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using JingTum.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class EnumConverterTests
    {
        [TestMethod]
        [DataRow("AccountSet", TxResultType.AccountSet, DisplayName ="Pascal")]
        [DataRow("accountSet", TxResultType.AccountSet, DisplayName = "Camel")]
        [DataRow("account_set", TxResultType.AccountSet, DisplayName = "Underscore")]
        [DataRow("ACCOUNTSET", TxResultType.AccountSet, DisplayName = "UpperCase")]
        [DataRow("accountset", TxResultType.AccountSet, DisplayName = "LowerCase")]
        [DataRow("Unknown", TxResultType.Unknown, DisplayName = "Unknown")]
        [DataRow("Invalid", TxResultType.Unknown, DisplayName = "Invalid")]
        public void TestEnumTypeFromString(string value, TxResultType type)
        {
            var json = string.Format("{{type: '{0}'}}", value);
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.AreEqual(type, model.Type);
        }

        [TestMethod]
        [DataRow((int)TxResultType.Sent, TxResultType.Sent, DisplayName ="Sent")]
        [DataRow((int)TxResultType.Trusting, TxResultType.Trusting, DisplayName = "Trusting")]
        [DataRow(-1, TxResultType.Unknown, DisplayName ="Invalid")]
        public void TestEnumTypeFromInt(int value, TxResultType type)
        {
            var json = string.Format("{{type: {0}}}", value);
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.AreEqual(type, model.Type);
        }

        [TestMethod]
        [DataRow("OfferBought", EffectType.OfferBought, DisplayName ="Pascal")]
        [DataRow("offerBought", EffectType.OfferBought, DisplayName = "Camel")]
        [DataRow("offer_bought", EffectType.OfferBought, DisplayName = "Underscore")]
        [DataRow("OFFERBOUGHT", EffectType.OfferBought, DisplayName = "UpperCase")]
        [DataRow("offerbought", EffectType.OfferBought, DisplayName = "LowerCase")]
        [DataRow("Unknown", EffectType.Unknown, DisplayName = "Unknown")]
        [DataRow("Invalid", null, DisplayName = "Invalid")]
        public void TestNullableEnumTypeFromString(string value, EffectType? effect)
        {
            var json = string.Format("{{effect: '{0}'}}", value);
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.AreEqual(effect, model.Effect);
        }

        [TestMethod]
        [DataRow((int)EffectType.OfferBought, EffectType.OfferBought, DisplayName = "OfferBought")]
        [DataRow((int)EffectType.SetRegularKey, EffectType.SetRegularKey, DisplayName = "SetRegularKey")]
        [DataRow(-1, null, DisplayName = "Invalid")]
        public void TestNullableEnumTypeFromInt(int value, EffectType? type)
        {
            var json = string.Format("{{effect: {0}}}", value);
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.AreEqual(type, model.Effect);
        }

        private class MockModel
        {
            [JsonProperty("type")]
            [JsonConverter(typeof(EnumConverter<TxResultType>))]
            public TxResultType Type { get; set; }
            [JsonProperty("effect")]
            [JsonConverter(typeof(NullableEnumConverter<EffectType>))]
            public EffectType? Effect { get; set; }
        }
    }
}
