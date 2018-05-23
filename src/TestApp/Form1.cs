using JingTum.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        private Remote _remote;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            UpdateTabsEnabled(false);

            InitRequestLedgerOptions();
            InitRequestTxOptions();
            InitRequestAccountInfoOptions();
            InitRequestAccountTumsOptions();
            InitRequestAccountRelationsOptions();
            InitRequestAccountOffersOptions();
            InitRequestAccountTxOptions();
            InitRequestOrderBookOptions();

            InitRequestPathFindOptions();

            InitBuildPaymentTxOptions();
            InitBuildRelationTxOptions();
            InitBuildAccountSetTxOptions();
            InitBuildOfferCreateTxOptions();
            InitBuildOfferCancelTxOptions();
            InitDeployContractTxOptions();
            InitCallContractTxOptions();
            InitBuildSignTxOptions();
        }

        private void ClearResult()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new Action(ClearResultImp));
            }
            else
            {
                ClearResultImp();
            }
        }

        private void ClearResultImp()
        {
            txtException.Text = string.Empty;
            jtvResultMessage.ShowJson("{}");
            ovResult.ShowObject(null);
        }

        private void ShowResult<T>(MessageResult<T> result)
        {
            ClearResult();

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<MessageResult<T>>(ShowResultImp), result);
            }
            else
            {
                ShowResultImp(result);
            }
        }

        private void ShowResultImp<T>(MessageResult<T> result)
        {
            if (result.Exception != null)
            {
                txtException.Text = result.Exception.ToString();
                var ex = result.Exception as ResponseException;
                if (ex != null && ex.Request!=null)
                {
                    txtException.Text += "\r\nRequest: " + ex.Request;
                }
            }
            else
            {
                txtException.Text = "";
            }
            
            jtvResultMessage.ShowJson(result.Message);
            ovResult.ShowObject(result.Result);
        }

        private void UpdateTabsEnabled(bool enabled)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<bool>(UpdateTabsEnabledImp), enabled);
            }
            else
            {
                UpdateTabsEnabledImp(enabled);
            }
        }

        private void UpdateTabsEnabledImp(bool enabled)
        {
            for(int i = 1; i < tabControlRemote.TabCount; i++)
            {
                var tab = tabControlRemote.TabPages[i];
                tab.Enabled = enabled;
            }
        }

        private void btnWalletGenerate_Click(object sender, EventArgs e)
        {
            var wallet = Wallet.Generate();
            txtAddress.Text = wallet.Address;
            txtSecret.Text = wallet.Secret;
        }

        private void btnFromSecret_Click(object sender, EventArgs e)
        {
            try
            {
                var wallet = Wallet.FromSecret(txtSecret.Text);
                txtAddress.Text = wallet.Address;
                txtSecret.Text = wallet.Secret;
            }
            catch(Exception ex)
            {
                txtAddress.Text = "";
                txtException.Text = ex.ToString();
            }
        }

        private void btnRequestServerInfo_Click(object sender, EventArgs e)
        {
            ClearResult();
            _remote.RequestServerInfo().Submit(ShowResult);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ClearResult();

            DisconnectImp();

            _remote = new JingTum.Lib.Remote(txtServerUrl.Text, chkLocalSign.Checked);
            _remote.ServerStatusChanged += _remote_ServerStatusChanged;
            _remote.Connect(result =>
            {
                ShowResult(result);
                if(result != null)
                {
                    UpdateTabsEnabled(true);
                }
            });
        }

        private void _remote_ServerStatusChanged(object sender, ServerStatusEventArgs e)
        {
            UpdateTabsEnabled(_remote.IsConnected);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectImp();
        }

        private void DisconnectImp()
        {
            if (_remote != null)
            {
                _remote.Disconnect();
            }

            ClearResult();
            UpdateTabsEnabled(false);
        }

        private void btnRequestLedgerClosed_Click(object sender, EventArgs e)
        {
            ClearResult();
            _remote.RequestLedgerClosed().Submit(ShowResult);
        }

        private void btnResetRequestLedgerOptions_Click(object sender, EventArgs e)
        {
            InitRequestLedgerOptions();
        }

        private void InitRequestLedgerOptions()
        {
            var options = new LedgerOptions();
            options.LedgerIndex = 8488670;
            pgRequestLedgerOptions.SelectedObject = options;
        }

        private void btnRequestLedger_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestLedgerOptions.SelectedObject as LedgerOptions;
            _remote.RequestLedger(options).Submit(ShowResult);
        }

        private void btnResetRequestTxOptions_Click(object sender, EventArgs e)
        {
            InitRequestTxOptions();
        }

        private void btnRequestTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestTxOptions.SelectedObject as TxOptions;
            _remote.RequestTx(options).Submit(ShowResult);
        }

        private void InitRequestTxOptions()
        {
            var options = new TxOptions();
            options.Hash = "084C7823C318B8921A362E39C67A6FB15ADA5BCCD0C7E9A3B13485B1EF2A4313";
            pgRequestTxOptions.SelectedObject = options;
        }

        private void btnResetRequestAccountInfoOptions_Click(object sender, EventArgs e)
        {
            InitRequestAccountInfoOptions();
        }

        private void btnRequestAccountInfo_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestAccountInfoOptions.SelectedObject as AccountInfoOptions;
            _remote.RequestAccountInfo(options).Submit(ShowResult);
        }

        private void InitRequestAccountInfoOptions()
        {
            var options = new AccountInfoOptions();
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.Ledger = new LedgerSettings();
            pgRequestAccountInfoOptions.SelectedObject = options;
        }

        private void btnResetRequestAccountTumsOptions_Click(object sender, EventArgs e)
        {
            InitRequestAccountTumsOptions();
        }

        private void btnRequestAccountTums_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestAccountTumsOptions.SelectedObject as AccountTumsOptions;
            _remote.RequestAccountTums(options).Submit(ShowResult);
        }

        private void InitRequestAccountTumsOptions()
        {
            var options = new AccountTumsOptions();
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.Ledger = new LedgerSettings();
            pgRequestAccountTumsOptions.SelectedObject = options;
        }

        private void btnResetRequestAccountRelationsOptions_Click(object sender, EventArgs e)
        {
            InitRequestAccountRelationsOptions();
        }

        private void btnRequestAccountRelations_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestAccountRelationsOptions.SelectedObject as AccountRelationsOptions;
            _remote.RequestAccountRelations(options).Submit(ShowResult);
        }

        private void InitRequestAccountRelationsOptions()
        {
            var options = new AccountRelationsOptions();
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.Ledger = new LedgerSettings();
            pgRequestAccountRelationsOptions.SelectedObject = options;
        }

        private void btnResetRequestAccountOffersOptions_Click(object sender, EventArgs e)
        {
            InitRequestAccountOffersOptions();
        }

        private void btnRequestAccountOffers_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestAccountOffersOptions.SelectedObject as AccountOffersOptions;
            _remote.RequestAccountOffers(options).Submit(ShowResult);
        }

        private void InitRequestAccountOffersOptions()
        {
            var options = new AccountOffersOptions();
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.Ledger = new LedgerSettings();
            pgRequestAccountOffersOptions.SelectedObject = options;
        }

        private void btnResetRequestAccountTxOptions_Click(object sender, EventArgs e)
        {
            InitRequestAccountTxOptions();
        }

        private void btnRequestAccountTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestAccountTxOptions.SelectedObject as AccountTxOptions;
            _remote.RequestAccountTx(options).Submit(ShowResult);
        }

        private void InitRequestAccountTxOptions()
        {
            var options = new AccountTxOptions();
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.Ledger = new LedgerSettings();
            options.Marker = new MarkerSettings();
            pgRequestAccountTxOptions.SelectedObject = options;
        }

        private void btnResetRequestOrderBookOptions_Click(object sender, EventArgs e)
        {
            InitRequestOrderBookOptions();
        }

        private void btnRequestOrderBook_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestOrderBookOptions.SelectedObject as OrderBookOptions;
            _remote.RequestOrderBook(options).Submit(ShowResult);
        }

        private void InitRequestOrderBookOptions()
        {
            var options = new OrderBookOptions();
            options.Gets = AmountSettings.SWT();
            options.Pays = new AmountSettings("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or");
            pgRequestOrderBookOptions.SelectedObject = options;
        }


        private void ApplyTxSettings<T>(TxSettings settings, Transaction<T> tx)
            where T : GeneralTxResponse
        {
            tx.SetSecret(settings.Secret);
            if (!string.IsNullOrEmpty(settings.Memo))
            {
                tx.AddMemo(settings.Memo);
            }
            if (settings.Fee != null)
            {
                tx.SetFee(settings.Fee.Value);
            }
            if (!string.IsNullOrEmpty(settings.Path))
            {
                tx.SetPath(settings.Path);
            }
            if (settings.TransferRate != null)
            {
                tx.SetTransferRate(settings.TransferRate.Value);
            }
            if (settings.Flags != null)
            {
                tx.SetFlags(settings.Flags.Value);
            }
            if (settings.SendMax != null && settings.SendMax.Currency!=null)
            {
                tx.SetSendMax(settings.SendMax);
            }
        }

        private void btnResetBuildPaymentTxOptions_Click(object sender, EventArgs e)
        {
            InitBuildPaymentTxOptions();
        }

        private void btnBuildPaymentTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgBuildPaymentTxOptions.SelectedObject as PaymentTxOptions;
            var tx = _remote.BuildPaymentTx(options);
            var settings = pgBuildPaymentTxSettings.SelectedObject as TxSettings;
            ApplyTxSettings(settings, tx);
            tx.Submit(ShowResult);
        }

        private void InitBuildPaymentTxOptions()
        {
            var options = new PaymentTxOptions();
            options.From = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.To = "jDUjqoDZLhzx4DCf6pvSivjkjgtRESY62c";
            options.Amount = new AmountSettings("SWT", "", "0.5");
            pgBuildPaymentTxOptions.SelectedObject = options;

            var settings = new TxSettings();
            settings.Secret = "sn37nYrQ6KPJvTFmaBYokS3FjXUWd";
            settings.SendMax = new AmountSettings();
            pgBuildPaymentTxSettings.SelectedObject = settings;
        }

        private void btnResetBuildRelationOptions_Click(object sender, EventArgs e)
        {
            InitBuildRelationTxOptions();
        }

        private void btnBuildRelationTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgBuildRelationTxOptions.SelectedObject as RelationTxOptions;
            var tx = _remote.BuildRelationTx(options);
            var settings = pgBuildRelationTxSettings.SelectedObject as TxSettings;
            ApplyTxSettings(settings, tx);
            tx.Submit(ShowResult);
        }

        private void InitBuildRelationTxOptions()
        {
            var options = new RelationTxOptions();
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.Target = "jDUjqoDZLhzx4DCf6pvSivjkjgtRESY62c";
            options.Limit = new AmountSettings { Currency = "CCA", Value = "0.01", Issuer = "js7M6x28mYDiZVJJtfJ84ydrv2PthY9W9u" };
            pgBuildRelationTxOptions.SelectedObject = options;

            var settings = new TxSettings();
            settings.Secret = "sn37nYrQ6KPJvTFmaBYokS3FjXUWd";
            settings.SendMax = new AmountSettings();
            pgBuildRelationTxSettings.SelectedObject = settings;
        }

        private void btnResetBuildAccountSetTxOptions_Click(object sender, EventArgs e)
        {
            InitBuildAccountSetTxOptions();
        }

        private void btnBuildAccountSetTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgBuildAccountSetTxOptions.SelectedObject as AccountSetTxOptions;
            var tx = _remote.BuildAccountSetTx(options);
            var settings = pgBuildAccountSetTxSettings.SelectedObject as TxSettings;
            ApplyTxSettings(settings, tx);
            tx.Submit(ShowResult);
        }

        private void InitBuildAccountSetTxOptions()
        {
            var options = new AccountSetTxOptions();
            options.Type = AccountSetType.Property;
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            pgBuildAccountSetTxOptions.SelectedObject = options;

            var settings = new TxSettings();
            settings.Secret = "sn37nYrQ6KPJvTFmaBYokS3FjXUWd";
            settings.SendMax = new AmountSettings();
            pgBuildAccountSetTxSettings.SelectedObject = settings;
        }

        private void btnResetBuildOfferCreateTxOptions_Click(object sender, EventArgs e)
        {
            InitBuildOfferCreateTxOptions();
        }

        private void btnBuildOfferCreateTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgBuildOfferCreateTxOptions.SelectedObject as OfferCreateTxOptions;
            var tx = _remote.BuildOfferCreateTx(options);
            var settings = pgBuildOfferCreateTxSettings.SelectedObject as TxSettings;
            ApplyTxSettings(settings, tx);
            tx.Submit(ShowResult);
        }

        private void InitBuildOfferCreateTxOptions()
        {
            var options = new OfferCreateTxOptions();
            options.Type = OfferType.Sell;
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.TakerGets = new AmountSettings { Currency = "CNY", Issuer = "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS", Value = "0.01" };
            options.TakerPays = new AmountSettings { Currency = "SWT", Issuer = "", Value = "1" };
            pgBuildOfferCreateTxOptions.SelectedObject = options;

            var settings = new TxSettings();
            settings.Secret = "sn37nYrQ6KPJvTFmaBYokS3FjXUWd";
            settings.SendMax = new AmountSettings();
            pgBuildOfferCreateTxSettings.SelectedObject = settings;
        }

        private void btnResetBuildOfferCancelTxOptions_Click(object sender, EventArgs e)
        {
            InitBuildOfferCancelTxOptions();
        }

        private void btnBuildOfferCancelTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgBuildOfferCancelTxOptions.SelectedObject as OfferCancelTxOptions;
            var tx = _remote.BuildOfferCancelTx(options);
            var settings = pgBuildOfferCancelTxSettings.SelectedObject as TxSettings;
            ApplyTxSettings(settings, tx);
            tx.Submit(ShowResult);
        }

        private void InitBuildOfferCancelTxOptions()
        {
            var options = new OfferCancelTxOptions();
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.Sequence = 688;
            pgBuildOfferCancelTxOptions.SelectedObject = options;

            var settings = new TxSettings();
            settings.Secret = "sn37nYrQ6KPJvTFmaBYokS3FjXUWd";
            settings.SendMax = new AmountSettings();
            pgBuildOfferCancelTxSettings.SelectedObject = settings;
        }

        private void btnDeployContractTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgDeployContractTxOptions.SelectedObject as DeployContractTxOptions;
            var tx = _remote.DeployContractTx(options);
            var settings = pgDeployContractTxSettings.SelectedObject as TxSettings;
            ApplyTxSettings(settings, tx);
            tx.Submit(ShowResult);
        }

        private void btnResetDeployContractTxOptions_Click(object sender, EventArgs e)
        {
            InitDeployContractTxOptions();
        }

        private void InitDeployContractTxOptions()
        {
            var options = new DeployContractTxOptions();
            options.Account = "jHb9CJAWyB4jr91VRWn96DkukG4bwdtyTh";
            options.Amount = 30;
            options.Payload = "result={}; function Init(t) result=scGetAccountInfo(t) return result end; function foo(t) a={} result=scGetAccountInfo(t) return result end;";
            options.Params = new string[] { "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT" };
            pgDeployContractTxOptions.SelectedObject = options;

            var settings = new TxSettings();
            settings.Secret = "snoPBjXtMeMyMHUVTgbuqAfg1SUTb";
            settings.SendMax = new AmountSettings();
            pgDeployContractTxSettings.SelectedObject = settings;
        }

        private void btnCallContractTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgCallContractTxOptions.SelectedObject as CallContractTxOptions;
            var tx = _remote.CallContractTx(options);
            var settings = pgCallContractTxSettings.SelectedObject as TxSettings;
            ApplyTxSettings(settings, tx);
            tx.Submit(ShowResult);
        }

        private void btnResetCallContractTxOptions_Click(object sender, EventArgs e)
        {
            InitCallContractTxOptions();
        }

        private void InitCallContractTxOptions()
        {
            var options = new CallContractTxOptions();
            options.Account = "jHb9CJAWyB4jr91VRWn96DkukG4bwdtyTh";
            options.Destination = "jBjprZbEjNu4NYD68HCMqQ7B5n8mqGZ4";
            options.Foo = "foo";
            options.Params = new string[] { "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT" };
            pgCallContractTxOptions.SelectedObject = options;

            var settings = new TxSettings();
            settings.Secret = "snoPBjXtMeMyMHUVTgbuqAfg1SUTb";
            settings.SendMax = new AmountSettings();
            pgCallContractTxSettings.SelectedObject = settings;
        }

        private void btnBuildSignTx_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgBuildSignTxOptions.SelectedObject as SignTxOptions;
            var tx = _remote.BuildSignTx(options);
            tx.Submit(ShowResult);
        }

        private void btnResetBuildSignTxOptions_Click(object sender, EventArgs e)
        {
            InitBuildSignTxOptions();
        }

        private void InitBuildSignTxOptions()
        {
            var options = new SignTxOptions();
            pgBuildSignTxOptions.SelectedObject = options;
        }

        private void btnResetRequestPathFindOptions_Click(object sender, EventArgs e)
        {
            InitRequestPathFindOptions();
        }

        private void btnRequestPathFind_Click(object sender, EventArgs e)
        {
            ClearResult();
            var options = pgRequestPathFindOptions.SelectedObject as PathFindOptions;
            _remote.RequestPathFind(options).Submit(ShowResult);
        }

        private void InitRequestPathFindOptions()
        {
            var options = new PathFindOptions();
            options.Account = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            options.Destination = "jDUjqoDZLhzx4DCf6pvSivjkjgtRESY62c";
            options.Amount = new AmountSettings { Currency = "SWT", Issuer = "", Value = "0.001" };
            pgRequestPathFindOptions.SelectedObject = options;
        }

        private void btnOnTransactions_Click(object sender, EventArgs e)
        {
            _remote.Transactions += OnTransactions;
            lblTransactionsIndex.Text = "On";
        }

        private void btnUnTransactions_Click(object sender, EventArgs e)
        {
            _remote.Transactions -= OnTransactions;
            lblTransactionsIndex.Text = "Off";
        }

        private int _transactionsIndex = 0;
        private void OnTransactions(object sender, TransactionsEventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<TransactionsEventArgs>(UpdateTransactionsImp), args);
            }
            else
            {
                UpdateTransactionsImp(args);
            }
        }

        private void UpdateTransactionsImp(TransactionsEventArgs args)
        {
            lblTransactionsIndex.Text = (_transactionsIndex++).ToString();
            if (!chkRefreshTransactions.Checked) return;

            jtvTransactionsMessage.ShowJson(args.Message);
            ovTransactionsMessage.ShowObject(args.Result);
        }

        private void btnOnLedgerClosed_Click(object sender, EventArgs e)
        {
            _remote.LedgerClosed += OnLedgerClosed;
            lblLedgerClosedIndex.Text = "On";
        }

        private void btnUnLedgerClosed_Click(object sender, EventArgs e)
        {
            _remote.LedgerClosed -= OnLedgerClosed;
            lblLedgerClosedIndex.Text = "Off";
        }

        private int _ledgerClosedIndex = 0;
        private void OnLedgerClosed(object sender, LedgerClosedEventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<LedgerClosedEventArgs>(UpdateLedgerClosedImp), args);
            }
            else
            {
                UpdateLedgerClosedImp(args);
            }
        }

        private void UpdateLedgerClosedImp(LedgerClosedEventArgs args)
        {
            lblLedgerClosedIndex.Text = (_ledgerClosedIndex++).ToString();
            if (!chkRefreshLedgerClosed.Checked) return;

            jtvLedgerClosedMessage.ShowJson(args.Message);
            ovLedgerClosedMessage.ShowObject(args.LedgerClosed);
        }

        private void btnOrderBooksStub_Click(object sender, EventArgs e)
        {
            var stub = _remote.CreateOrderBooksStub();
            var swtAmount = new Amount { Currency = "SWT" };
            var cnyAmount = new Amount { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" };
            stub.RegisterListener(swtAmount, cnyAmount, OnSwtCnyOrderBook);
            stub.RegisterListener(cnyAmount, swtAmount, OnCnySwtOrderBook);
        }

        private void OnSwtCnyOrderBook(MessageResult<TxResult> result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<MessageResult<TxResult>>(UpdateSwtCnyImpl), result);
            }
            else
            {
                UpdateSwtCnyImpl(result);
            }
        }

        private int _swtCnyIndex = 0;
        private void UpdateSwtCnyImpl(MessageResult<TxResult> result)
        {
            lblSwtCnyIndex.Text = (_swtCnyIndex++).ToString();
            if (!chkRefreshOrderBook.Checked) return;
            jtvSwtCny.Text = result.Message;
            ovSwtCny.ShowObject(result.Result);
        }

        private void OnCnySwtOrderBook(MessageResult<TxResult> result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<MessageResult<TxResult>>(UpdateCnySwtImpl), result);
            }
            else
            {
                UpdateCnySwtImpl(result);
            }
        }

        private int _cnySwtIndex = 0;
        private void UpdateCnySwtImpl(MessageResult<TxResult> result)
        {
            lblCnySwtIndex.Text = (_cnySwtIndex++).ToString();
            if (!chkRefreshOrderBook.Checked) return;
            jtvCnySwt.Text = result.Message;
            ovCnySwt.ShowObject(result.Result);
        }

        private void btnCreateAccountStub_Click(object sender, EventArgs e)
        {
            var stub = _remote.CreateAccountStub();
            stub.Subscribe(txtAccountStubAccount.Text, UpdateAccountStub);
        }

        private void UpdateAccountStub(MessageResult<TxResult> result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<MessageResult<TxResult>>(UpdateAccountStubImpl), result);
            }
            else
            {
                UpdateAccountStubImpl(result);
            }
        }

        private int _accountStubIndex = 0;
        private void UpdateAccountStubImpl(MessageResult<TxResult> result)
        {
            lblAccountStubIndex.Text = (_accountStubIndex++).ToString();
            if (!chkRefreshAccountStub.Checked) return;
            jtvAccountStub.Text = result.Message;
            ovAccountStub.ShowObject(result.Result);
        }
    }
}
