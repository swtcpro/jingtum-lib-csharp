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
    [TestCategory("Json: Converters")]
    public class HexToStringConverterTests
    {
        [TestMethod]
        [DataRow("6A615644616F7A6B6D467A4347777542594C357751335376686E55727953756F666E", "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", DisplayName ="Contract Parameter")]
        [DataRow("666F6F", "foo", DisplayName = "Contract Method")]
        [DataRow("726573756C743D7B7D3B2066756E6374696F6E20496E697428742920613D27636F63612720726573756C743D73634765744163636F756E74496E666F28742920623D277365616E272072657475726E20726573756C7420656E643B2066756E6374696F6E20666F6F28742920726573756C743D73634765744163636F756E74496E666F2874292072657475726E20726573756C7420656E643B", 
            "result={}; function Init(t) a='coca' result=scGetAccountInfo(t) b='sean' return result end; function foo(t) result=scGetAccountInfo(t) return result end;", 
            DisplayName ="Contract Payload")]
        [DataRow("xyz", "xyz", DisplayName ="invalid data")]
        public void TestHexToString(string hex, string data)
        {
            var json = string.Format("{{data:'{0}'}}", hex);
            var model = JsonConvert.DeserializeObject<TestModel>(json);
            Assert.IsNotNull(model);
            Assert.AreEqual(data, model.Data);
        }

        private class TestModel
        {
            [JsonProperty("data")]
            [JsonConverter(typeof(HexToStringConverter))]
            public string Data { get; set; }
        }
    }
}
