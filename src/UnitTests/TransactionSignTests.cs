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
    [TestCategory("Local Sign")]
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
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.To = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Amount = Amount.SWT("0.5");
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

        [TestMethod]
        public void TestSignPaymentTx_CNY()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new PaymentTxOptions();
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.To = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Amount = new Amount("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "12.33");
            var tx = remote.BuildPaymentTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("1200002200000000240000000561D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B8314DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", blob);
        }

        [TestMethod]
        public void TestSignPaymentTx_SendMax()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new PaymentTxOptions();
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.To = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Amount = Amount.SWT("0.5");
            var tx = remote.BuildPaymentTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.SetSendMax(new Amount("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "12.33"));
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("1200002200000000240000000561400000000007A12068400000000000271069D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B8314DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", blob);
        }

        [TestMethod]
        public void TestSignPaymentTx_Full()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new PaymentTxOptions();
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.To = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Amount = Amount.SWT("0.5");
            var tx = remote.BuildPaymentTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.SetFee(20);
            tx.SetFlags(1);
            tx.SetSendMax(Amount.SWT("1000"));
            tx.SetTransferRate(0.5f);
            tx.AddMemo("I Love SWTC!我爱祖国。");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("120000220000000124000000052B59682F0061400000000007A12068400000000000001469400000003B9ACA00732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B8314DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47F9EA7D1B49204C6F7665205357544321E68891E788B1E7A596E59BBDE38082E1F1", blob);
        }

        [TestMethod]
        public void TestSignOfferCreateTx_Sell()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new OfferCreateTxOptions();
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.TakerPays = Amount.SWT("0.5");
            options.TakerGets = new Amount("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "12.33");
            options.Type = OfferType.Sell;
            var tx = remote.BuildOfferCreateTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("1200072200080000240000000564400000000007A12065D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B", blob);
        }

        [TestMethod]
        public void TestSignOfferCreateTx_Buy()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new OfferCreateTxOptions();
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.TakerGets = Amount.SWT("0.5");
            options.TakerPays = new Amount("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "12.33");
            var tx = remote.BuildOfferCreateTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("1200072200000000240000000564D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D4159665400000000007A120684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B", blob);
        }

        [TestMethod]
        public void TestSignOfferCancelTx()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new OfferCancelTxOptions();
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.Sequence = 7;
            var tx = remote.BuildOfferCancelTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("12000822000000002400000005201900000007684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B", blob);
        }

        [TestMethod]
        public void TestSignRelationTx_TrustSet()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new RelationTxOptions();
            options.Type = RelationType.Trust;
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.QualityIn = 1;
            options.QualityOut = 2;
            options.Limit = new Amount("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "12.33");
            var tx = remote.BuildRelationTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("1200142200000000240000000520140000000120150000000263D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B", blob);
        }

        [TestMethod]
        public void TestSignRelationTx_RelationSet()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new RelationTxOptions();
            options.Type = RelationType.Freeze;
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.Target = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Limit = new Amount("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "12.33");
            var tx = remote.BuildRelationTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("1200152200000000240000000520230000000363D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B8714DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", blob);
        }

        [TestMethod]
        public void TestSignRelationTx_RelationDel()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new RelationTxOptions();
            options.Type = RelationType.Unfreeze;
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.Target = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Limit = new Amount("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "12.33");
            var tx = remote.BuildRelationTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("1200162200000000240000000520230000000363D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B8714DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", blob);
        }

        [TestMethod]
        public void TestSignAccountSetTx_Property()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new AccountSetTxOptions();
            options.Type = AccountSetType.Property;
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.SetFlag = SetClearFlags.DisallowSWT;
            options.ClearFlag = SetClearFlags.NoFreeze;
            var tx = remote.BuildAccountSetTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("12000322000000002400000005202100000003202200000006684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B", blob);
        }

        [TestMethod]
        public void TestSignAccountSetTx_Delegate()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new AccountSetTxOptions();
            options.Type = AccountSetType.Delegate;
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.DelegateKey = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            var tx = remote.BuildAccountSetTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("12000522000000002400000005684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB811460B1227191135B3B16CB1D74F2509BD5C5DF985B8814DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", blob);
        }

        [TestMethod]
        public void TestDeployContractTx()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new DeployContractTxOptions();
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.Amount = 30;
            options.Payload = "result={}; function Init(t) result=scGetAccountInfo(t) return result end; function foo(t) a={} result=scGetAccountInfo(t) return result end;";
            options.Params = new[] { "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr" };
            var tx = remote.DeployContractTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.AddMemo("Test deploy contract.");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime*10000));
            Assert.AreEqual("12001E22000000002400000005202400000000614000000001C9C380684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB7F8C726573756C743D7B7D3B2066756E6374696F6E20496E697428742920726573756C743D73634765744163636F756E74496E666F2874292072657475726E20726573756C7420656E643B2066756E6374696F6E20666F6F28742920613D7B7D20726573756C743D73634765744163636F756E74496E666F2874292072657475726E20726573756C7420656E643B811460B1227191135B3B16CB1D74F2509BD5C5DF985BF9EA7D1554657374206465706C6F7920636F6E74726163742EE1F1FAEB7012226A4D773378726B5832795377645169456F72796D7975544C55535361383577765372E1F1", blob);
        }

        [TestMethod]
        public void TestCallContractTx()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            var deferred = new Task(() => { });
            string blob = null;
            var options = new CallContractTxOptions();
            options.Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.Destination = "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT";
            options.Foo = "foo";
            options.Params = new[] { "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr" };
            var tx = remote.CallContractTx(options);
            tx.Data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";
            tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
            tx.AddMemo("Test call contract.");
            tx.Sign(r =>
            {
                blob = r.Result;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));
            Assert.AreEqual("12001E22000000002400000005202400000001684000000000002710732102EA9C744CB32386A12B4E85A2E4A7844B3952528F700C1031DCBCD1DAC07ECD1F74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB701103666F6F811460B1227191135B3B16CB1D74F2509BD5C5DF985B8314BBDC8C3F874E0EB6957A3970C969E45CDCF48457F9EA7D13546573742063616C6C20636F6E74726163742EE1F1FAEB7012226A4D773378726B5832795377645169456F72796D7975544C55535361383577765372E1F1", blob);
        }
    }
}
