using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class HexToUtf8ConverterTests
    {
        [TestMethod]
        [DataRow("E8AEBAE59D9BE79B96E6A5BCE5A596E58AB1", "论坛盖楼奖励", DisplayName ="memo data 1")]
        [DataRow("E5868DE4B880E6ACA1E6B58BE8AF9543616C6C436F6E74726163742032", "再一次测试CallContract 2", DisplayName ="memo data 2")]
        [DataRow("xyz", "xyz", DisplayName ="invalid data")]
        public void TestHexToUtf8(string hex, string data)
        {
            var json = string.Format("{{data:'{0}'}}", hex);
            var model = JsonConvert.DeserializeObject<TestModel>(json);
            Assert.IsNotNull(model);
            Assert.AreEqual(data, model.Data);
        }

        private class TestModel
        {
            [JsonProperty("data")]
            [JsonConverter(typeof(HexToUtf8Converter))]
            public string Data { get; set; }
        }
    }
}
