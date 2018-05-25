using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class DateTimeConverterTests
    {
        [TestMethod]
        [DataRow(579097134, "2018/05/08 20:18:54", DisplayName = "Timestamp")]
        [DataRow(579097140, "2018/05/08 20:19:00", DisplayName = "Date")]
        [DataRow(579428920, "2018/05/12 16:28:40", DisplayName = "CloseTime")]
        [DataRow(0, "0001/01/01 00:00:00", DisplayName = "Invalid Date")]
        public void TestDateTime(int unixTime, string date)
        {
            var json = string.Format("{{date: {0}}}", unixTime);
            var model = JsonConvert.DeserializeObject<MockModel>(json);
            Assert.IsNotNull(model);
            Assert.AreEqual(date, model.Date.ToString("yyyy\\/MM\\/dd HH:mm:ss"));
        }

        private class MockModel
        {
            [JsonProperty("date")]
            [JsonConverter(typeof(JingTum.Lib.DateTimeConverter))]
            public DateTime Date { get; set; }
        }
    }
}
