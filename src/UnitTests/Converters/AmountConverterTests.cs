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
    [TestCategory("Json: Converters")]
    public class AmountConverterTests
    {
        [TestMethod]
        [DataRow("10000", "0.01", DisplayName ="Fee (0.01 SWT)")]
        [DataRow("500000", "0.5",  DisplayName ="0.5 SWT")]
        [DataRow("89178240000", "89178.24", DisplayName ="Balance")]
        public void TestSWT(string value, string swt)
        {
            var json = string.Format("{{amount: '{0}'}}", value);
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Amount);
            Assert.AreEqual("SWT", model.Amount.Currency);
            Assert.AreEqual("", model.Amount.Issuer);
            Assert.AreEqual(swt, model.Amount.Value);
        }

        [TestMethod]
        public void TestSWTInvalid()
        {
            var json = "{amount: 'abc'}";
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.IsNull(model.Amount);
        }

        [TestMethod]
        [DataRow("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "0.20555", DisplayName ="CNY")]
        [DataRow("COCA", "SEAN", "0.5", DisplayName ="Custom")]
        public void TestCurrency(string currency, string issuer, string value)
        {
            var json = string.Format("{{amount: {{currency: '{0}', issuer: '{1}', value: '{2}'}}}}", currency, issuer, value);
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Amount);
            Assert.AreEqual(currency, model.Amount.Currency);
            Assert.AreEqual(issuer, model.Amount.Issuer);
            Assert.AreEqual(value, model.Amount.Value);
        }

        private class MockModel
        {
            [JsonProperty("amount")]
            public Amount Amount { get; set; }
        }
    }
}
