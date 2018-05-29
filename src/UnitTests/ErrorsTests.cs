using Microsoft.VisualStudio.TestTools.UnitTesting;
using JingTum.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class ErrorsTests
    {
        [TestMethod]
        [DataRow("NONE", null)]
        [DataRow(null, null)]
        [DataRow("tecCLAIM", "Fee claimed. Sequence used. No action.")]
        [DataRow("tecNO_REGULAR_KEY", "Regular key is not set.")]
        [DataRow("tecNOREGULARKEY", "Regular key is not set.")]
        [DataRow("TECNO_REGULAR_KEY", "Regular key is not set.")]
        [DataRow("tecno_regular_key", "Regular key is not set.")]
        public void TestGetErrorMessage(string key, string message)
        {
            Assert.AreEqual(message, Errors.GetErrorMessage(key));
        }
    }
}
