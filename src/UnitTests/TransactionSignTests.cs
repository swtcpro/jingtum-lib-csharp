using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    /// <summary>
    /// Unit tests compare with the result of Node.js.
    /// </summary>
    [TestClass]
    public class TransactionSignTests
    {
        private const int DeferredWaitingTime = 10000;

        [TestMethod]
        public void TestSignPaymentTx()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new PaymentTxOptions();
            options.From = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.To = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Amount = AmountSettings.SWT("0.5");
            var tx = remote.BuildPaymentTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("1200002200000000240000000561400000000007A120684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B8314DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", blob);
        }
    }
}
