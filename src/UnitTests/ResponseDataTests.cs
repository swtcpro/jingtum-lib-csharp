using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using JingTum.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using JingTum.Lib.Core;
using System.Threading;

namespace UnitTests
{
    [TestClass]
    public class ResponseDataTests
    {
        private const int DeferredWaitingTime = 10000;

        [TestMethod]
        public void TestErrorResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "Error.json"));
            var deferred = new Task(() => { });
            MessageResult<ServerInfoResponse> response = null;

            remote.RequestServerInfo().Submit(r=>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var exception = response.Exception as ResponseException;
            Assert.IsNotNull(exception);
            Assert.AreEqual("txnNotFound", exception.Error);
            Assert.AreEqual("Transaction not found.", exception.Message);
            Assert.AreEqual(27, exception.ErrorCode);

            var request = exception.Request.Replace("\r\n", "").Replace(" ", "");
            Assert.AreEqual("{\"command\":\"tx\",\"id\":1,\"transaction\":\"084C7823C318B8921A362E39C67A6FB15ADA5BCCD0C7E9A3B13485B1EF2A4313\"}", request);
        }

        [TestMethod]
        public void TestConnectResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "Connect.json", false));
            MessageResult<ConnectResponse> response = null;
            var deferred = new Task(() => { });

            remote.Connect(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response.Result);
            var result = response.Result;
            Assert.AreEqual(10, result.FeeBase);
            Assert.AreEqual(10, result.FeeRef);
            Assert.AreEqual("iZ2848appwoZ", result.HostId);
            Assert.AreEqual("EA11FCEE992292106808E2D3B124EF6C63B7EDE4E094177F96785F8D6AC8344B", result.LedgerHash);
            Assert.AreEqual((uint)1640880, result.LedgerIndex);
            Assert.AreEqual(Utils.UnitTimeToDateTime(579432630), result.LedgerTime);
            Assert.AreEqual(256, result.LoadBase);
            Assert.AreEqual(256, result.LoadFactor);
            Assert.AreEqual("n9M1PrHjWz6GV6y6zaJk1UrPe3cik7hgvnoDAGNpyieQFA5BwfXq", result.PubkeyNode);
            Assert.AreEqual(10000000, result.ReserveBase);
            Assert.AreEqual(1000000, result.ReserveInc);
            Assert.AreEqual("proposing", result.Status);
            Assert.AreEqual("1439816-1640880", result.ValidatedLedgers);
        }

        [TestMethod]
        public void TestRequestServerInfoResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestServerInfo.json"));
            MessageResult<ServerInfoResponse> response = null;
            var deferred = new Task(() => { });

            remote.RequestServerInfo().Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            var info = result.Info;
            Assert.IsNotNull(info);

            Assert.AreEqual("iZ2848appwoZ", info.HostId);
            Assert.AreEqual("1439816-1641979", info.Ledgers);
            Assert.AreEqual("n9M1PrHjWz6GV6y6zaJk1UrPe3cik7hgvnoDAGNpyieQFA5BwfXq", info.Node);
            Assert.AreEqual("proposing   69:31:03", info.State);
            Assert.AreEqual("0.28.1", info.Version);
        }

        [TestMethod]
        public Task TestRequestLedgerClosedResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestLedgerClosed.json"));
            MessageResult<LedgerClosedResponse> response = null;
            var deferred = new Task(() => { });

            remote.RequestLedgerClosed().Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual(1642044, result.LedgerIndex);
            Assert.AreEqual("178D8339EBAA2C06356692FE43AF977175E4F03B5A06146951E013F88FAC9084", result.LedgerHash);

            return Task.FromResult(0);
        }

        [TestMethod]
        public void TestRequestLedgerResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestLedger.json"));
            MessageResult<LedgerResponse> response = null;
            var deferred = new Task(() => { });

            remote.RequestLedger().Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            var ledger = result.Ledger;
            Assert.IsNotNull(ledger);

            Assert.AreEqual(true, ledger.Accepted);
            Assert.AreEqual("49424395D2B6AC2A2717CD17C40570951809A59EA96AFFCFAE2D5E704FD2B071", ledger.AccountHash);
            Assert.AreEqual(true, ledger.Closed);
            Assert.AreEqual("2018/05/12 21:43:30", ledger.CloseTime.ToString("yyyy\\/MM\\/dd HH:mm:ss"));
            Assert.AreEqual("2018-May-12 13:43:30", ledger.CloseTimeHuman);
            Assert.AreEqual(10, ledger.CloseTimeResolution);
            Assert.AreEqual("B405C42D4F5E5BEF1D07CB07235DDADD51A13158F8142649700340C26BE18D2E", ledger.Hash);
            Assert.AreEqual("B405C42D4F5E5BEF1D07CB07235DDADD51A13158F8142649700340C26BE18D2E", ledger.LedgerHash);
            Assert.AreEqual((uint)1642398, ledger.LedgerIndex);
            Assert.AreEqual("F34A4D87C2440F8AE44F7556D16646140CD5132585AA966D5537ACA25176B3C5", ledger.ParentHash);
            Assert.AreEqual("1642398", ledger.SeqNum);
            Assert.AreEqual("600000000000000000", ledger.TotalCoins);
            Assert.AreEqual("0000000000000000000000000000000000000000000000000000000000000000", ledger.TransactionHash);
        }

        [TestMethod]
        public void TestRequestLedgerResponse_Transactions()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestLedger_Transactions.json"));
            MessageResult<LedgerResponse> response = null;
            var deferred = new Task(() => { });

            remote.RequestLedger().Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            var ledger = result.Ledger;
            Assert.IsNotNull(ledger);

            Assert.IsNotNull(ledger.Transactions);
            Assert.AreEqual(1, ledger.Transactions.Length);

            var tx0 = ledger.Transactions[0];
            Assert.IsFalse(tx0.IsExpanded);
            Assert.AreEqual("08759EF7C9553C1A1DE014C88FFA9E8F4356E4962105BBE2D2E973B51714403A", tx0.Hash);
        }

        [TestMethod]
        public void TestRequestLedgerResponse_TransactionsExpanded()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestLedger_TransactionsExpanded.json"));

            MessageResult<LedgerResponse> response = null;
            var deferred = new Task(() => { });
            remote.RequestLedger().Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            var ledger = result.Ledger;
            Assert.IsNotNull(ledger);

            Assert.IsNotNull(ledger.Transactions);
            Assert.AreEqual(1, ledger.Transactions.Length);

            var tx0 = ledger.Transactions[0];
            Assert.IsTrue(tx0.IsExpanded);
            Assert.AreEqual("08759EF7C9553C1A1DE014C88FFA9E8F4356E4962105BBE2D2E973B51714403A", tx0.Hash);
            Assert.AreEqual("jHb9CJAWyB4jr91VRWn96DkukG4bwdtyTh", tx0.Account);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "10000" }, tx0.Amount);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", tx0.Destination);
            Assert.AreEqual("10", tx0.Fee);
            Assert.AreEqual(2147483648, tx0.Flags);
            Assert.AreEqual(1135, tx0.Sequence);
            Assert.AreEqual("0330E7FC9D56BB25D6893BA3F317AE5BCF33B3291BD63DB32654A313222F7FD020", tx0.SigningPubKey);
            Assert.AreEqual(TransactionType.Payment, tx0.TransactionType);
            Assert.AreEqual("3045022100989850A789C74C106EDCCEE3DE5986E24EF7F8B9E7BD63E7E0F19AC80B8A1A4302205330CB17C8AF08AFAF07FE3BAC7E5A90B55E52D53BF2EDC5A5AC65ECA396EE90", tx0.TxnSignature);
            Assert.IsNotNull(tx0.Meta);
            Assert.IsNotNull(tx0.TxResult);
            Assert.AreEqual(TxResultType.Sent, tx0.TxResult.Type);
        }

        [TestMethod]
        public void TestRequestLedgerResponse_Accounts()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestLedger_Accounts.json"));

            MessageResult<LedgerResponse> response = null;
            var deferred = new Task(() => { });
            remote.RequestLedger().Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            var ledger = result.Ledger;
            Assert.IsNotNull(ledger);

            Assert.IsNotNull(ledger.AccountStates);
            Assert.AreEqual(2, ledger.AccountStates.Length);

            var state0 = ledger.AccountStates[0];
            Assert.IsFalse(state0.IsExpanded);
            Assert.AreEqual("01FB2F8725836F327FB22483523B443B6AA3027B993CA69D8F158CC28613C4FE", state0.Index);

            var state1 = ledger.AccountStates[1];
            Assert.IsFalse(state1.IsExpanded);
            Assert.AreEqual("021184D175172280FF9475E60B266F946F4F2611D61EF2731A627CFAD7A96471", state1.Index);
        }

        [TestMethod]
        public void TestRequestLedgerResponse_AccountsExpanded()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestLedger_AccountsExpanded.json"));

            MessageResult<LedgerResponse> response = null;
            var deferred = new Task(() => { });
            remote.RequestLedger().Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            var ledger = result.Ledger;
            Assert.IsNotNull(ledger);

            Assert.IsNotNull(ledger.AccountStates);
            Assert.AreEqual(7, ledger.AccountStates.Length);

            var state0 = ledger.AccountStates[0] as LedgerHashesAccountState;
            Assert.IsNotNull(state0);
            Assert.IsTrue(state0.IsExpanded);
            Assert.AreEqual(LedgerEntryType.LedgerHashes, state0.LedgerEntryType);
            Assert.AreEqual("01FB2F8725836F327FB22483523B443B6AA3027B993CA69D8F158CC28613C4FE", state0.Index);
            Assert.IsNotNull(state0.Hashes);
            Assert.IsTrue(state0.Hashes.Length > 0);

            var state1 = ledger.AccountStates[1] as AccountRootAccountState;
            Assert.IsNotNull(state1);
            Assert.IsTrue(state1.IsExpanded);
            Assert.AreEqual(LedgerEntryType.AccountRoot, state1.LedgerEntryType);
            Assert.AreEqual("021184D175172280FF9475E60B266F946F4F2611D61EF2731A627CFAD7A96471", state1.Index);
            Assert.AreEqual("jBQEaLcymFvEtXx6KFNsUWER95JBKfvo54", state1.Account);
            Assert.AreEqual("10", state1.Balance.Value);

            var state2 = ledger.AccountStates[2] as StateAccountState;
            Assert.IsNotNull(state2);
            Assert.IsTrue(state2.IsExpanded);
            Assert.AreEqual(LedgerEntryType.State, state2.LedgerEntryType);
            Assert.AreEqual("099C71F7CD6576D9C990D7921CAF5782629113F426F6680FFB2757323C65BB61", state2.Index);

            var state3 = ledger.AccountStates[3] as FeeSettingsAccountState;
            Assert.IsNotNull(state3);
            Assert.IsTrue(state3.IsExpanded);
            Assert.AreEqual(LedgerEntryType.FeeSettings, state3.LedgerEntryType);
            Assert.AreEqual("4BC50C9B0D8515D3EAAE1E74B29A95804346C491EE1A95BF25E4AAB854A6A651", state3.Index);

            var state4 = ledger.AccountStates[4] as DirectoryNodeAccountState;
            Assert.IsNotNull(state4);
            Assert.IsTrue(state4.IsExpanded);
            Assert.AreEqual(LedgerEntryType.DirectoryNode, state4.LedgerEntryType);
            Assert.AreEqual("90B6ADE38D0E124E8F4DB570FA5CDA58431910DCB501B633456D8BA81EE8AC13", state4.Index);
            Assert.AreEqual("jJbpadnWHeqBN7aK6wcDj71FuW27bu6Gk5", state4.Owner);
            Assert.IsNotNull(state4.Indexes);

            var state5 = ledger.AccountStates[5] as SkywellStateAccountState;
            Assert.IsNotNull(state5);
            Assert.IsTrue(state5.IsExpanded);
            Assert.AreEqual(LedgerEntryType.SkywellState, state5.LedgerEntryType);
            Assert.AreEqual("A02189187F1A6403A2AA64EEE7C30D2C69A3AA19F9ED6624695A15C94CB0B655", state5.Index);
            Assert.IsNotNull(state5.Balance);
            Assert.IsNotNull(state5.HighLimit);
            Assert.IsNotNull(state5.LowLimit);

            var state6 = ledger.AccountStates[6] as ManageIssuerAccountState;
            Assert.IsNotNull(state6);
            Assert.IsTrue(state6.IsExpanded);
            Assert.AreEqual(LedgerEntryType.ManageIssuer, state6.LedgerEntryType);
            Assert.AreEqual("E58282F5EF495989A8F66991B26162B8FA9B9D393EC64E232DAAB454D69C6507", state6.Index);
            Assert.AreEqual("j9cZ5oHbdL4Z9Mar6TdnfAos35nVzYuNds", state6.IssuerAccountID);
        }

        [TestMethod]
        public void TestRequestTxResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestTx.json"));

            MessageResult<TxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new TxOptions();
            options.Hash = "8CF8868AE692CCC7B6E0882A3FF06DD6E0757EB73549473F2C4F5BE7A5161B67";
            remote.RequestTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var tx = response.Result;
            Assert.IsNotNull(tx);

            Assert.AreEqual("j4VnosbcpqPMRf1kMoyvfxjmWyUfVJGWSB", tx.Account);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "30" }, tx.Amount);
            Assert.AreEqual("jNkJTKfhkQF2dLUurYQCyqyyGkYywn1tQm", tx.Destination);
            Assert.AreEqual(32746, tx.Sequence);
            Assert.AreEqual(TransactionType.Payment, tx.TransactionType);
            Assert.AreEqual("8CF8868AE692CCC7B6E0882A3FF06DD6E0757EB73549473F2C4F5BE7A5161B67", tx.Hash);
            Assert.IsTrue(tx.Validated);
            Assert.IsNotNull(tx.Meta);

            var txResult = tx.TxResult as SentTxResult;
            Assert.IsNotNull(txResult);
            Assert.AreEqual(TxResultType.Sent, txResult.Type);
            Assert.AreEqual("jNkJTKfhkQF2dLUurYQCyqyyGkYywn1tQm", txResult.CounterParty);
            Assert.AreEqual("8CF8868AE692CCC7B6E0882A3FF06DD6E0757EB73549473F2C4F5BE7A5161B67", txResult.Hash);
        }

        [TestMethod]
        public void TestRequestAccountInfoResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountInfo.json"));

            MessageResult<AccountInfoResponse> response = null;
            var deferred = new Task(() => { });
            var options = new AccountInfoOptions();
            options.Account = "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT";
            remote.RequestAccountInfo(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            var data = result.AccountData;
            Assert.IsNotNull(data);

            Assert.AreEqual("jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT", data.Account);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "1015.641623" }, data.Balance);
            Assert.AreEqual((uint)5, data.Sequence);
            Assert.AreEqual("8532904D645677129310E09C74A0F4CF371ED569B49B78CC4D0F3F2D3C377986", data.Index);
            Assert.AreEqual(LedgerEntryType.AccountRoot, data.LedgerEntryType);
        }

        [TestMethod]
        public void TestRequestAccountTumsResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountTums.json"));

            MessageResult<AccountTumsResponse> response = null;
            var deferred = new Task(() => { });
            var options = new AccountTumsOptions();
            options.Account = "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT";
            remote.RequestAccountTums(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("62F993679853F86A6193CBFC017F307A60A78B74D1E57C50676D74816B0CCFC6", result.LedgerHash);
            Assert.AreEqual((uint)9646172, result.LedgerIndex);

            Assert.IsNotNull(result.ReceiveCurrencies);
            Assert.AreEqual(1, result.ReceiveCurrencies.Length);
            Assert.AreEqual("CNY", result.ReceiveCurrencies[0]);

            Assert.IsNotNull(result.SendCurrencies);
            Assert.AreEqual(1, result.SendCurrencies.Length);
            Assert.AreEqual("USD", result.SendCurrencies[0]);
        }

        [TestMethod]
        public void TestRequestAccountRelationsResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountRelations.json"));

            MessageResult<AccountRelationsResponse> response = null;
            var deferred = new Task(() => { });
            var options = new AccountRelationsOptions();
            options.Account = "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT";
            remote.RequestAccountRelations(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", result.Account);
            Assert.AreEqual("A7C9D84B7FAD0AE7DA7A835AA74FAAF02CF69042735E32EA13EEB30C00CD972E", result.LedgerHash);
            Assert.AreEqual((uint)9646353, result.LedgerIndex);
            Assert.IsNotNull(result.Lines);
            Assert.AreEqual(3, result.Lines.Length);

            var line0 = result.Lines[0];
            Assert.AreEqual("jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", line0.Account);
            Assert.AreEqual("1.18017", line0.Balance);
            Assert.AreEqual("CNY", line0.Currency);
            Assert.AreEqual("10000000000", line0.Limit);
            Assert.AreEqual("0", line0.LimitPeer);
            Assert.IsTrue(line0.NoSkywell);
            Assert.AreEqual(0, line0.QualityIn);
            Assert.AreEqual(0, line0.QualityOut);

            var line1 = result.Lines[1];
            Assert.AreEqual("CCA", line1.Currency);
            Assert.AreEqual("js7M6x28mYDiZVJJtfJ84ydrv2PthY9W9u", line1.Issuer);
            Assert.AreEqual("0.01", line1.Limit);
            Assert.AreEqual("jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT", line1.LimitPeer);
            Assert.AreEqual(1, line1.RelationType);

            var line2 = result.Lines[2];
            Assert.AreEqual("CCA", line2.Currency);
            Assert.AreEqual("js7M6x28mYDiZVJJtfJ84ydrv2PthY9W9u", line2.Issuer);
            Assert.AreEqual("0.02", line2.Limit);
            Assert.AreEqual("jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT", line2.LimitPeer);
            Assert.AreEqual(3, line2.RelationType);
        }

        [TestMethod]
        public void TestRequestAccountOffersResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountOffers.json"));

            MessageResult<AccountOffersResponse> response = null;
            var deferred = new Task(() => { });
            var options = new AccountOffersOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            remote.RequestAccountOffers(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", result.Account);
            Assert.AreEqual("39210701654A5DCDA1B01EF636B0A4FFA5159342B75F082B81B58E92DDCB24B1", result.LedgerHash);
            Assert.AreEqual((uint)9646438, result.LedgerIndex);
            Assert.IsNotNull(result.Offers);
            Assert.AreEqual(1, result.Offers.Length);

            var offer0 = result.Offers[0];
            Assert.AreEqual(131072, offer0.Flags);
            Assert.AreEqual(16, offer0.Seq);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "0.01" }, offer0.TakerGets);
            Assert.AreEqual(new Amount { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "1" }, offer0.TakerPays);
            Assert.IsTrue(offer0.IsSell);
        }

        [TestMethod]
        public void TestRequestAccountTxResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestAccountTx.json"));

            MessageResult<AccountTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new AccountTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            remote.RequestAccountTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", result.Account);
            Assert.AreEqual((uint)1439816, result.LedgerIndexMin);
            Assert.AreEqual((uint)1667467, result.LedgerIndexMax);
            Assert.IsNotNull(result.Transactions);
            Assert.AreEqual(3, result.Transactions.Length);

            var tx0 = result.Transactions[0] as CallContractTxResult;
            Assert.IsNotNull(tx0);
            Assert.AreEqual("jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", tx0.Destination);
            Assert.AreEqual(ContractMethod.Call, tx0.Method);
            Assert.AreEqual("jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", tx0.Params[0]);
            Assert.AreEqual("foo", tx0.Foo);

            var tx1 = result.Transactions[1] as DeployContractTxResult;
            Assert.IsNotNull(tx1);
            Assert.AreEqual(ContractMethod.Deploy, tx1.Method );
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", tx1.Params[0]);

            var tx2 = result.Transactions[2] as ReceivedTxResult;
            Assert.IsNotNull(tx2);
            Assert.AreEqual("jHb9CJAWyB4jr91VRWn96DkukG4bwdtyTh", tx2.CounterParty);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "10000" }, tx2.Amount);
        }

        [TestMethod]
        public void TestRequestOrderBookResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestOrderBook.json"));

            MessageResult<OrderBookResponse> response = null;
            var deferred = new Task(() => { });
            var options = new OrderBookOptions();
            options.Gets = Amount.SWT();
            options.Pays = new Amount ("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or");
            remote.RequestOrderBook(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Offers.Length);

            var offer0 = result.Offers[0];
            Assert.AreEqual("jJpfha3V2W8cHuB246Nm1EscFPyyhy5NiL", offer0.Account);
            Assert.AreEqual(5261, offer0.Sequence);

            var offer9 = result.Offers[9];
            Assert.AreEqual("jJ6NTADAhFJJMKMq8bdLA84YixXX31TkaX", offer9.Account);
            Assert.IsFalse(offer9.IsSell);
        }

        [TestMethod]
        public void TestRequestPathFindResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "RequestPathFind.json"));

            MessageResult<PathFindResponse> response = null;
            var deferred = new Task(() => { });
            var options = new PathFindOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Destination = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn";
            options.Amount = new Amount { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "0.01" };
            remote.RequestPathFind(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", result.Source);
            Assert.AreEqual("jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", result.Destination);
            Assert.AreEqual(new Amount { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "0.001" }, result.Amount);
            Assert.AreEqual(1, result.Alternatives.Length);

            var path0 = result.Alternatives[0];
            Assert.IsNotNull(path0.Key);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "0.024661" }, path0.Choice);
        }

        [TestMethod]
        public void TestBuildPaymentTxResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "BuildPaymentTx.json"));

            MessageResult<PaymentTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new PaymentTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.To = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            options.Amount = new Amount { Currency="SWT", Issuer="", Value="0.5" };
            remote.BuildPaymentTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual("j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", txJson.Destination);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "0.5" }, txJson.Amount);
            Assert.AreEqual("7B6ECFA43073AE247EC1228DD27502671438395909D0C060C0B53D24685050E2", txJson.Hash);
            Assert.AreEqual("haha", txJson.Memos[0].Memo.MemoData);
            Assert.AreEqual(TransactionType.Payment, txJson.TransactionType);
        }

        [TestMethod]
        public void TestDeployContractTxResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "DeployContractTx.json"));

            MessageResult<DeployContractTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new DeployContractTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Payload = "...";
            options.Amount = 30;
            remote.DeployContractTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Args[0].Arg.Parameter);
            Assert.AreEqual("haha", txJson.Memos[0].Memo.MemoData);
            Assert.AreEqual(0, txJson.Method);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "30" }, txJson.Amount);
            Assert.AreEqual(TransactionType.ConfigContract, txJson.TransactionType);
        }

        [TestMethod]
        public void TestCallContractTxResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "CallContractTx.json"));

            MessageResult<CallContractTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new CallContractTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Destination = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn";
            options.Foo = "foo";
            options.Params = new[] { "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr" };
            remote.CallContractTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Args[0].Arg.Parameter);
            Assert.AreEqual("haha", txJson.Memos[0].Memo.MemoData);
            Assert.AreEqual(1, txJson.Method);
            Assert.AreEqual(TransactionType.ConfigContract, txJson.TransactionType);
        }

        [TestMethod]
        public void TestBuildOfferCreateTxResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "BuildOfferCreateTx.json"));

            MessageResult<OfferCreateTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new OfferCreateTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.TakerPays = new Amount { Currency = "CNY", Issuer = "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", Value = "1" };
            options.TakerGets = new Amount { Currency = "SWT", Issuer = "", Value = "0.02" };
            remote.BuildOfferCreateTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual(new Amount { Currency = "SWT", Issuer = "", Value = "0.02" }, txJson.TakerGets);
            Assert.AreEqual(new Amount { Currency = "CNY", Issuer = "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", Value = "1" }, txJson.TakerPays);
            Assert.AreEqual(9, txJson.Sequence);
            Assert.AreEqual(TransactionType.OfferCreate, txJson.TransactionType);
        }

        [TestMethod]
        public void TestBuildOfferCancelTxResponse()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "BuildOfferCancelTx.json"));

            MessageResult<OfferCancelTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new OfferCancelTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Sequence = 8;
            remote.BuildOfferCancelTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual(12, txJson.Sequence);
            Assert.AreEqual(8, txJson.OfferSequence);
            Assert.AreEqual(TransactionType.OfferCancel, txJson.TransactionType);
        }

        [TestMethod]
        public void TestBuildRelationTxResponse_TrustSet()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "BuildRelationTx_TrustSet.json"));

            MessageResult<RelationTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new RelationTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Type = RelationType.Trust;
            options.Limit = new Amount { Currency = "CCA", Issuer = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", Value = "0.01" };
            remote.BuildRelationTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual(new Amount { Currency = "CCA", Issuer = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", Value = "0.01" }, txJson.LimitAmount);
            Assert.AreEqual(TransactionType.TrustSet, txJson.TransactionType);
        }

        [TestMethod]
        public void TestBuildRelationTxResponse_RelationSet()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "BuildRelationTx_RelationSet.json"));

            MessageResult<RelationTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new RelationTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Target = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn";
            options.Type = RelationType.Authorize;
            options.Limit = new Amount { Currency = "CCA", Issuer = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", Value = "0.01" };
            remote.BuildRelationTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual("jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", txJson.Target);
            Assert.AreEqual(new Amount { Currency = "CCA", Issuer = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", Value = "0.01" }, txJson.LimitAmount);
            Assert.AreEqual(TransactionType.RelationSet, txJson.TransactionType);
            Assert.AreEqual(RelationType.Authorize, txJson.RelationType);
        }

        [TestMethod]
        public void TestBuildRelationTxResponse_RelationDel()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "BuildRelationTx_RelationDel.json"));

            MessageResult<RelationTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new RelationTxOptions();
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.Target = "jDUjqoDZLhzx4DCf6pvSivjkjgtRESY62c";
            options.Type = RelationType.Unfreeze;
            options.Limit = new Amount { Currency = "CCA", Issuer = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", Value = "0.01" };
            remote.BuildRelationTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tecNO_DST", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual("jDUjqoDZLhzx4DCf6pvSivjkjgtRESY62c", txJson.Target);
            Assert.AreEqual(new Amount { Currency = "CCA", Issuer = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn", Value = "0.01" }, txJson.LimitAmount);
            Assert.AreEqual(TransactionType.RelationDel, txJson.TransactionType);
            Assert.AreEqual(RelationType.Unfreeze, txJson.RelationType);
        }

        [TestMethod]
        public void TestBuildAccountSetTxResponse_Property()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "BuildAccountSetTx_Property.json"));

            MessageResult<AccountSetTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new AccountSetTxOptions();
            options.Type = AccountSetType.Property;
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.SetFlag = SetClearFlags.RequireAuth;
            remote.BuildAccountSetTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tecOWNERS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual(SetClearFlags.RequireAuth, txJson.SetFlag.Value);
            Assert.AreEqual(TransactionType.AccountSet, txJson.TransactionType);
        }

        [TestMethod]
        public void TestBuildAccountSetTxResponse_Delegate()
        {
            var remote = new Remote("");
            remote.SetMockServer(new MockServer(remote, "BuildAccountSetTx_Delegate.json"));

            MessageResult<AccountSetTxResponse> response = null;
            var deferred = new Task(() => { });
            var options = new AccountSetTxOptions();
            options.Type = AccountSetType.Delegate;
            options.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            options.DelegateKey = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            remote.BuildAccountSetTx(options).Submit(r =>
            {
                response = r;
                deferred.Start();
            });

            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsNotNull(response);
            var result = response.Result;
            Assert.IsNotNull(result);

            Assert.AreEqual("tesSUCCESS", result.EngineResult);
            var txJson = result.TxJson;

            Assert.IsNotNull(txJson);
            Assert.AreEqual("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", txJson.Account);
            Assert.AreEqual("j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", txJson.RegularKey);
            Assert.AreEqual(TransactionType.SetRegularKey, txJson.TransactionType);
        }
    }
}
