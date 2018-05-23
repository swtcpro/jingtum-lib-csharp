namespace TestApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.jtvResultMessage = new TestApp.JsonViewer();
            this.txtException = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlRemote = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnWalletGenerate = new System.Windows.Forms.Button();
            this.btnFromSecret = new System.Windows.Forms.Button();
            this.txtServerUrlsList = new System.Windows.Forms.TextBox();
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.chkLocalSign = new System.Windows.Forms.CheckBox();
            this.lblServerUrl = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabRequests = new System.Windows.Forms.TabPage();
            this.tabControlRequests = new System.Windows.Forms.TabControl();
            this.tabRequestServerInfo = new System.Windows.Forms.TabPage();
            this.btnRequestServerInfo = new System.Windows.Forms.Button();
            this.tabRequestLedgerClosed = new System.Windows.Forms.TabPage();
            this.btnRequestLedgerClosed = new System.Windows.Forms.Button();
            this.tabRequestLedger = new System.Windows.Forms.TabPage();
            this.btnResetRequestLedgerOptions = new System.Windows.Forms.Button();
            this.pgRequestLedgerOptions = new System.Windows.Forms.PropertyGrid();
            this.btnRequestLedger = new System.Windows.Forms.Button();
            this.tabRequestTx = new System.Windows.Forms.TabPage();
            this.btnResetRequestTxOptions = new System.Windows.Forms.Button();
            this.btnRequestTx = new System.Windows.Forms.Button();
            this.pgRequestTxOptions = new System.Windows.Forms.PropertyGrid();
            this.tabRequestAccountInfo = new System.Windows.Forms.TabPage();
            this.pgRequestAccountInfoOptions = new System.Windows.Forms.PropertyGrid();
            this.btnResetRequestAccountInfoOptions = new System.Windows.Forms.Button();
            this.btnRequestAccountInfo = new System.Windows.Forms.Button();
            this.tabRequestAccountTums = new System.Windows.Forms.TabPage();
            this.btnResetRequestAccountTumsOptions = new System.Windows.Forms.Button();
            this.btnRequestAccountTums = new System.Windows.Forms.Button();
            this.pgRequestAccountTumsOptions = new System.Windows.Forms.PropertyGrid();
            this.tabRequestAccountRelations = new System.Windows.Forms.TabPage();
            this.pgRequestAccountRelationsOptions = new System.Windows.Forms.PropertyGrid();
            this.btnResetRequestAccountRelationsOptions = new System.Windows.Forms.Button();
            this.btnRequestAccountRelations = new System.Windows.Forms.Button();
            this.tabRequestAccountOffers = new System.Windows.Forms.TabPage();
            this.btnResetRequestAccountOffersOptions = new System.Windows.Forms.Button();
            this.btnRequestAccountOffers = new System.Windows.Forms.Button();
            this.pgRequestAccountOffersOptions = new System.Windows.Forms.PropertyGrid();
            this.tabRequestAccountTx = new System.Windows.Forms.TabPage();
            this.pgRequestAccountTxOptions = new System.Windows.Forms.PropertyGrid();
            this.btnResetRequestAccountTxOptions = new System.Windows.Forms.Button();
            this.btnRequestAccountTx = new System.Windows.Forms.Button();
            this.tabRequestOrderBook = new System.Windows.Forms.TabPage();
            this.btnResetRequestOrderBookOptions = new System.Windows.Forms.Button();
            this.btnRequestOrderBook = new System.Windows.Forms.Button();
            this.pgRequestOrderBookOptions = new System.Windows.Forms.PropertyGrid();
            this.tabPathFindRequest = new System.Windows.Forms.TabPage();
            this.tabControlPathFindRequest = new System.Windows.Forms.TabControl();
            this.tabRequestPathFind = new System.Windows.Forms.TabPage();
            this.pgRequestPathFindOptions = new System.Windows.Forms.PropertyGrid();
            this.btnResetRequestPathFindOptions = new System.Windows.Forms.Button();
            this.btnRequestPathFind = new System.Windows.Forms.Button();
            this.tabTransactions = new System.Windows.Forms.TabPage();
            this.tabControlTransactions = new System.Windows.Forms.TabControl();
            this.tabBuildPaymentTx = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.pgBuildPaymentTxOptions = new System.Windows.Forms.PropertyGrid();
            this.pgBuildPaymentTxSettings = new System.Windows.Forms.PropertyGrid();
            this.btnResetBuildPaymentTxOptions = new System.Windows.Forms.Button();
            this.btnBuildPaymentTx = new System.Windows.Forms.Button();
            this.tabBuildRelationTx = new System.Windows.Forms.TabPage();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.pgBuildRelationTxOptions = new System.Windows.Forms.PropertyGrid();
            this.pgBuildRelationTxSettings = new System.Windows.Forms.PropertyGrid();
            this.btnResetBuildRelationTxOptions = new System.Windows.Forms.Button();
            this.btnBuildRelationTx = new System.Windows.Forms.Button();
            this.tabBuildAccountSetTx = new System.Windows.Forms.TabPage();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.pgBuildAccountSetTxOptions = new System.Windows.Forms.PropertyGrid();
            this.pgBuildAccountSetTxSettings = new System.Windows.Forms.PropertyGrid();
            this.btnResetBuildAccountSetTxOptions = new System.Windows.Forms.Button();
            this.btnBuildAccountSetTx = new System.Windows.Forms.Button();
            this.tabBuildOfferCreateTx = new System.Windows.Forms.TabPage();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.pgBuildOfferCreateTxOptions = new System.Windows.Forms.PropertyGrid();
            this.pgBuildOfferCreateTxSettings = new System.Windows.Forms.PropertyGrid();
            this.btnResetBuildOfferCreateTxOptions = new System.Windows.Forms.Button();
            this.btnBuildOfferCreateTx = new System.Windows.Forms.Button();
            this.tabBuildOfferCancelTx = new System.Windows.Forms.TabPage();
            this.splitContainer9 = new System.Windows.Forms.SplitContainer();
            this.pgBuildOfferCancelTxOptions = new System.Windows.Forms.PropertyGrid();
            this.pgBuildOfferCancelTxSettings = new System.Windows.Forms.PropertyGrid();
            this.btnResetBuildOfferCancelTxOptions = new System.Windows.Forms.Button();
            this.btnBuildOfferCancelTx = new System.Windows.Forms.Button();
            this.DeployContractTx = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.pgDeployContractTxOptions = new System.Windows.Forms.PropertyGrid();
            this.pgDeployContractTxSettings = new System.Windows.Forms.PropertyGrid();
            this.btnResetDeployContractTxOptions = new System.Windows.Forms.Button();
            this.btnDeployContractTx = new System.Windows.Forms.Button();
            this.tabCallContractTx = new System.Windows.Forms.TabPage();
            this.splitContainer12 = new System.Windows.Forms.SplitContainer();
            this.pgCallContractTxOptions = new System.Windows.Forms.PropertyGrid();
            this.pgCallContractTxSettings = new System.Windows.Forms.PropertyGrid();
            this.btnResetCallContractTxOptions = new System.Windows.Forms.Button();
            this.btnCallContractTx = new System.Windows.Forms.Button();
            this.tabBuildSignTx = new System.Windows.Forms.TabPage();
            this.splitContainer10 = new System.Windows.Forms.SplitContainer();
            this.pgBuildSignTxOptions = new System.Windows.Forms.PropertyGrid();
            this.btnResetBuildSignTxOptions = new System.Windows.Forms.Button();
            this.btnBuildSignTx = new System.Windows.Forms.Button();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.tabControlEvents = new System.Windows.Forms.TabControl();
            this.tabTransactionsEvent = new System.Windows.Forms.TabPage();
            this.ovTransactionsMessage = new TestApp.ObjectViewer();
            this.jtvTransactionsMessage = new TestApp.JsonViewer();
            this.chkRefreshTransactions = new System.Windows.Forms.CheckBox();
            this.btnOnTransactions = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUnTransactions = new System.Windows.Forms.Button();
            this.lblTransactionsIndex = new System.Windows.Forms.Label();
            this.tabLedgerClosedEvent = new System.Windows.Forms.TabPage();
            this.splitContainer11 = new System.Windows.Forms.SplitContainer();
            this.jtvLedgerClosedMessage = new TestApp.JsonViewer();
            this.ovLedgerClosedMessage = new TestApp.ObjectViewer();
            this.chkRefreshLedgerClosed = new System.Windows.Forms.CheckBox();
            this.btnOnLedgerClosed = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnUnLedgerClosed = new System.Windows.Forms.Button();
            this.lblLedgerClosedIndex = new System.Windows.Forms.Label();
            this.tabOrderBooksStub = new System.Windows.Forms.TabPage();
            this.tabCreateOrderBooksStub = new System.Windows.Forms.TabControl();
            this.tabCNYSWT = new System.Windows.Forms.TabPage();
            this.ovCnySwt = new TestApp.ObjectViewer();
            this.jtvCnySwt = new TestApp.JsonViewer();
            this.lblCnySwtIndex = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabSWTCNY = new System.Windows.Forms.TabPage();
            this.ovSwtCny = new TestApp.ObjectViewer();
            this.jtvSwtCny = new TestApp.JsonViewer();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSwtCnyIndex = new System.Windows.Forms.Label();
            this.chkRefreshOrderBook = new System.Windows.Forms.CheckBox();
            this.btnOrderBooksStub = new System.Windows.Forms.Button();
            this.tabAccountStub = new System.Windows.Forms.TabPage();
            this.ovAccountStub = new TestApp.ObjectViewer();
            this.jtvAccountStub = new TestApp.JsonViewer();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAccountStubIndex = new System.Windows.Forms.Label();
            this.chkRefreshAccountStub = new System.Windows.Forms.CheckBox();
            this.txtAccountStubAccount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCreateAccountStub = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ovResult = new TestApp.ObjectViewer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlRemote.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabRequests.SuspendLayout();
            this.tabControlRequests.SuspendLayout();
            this.tabRequestServerInfo.SuspendLayout();
            this.tabRequestLedgerClosed.SuspendLayout();
            this.tabRequestLedger.SuspendLayout();
            this.tabRequestTx.SuspendLayout();
            this.tabRequestAccountInfo.SuspendLayout();
            this.tabRequestAccountTums.SuspendLayout();
            this.tabRequestAccountRelations.SuspendLayout();
            this.tabRequestAccountOffers.SuspendLayout();
            this.tabRequestAccountTx.SuspendLayout();
            this.tabRequestOrderBook.SuspendLayout();
            this.tabPathFindRequest.SuspendLayout();
            this.tabControlPathFindRequest.SuspendLayout();
            this.tabRequestPathFind.SuspendLayout();
            this.tabTransactions.SuspendLayout();
            this.tabControlTransactions.SuspendLayout();
            this.tabBuildPaymentTx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.tabBuildRelationTx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.tabBuildAccountSetTx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.tabBuildOfferCreateTx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.tabBuildOfferCancelTx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).BeginInit();
            this.splitContainer9.Panel1.SuspendLayout();
            this.splitContainer9.Panel2.SuspendLayout();
            this.splitContainer9.SuspendLayout();
            this.DeployContractTx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabCallContractTx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).BeginInit();
            this.splitContainer12.Panel1.SuspendLayout();
            this.splitContainer12.Panel2.SuspendLayout();
            this.splitContainer12.SuspendLayout();
            this.tabBuildSignTx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).BeginInit();
            this.splitContainer10.Panel1.SuspendLayout();
            this.splitContainer10.SuspendLayout();
            this.tabEvents.SuspendLayout();
            this.tabControlEvents.SuspendLayout();
            this.tabTransactionsEvent.SuspendLayout();
            this.tabLedgerClosedEvent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).BeginInit();
            this.splitContainer11.Panel1.SuspendLayout();
            this.splitContainer11.Panel2.SuspendLayout();
            this.splitContainer11.SuspendLayout();
            this.tabOrderBooksStub.SuspendLayout();
            this.tabCreateOrderBooksStub.SuspendLayout();
            this.tabCNYSWT.SuspendLayout();
            this.tabSWTCNY.SuspendLayout();
            this.tabAccountStub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(3, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 75);
            this.panel1.TabIndex = 1;
            // 
            // jtvResultMessage
            // 
            this.jtvResultMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jtvResultMessage.Location = new System.Drawing.Point(6, 15);
            this.jtvResultMessage.Name = "jtvResultMessage";
            this.jtvResultMessage.Size = new System.Drawing.Size(397, 144);
            this.jtvResultMessage.TabIndex = 0;
            // 
            // txtException
            // 
            this.txtException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtException.Location = new System.Drawing.Point(5, 22);
            this.txtException.Multiline = true;
            this.txtException.Name = "txtException";
            this.txtException.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtException.Size = new System.Drawing.Size(395, 81);
            this.txtException.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlRemote);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(858, 448);
            this.splitContainer1.SplitterDistance = 451;
            this.splitContainer1.TabIndex = 4;
            // 
            // tabControlRemote
            // 
            this.tabControlRemote.Controls.Add(this.tabGeneral);
            this.tabControlRemote.Controls.Add(this.tabRequests);
            this.tabControlRemote.Controls.Add(this.tabPathFindRequest);
            this.tabControlRemote.Controls.Add(this.tabTransactions);
            this.tabControlRemote.Controls.Add(this.tabEvents);
            this.tabControlRemote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRemote.ItemSize = new System.Drawing.Size(54, 24);
            this.tabControlRemote.Location = new System.Drawing.Point(0, 0);
            this.tabControlRemote.Name = "tabControlRemote";
            this.tabControlRemote.SelectedIndex = 0;
            this.tabControlRemote.Size = new System.Drawing.Size(451, 448);
            this.tabControlRemote.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Controls.Add(this.txtServerUrlsList);
            this.tabGeneral.Controls.Add(this.txtServerUrl);
            this.tabGeneral.Controls.Add(this.chkLocalSign);
            this.tabGeneral.Controls.Add(this.lblServerUrl);
            this.tabGeneral.Controls.Add(this.btnDisconnect);
            this.tabGeneral.Controls.Add(this.btnConnect);
            this.tabGeneral.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabGeneral.Location = new System.Drawing.Point(4, 28);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(443, 429);
            this.tabGeneral.TabIndex = 2;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSecret);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.btnWalletGenerate);
            this.groupBox1.Controls.Add(this.btnFromSecret);
            this.groupBox1.Location = new System.Drawing.Point(11, 267);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 139);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wallet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Address:";
            // 
            // txtSecret
            // 
            this.txtSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecret.Location = new System.Drawing.Point(72, 46);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(348, 21);
            this.txtSecret.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Secret:";
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.Location = new System.Drawing.Point(72, 14);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(348, 21);
            this.txtAddress.TabIndex = 13;
            // 
            // btnWalletGenerate
            // 
            this.btnWalletGenerate.Location = new System.Drawing.Point(72, 82);
            this.btnWalletGenerate.Name = "btnWalletGenerate";
            this.btnWalletGenerate.Size = new System.Drawing.Size(118, 41);
            this.btnWalletGenerate.TabIndex = 11;
            this.btnWalletGenerate.Text = "Generate";
            this.btnWalletGenerate.UseVisualStyleBackColor = true;
            this.btnWalletGenerate.Click += new System.EventHandler(this.btnWalletGenerate_Click);
            // 
            // btnFromSecret
            // 
            this.btnFromSecret.Location = new System.Drawing.Point(215, 82);
            this.btnFromSecret.Name = "btnFromSecret";
            this.btnFromSecret.Size = new System.Drawing.Size(124, 41);
            this.btnFromSecret.TabIndex = 12;
            this.btnFromSecret.Text = "From Secret";
            this.btnFromSecret.UseVisualStyleBackColor = true;
            this.btnFromSecret.Click += new System.EventHandler(this.btnFromSecret_Click);
            // 
            // txtServerUrlsList
            // 
            this.txtServerUrlsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerUrlsList.Location = new System.Drawing.Point(172, 100);
            this.txtServerUrlsList.Multiline = true;
            this.txtServerUrlsList.Name = "txtServerUrlsList";
            this.txtServerUrlsList.Size = new System.Drawing.Size(259, 161);
            this.txtServerUrlsList.TabIndex = 8;
            this.txtServerUrlsList.Text = "test 1: ws://123.57.219.57:5020\r\n\r\ntest 2: ws://ts5.jingtum.com:5020\r\n\r\ntest cont" +
    "ract: ws://139.129.194.175:5020\r\n\r\nReal 1: wss://s.jingtum.com:5020\r\n\r\nReal 2: w" +
    "ss://c05.jingtum.com:5020\r\n";
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerUrl.Location = new System.Drawing.Point(11, 35);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.Size = new System.Drawing.Size(420, 21);
            this.txtServerUrl.TabIndex = 2;
            this.txtServerUrl.Text = "ws://139.129.194.175:5020";
            // 
            // chkLocalSign
            // 
            this.chkLocalSign.AutoSize = true;
            this.chkLocalSign.Location = new System.Drawing.Point(11, 63);
            this.chkLocalSign.Name = "chkLocalSign";
            this.chkLocalSign.Size = new System.Drawing.Size(84, 16);
            this.chkLocalSign.TabIndex = 4;
            this.chkLocalSign.Text = "Local Sign";
            this.chkLocalSign.UseVisualStyleBackColor = true;
            // 
            // lblServerUrl
            // 
            this.lblServerUrl.AutoSize = true;
            this.lblServerUrl.Location = new System.Drawing.Point(9, 17);
            this.lblServerUrl.Name = "lblServerUrl";
            this.lblServerUrl.Size = new System.Drawing.Size(71, 12);
            this.lblServerUrl.TabIndex = 3;
            this.lblServerUrl.Text = "Server Url:";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(11, 152);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(155, 48);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(11, 100);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(155, 46);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tabRequests
            // 
            this.tabRequests.Controls.Add(this.tabControlRequests);
            this.tabRequests.Location = new System.Drawing.Point(4, 28);
            this.tabRequests.Name = "tabRequests";
            this.tabRequests.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequests.Size = new System.Drawing.Size(443, 429);
            this.tabRequests.TabIndex = 0;
            this.tabRequests.Text = "Request";
            this.tabRequests.UseVisualStyleBackColor = true;
            // 
            // tabControlRequests
            // 
            this.tabControlRequests.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlRequests.Controls.Add(this.tabRequestServerInfo);
            this.tabControlRequests.Controls.Add(this.tabRequestLedgerClosed);
            this.tabControlRequests.Controls.Add(this.tabRequestLedger);
            this.tabControlRequests.Controls.Add(this.tabRequestTx);
            this.tabControlRequests.Controls.Add(this.tabRequestAccountInfo);
            this.tabControlRequests.Controls.Add(this.tabRequestAccountTums);
            this.tabControlRequests.Controls.Add(this.tabRequestAccountRelations);
            this.tabControlRequests.Controls.Add(this.tabRequestAccountOffers);
            this.tabControlRequests.Controls.Add(this.tabRequestAccountTx);
            this.tabControlRequests.Controls.Add(this.tabRequestOrderBook);
            this.tabControlRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRequests.Location = new System.Drawing.Point(3, 3);
            this.tabControlRequests.Multiline = true;
            this.tabControlRequests.Name = "tabControlRequests";
            this.tabControlRequests.SelectedIndex = 0;
            this.tabControlRequests.Size = new System.Drawing.Size(437, 423);
            this.tabControlRequests.TabIndex = 0;
            // 
            // tabRequestServerInfo
            // 
            this.tabRequestServerInfo.Controls.Add(this.btnRequestServerInfo);
            this.tabRequestServerInfo.Location = new System.Drawing.Point(4, 73);
            this.tabRequestServerInfo.Name = "tabRequestServerInfo";
            this.tabRequestServerInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestServerInfo.Size = new System.Drawing.Size(429, 346);
            this.tabRequestServerInfo.TabIndex = 0;
            this.tabRequestServerInfo.Text = "RequestServerInfo";
            this.tabRequestServerInfo.UseVisualStyleBackColor = true;
            // 
            // btnRequestServerInfo
            // 
            this.btnRequestServerInfo.Location = new System.Drawing.Point(6, 6);
            this.btnRequestServerInfo.Name = "btnRequestServerInfo";
            this.btnRequestServerInfo.Size = new System.Drawing.Size(162, 36);
            this.btnRequestServerInfo.TabIndex = 0;
            this.btnRequestServerInfo.Text = "RequestServerInfo";
            this.btnRequestServerInfo.UseVisualStyleBackColor = true;
            this.btnRequestServerInfo.Click += new System.EventHandler(this.btnRequestServerInfo_Click);
            // 
            // tabRequestLedgerClosed
            // 
            this.tabRequestLedgerClosed.Controls.Add(this.btnRequestLedgerClosed);
            this.tabRequestLedgerClosed.Location = new System.Drawing.Point(4, 73);
            this.tabRequestLedgerClosed.Name = "tabRequestLedgerClosed";
            this.tabRequestLedgerClosed.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestLedgerClosed.Size = new System.Drawing.Size(429, 346);
            this.tabRequestLedgerClosed.TabIndex = 1;
            this.tabRequestLedgerClosed.Text = "RequestLedgerClosed";
            this.tabRequestLedgerClosed.UseVisualStyleBackColor = true;
            // 
            // btnRequestLedgerClosed
            // 
            this.btnRequestLedgerClosed.Location = new System.Drawing.Point(7, 7);
            this.btnRequestLedgerClosed.Name = "btnRequestLedgerClosed";
            this.btnRequestLedgerClosed.Size = new System.Drawing.Size(165, 35);
            this.btnRequestLedgerClosed.TabIndex = 0;
            this.btnRequestLedgerClosed.Text = "RequestLedgerClosed";
            this.btnRequestLedgerClosed.UseVisualStyleBackColor = true;
            this.btnRequestLedgerClosed.Click += new System.EventHandler(this.btnRequestLedgerClosed_Click);
            // 
            // tabRequestLedger
            // 
            this.tabRequestLedger.Controls.Add(this.btnResetRequestLedgerOptions);
            this.tabRequestLedger.Controls.Add(this.pgRequestLedgerOptions);
            this.tabRequestLedger.Controls.Add(this.btnRequestLedger);
            this.tabRequestLedger.Location = new System.Drawing.Point(4, 73);
            this.tabRequestLedger.Name = "tabRequestLedger";
            this.tabRequestLedger.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestLedger.Size = new System.Drawing.Size(429, 346);
            this.tabRequestLedger.TabIndex = 2;
            this.tabRequestLedger.Text = "RequestLedger";
            this.tabRequestLedger.UseVisualStyleBackColor = true;
            // 
            // btnResetRequestLedgerOptions
            // 
            this.btnResetRequestLedgerOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestLedgerOptions.Location = new System.Drawing.Point(295, 23);
            this.btnResetRequestLedgerOptions.Name = "btnResetRequestLedgerOptions";
            this.btnResetRequestLedgerOptions.Size = new System.Drawing.Size(128, 31);
            this.btnResetRequestLedgerOptions.TabIndex = 2;
            this.btnResetRequestLedgerOptions.Text = "Reset";
            this.btnResetRequestLedgerOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestLedgerOptions.Click += new System.EventHandler(this.btnResetRequestLedgerOptions_Click);
            // 
            // pgRequestLedgerOptions
            // 
            this.pgRequestLedgerOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestLedgerOptions.Location = new System.Drawing.Point(6, 60);
            this.pgRequestLedgerOptions.Name = "pgRequestLedgerOptions";
            this.pgRequestLedgerOptions.Size = new System.Drawing.Size(417, 270);
            this.pgRequestLedgerOptions.TabIndex = 1;
            // 
            // btnRequestLedger
            // 
            this.btnRequestLedger.Location = new System.Drawing.Point(6, 6);
            this.btnRequestLedger.Name = "btnRequestLedger";
            this.btnRequestLedger.Size = new System.Drawing.Size(196, 48);
            this.btnRequestLedger.TabIndex = 0;
            this.btnRequestLedger.Text = "RequestLedger";
            this.btnRequestLedger.UseVisualStyleBackColor = true;
            this.btnRequestLedger.Click += new System.EventHandler(this.btnRequestLedger_Click);
            // 
            // tabRequestTx
            // 
            this.tabRequestTx.Controls.Add(this.btnResetRequestTxOptions);
            this.tabRequestTx.Controls.Add(this.btnRequestTx);
            this.tabRequestTx.Controls.Add(this.pgRequestTxOptions);
            this.tabRequestTx.Location = new System.Drawing.Point(4, 73);
            this.tabRequestTx.Name = "tabRequestTx";
            this.tabRequestTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestTx.Size = new System.Drawing.Size(429, 346);
            this.tabRequestTx.TabIndex = 3;
            this.tabRequestTx.Text = "RequestTx";
            this.tabRequestTx.UseVisualStyleBackColor = true;
            // 
            // btnResetRequestTxOptions
            // 
            this.btnResetRequestTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestTxOptions.Location = new System.Drawing.Point(332, 37);
            this.btnResetRequestTxOptions.Name = "btnResetRequestTxOptions";
            this.btnResetRequestTxOptions.Size = new System.Drawing.Size(91, 25);
            this.btnResetRequestTxOptions.TabIndex = 2;
            this.btnResetRequestTxOptions.Text = "Reset";
            this.btnResetRequestTxOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestTxOptions.Click += new System.EventHandler(this.btnResetRequestTxOptions_Click);
            // 
            // btnRequestTx
            // 
            this.btnRequestTx.Location = new System.Drawing.Point(6, 6);
            this.btnRequestTx.Name = "btnRequestTx";
            this.btnRequestTx.Size = new System.Drawing.Size(153, 56);
            this.btnRequestTx.TabIndex = 1;
            this.btnRequestTx.Text = "RequestTx";
            this.btnRequestTx.UseVisualStyleBackColor = true;
            this.btnRequestTx.Click += new System.EventHandler(this.btnRequestTx_Click);
            // 
            // pgRequestTxOptions
            // 
            this.pgRequestTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestTxOptions.Location = new System.Drawing.Point(6, 68);
            this.pgRequestTxOptions.Name = "pgRequestTxOptions";
            this.pgRequestTxOptions.Size = new System.Drawing.Size(417, 272);
            this.pgRequestTxOptions.TabIndex = 0;
            // 
            // tabRequestAccountInfo
            // 
            this.tabRequestAccountInfo.Controls.Add(this.pgRequestAccountInfoOptions);
            this.tabRequestAccountInfo.Controls.Add(this.btnResetRequestAccountInfoOptions);
            this.tabRequestAccountInfo.Controls.Add(this.btnRequestAccountInfo);
            this.tabRequestAccountInfo.Location = new System.Drawing.Point(4, 73);
            this.tabRequestAccountInfo.Name = "tabRequestAccountInfo";
            this.tabRequestAccountInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestAccountInfo.Size = new System.Drawing.Size(429, 346);
            this.tabRequestAccountInfo.TabIndex = 4;
            this.tabRequestAccountInfo.Text = "RequestAccountInfo";
            this.tabRequestAccountInfo.UseVisualStyleBackColor = true;
            // 
            // pgRequestAccountInfoOptions
            // 
            this.pgRequestAccountInfoOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestAccountInfoOptions.Location = new System.Drawing.Point(6, 59);
            this.pgRequestAccountInfoOptions.Name = "pgRequestAccountInfoOptions";
            this.pgRequestAccountInfoOptions.Size = new System.Drawing.Size(417, 279);
            this.pgRequestAccountInfoOptions.TabIndex = 2;
            // 
            // btnResetRequestAccountInfoOptions
            // 
            this.btnResetRequestAccountInfoOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestAccountInfoOptions.Location = new System.Drawing.Point(348, 30);
            this.btnResetRequestAccountInfoOptions.Name = "btnResetRequestAccountInfoOptions";
            this.btnResetRequestAccountInfoOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetRequestAccountInfoOptions.TabIndex = 1;
            this.btnResetRequestAccountInfoOptions.Text = "Reset";
            this.btnResetRequestAccountInfoOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestAccountInfoOptions.Click += new System.EventHandler(this.btnResetRequestAccountInfoOptions_Click);
            // 
            // btnRequestAccountInfo
            // 
            this.btnRequestAccountInfo.Location = new System.Drawing.Point(3, 6);
            this.btnRequestAccountInfo.Name = "btnRequestAccountInfo";
            this.btnRequestAccountInfo.Size = new System.Drawing.Size(208, 47);
            this.btnRequestAccountInfo.TabIndex = 0;
            this.btnRequestAccountInfo.Text = "RequestAccountInfo";
            this.btnRequestAccountInfo.UseVisualStyleBackColor = true;
            this.btnRequestAccountInfo.Click += new System.EventHandler(this.btnRequestAccountInfo_Click);
            // 
            // tabRequestAccountTums
            // 
            this.tabRequestAccountTums.Controls.Add(this.btnResetRequestAccountTumsOptions);
            this.tabRequestAccountTums.Controls.Add(this.btnRequestAccountTums);
            this.tabRequestAccountTums.Controls.Add(this.pgRequestAccountTumsOptions);
            this.tabRequestAccountTums.Location = new System.Drawing.Point(4, 73);
            this.tabRequestAccountTums.Name = "tabRequestAccountTums";
            this.tabRequestAccountTums.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestAccountTums.Size = new System.Drawing.Size(429, 346);
            this.tabRequestAccountTums.TabIndex = 5;
            this.tabRequestAccountTums.Text = "RequestAccountTums";
            this.tabRequestAccountTums.UseVisualStyleBackColor = true;
            // 
            // btnResetRequestAccountTumsOptions
            // 
            this.btnResetRequestAccountTumsOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestAccountTumsOptions.Location = new System.Drawing.Point(348, 29);
            this.btnResetRequestAccountTumsOptions.Name = "btnResetRequestAccountTumsOptions";
            this.btnResetRequestAccountTumsOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetRequestAccountTumsOptions.TabIndex = 2;
            this.btnResetRequestAccountTumsOptions.Text = "Reset";
            this.btnResetRequestAccountTumsOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestAccountTumsOptions.Click += new System.EventHandler(this.btnResetRequestAccountTumsOptions_Click);
            // 
            // btnRequestAccountTums
            // 
            this.btnRequestAccountTums.Location = new System.Drawing.Point(6, 6);
            this.btnRequestAccountTums.Name = "btnRequestAccountTums";
            this.btnRequestAccountTums.Size = new System.Drawing.Size(189, 46);
            this.btnRequestAccountTums.TabIndex = 1;
            this.btnRequestAccountTums.Text = "RequestAccountTums";
            this.btnRequestAccountTums.UseVisualStyleBackColor = true;
            this.btnRequestAccountTums.Click += new System.EventHandler(this.btnRequestAccountTums_Click);
            // 
            // pgRequestAccountTumsOptions
            // 
            this.pgRequestAccountTumsOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestAccountTumsOptions.Location = new System.Drawing.Point(6, 65);
            this.pgRequestAccountTumsOptions.Name = "pgRequestAccountTumsOptions";
            this.pgRequestAccountTumsOptions.Size = new System.Drawing.Size(417, 275);
            this.pgRequestAccountTumsOptions.TabIndex = 0;
            // 
            // tabRequestAccountRelations
            // 
            this.tabRequestAccountRelations.Controls.Add(this.pgRequestAccountRelationsOptions);
            this.tabRequestAccountRelations.Controls.Add(this.btnResetRequestAccountRelationsOptions);
            this.tabRequestAccountRelations.Controls.Add(this.btnRequestAccountRelations);
            this.tabRequestAccountRelations.Location = new System.Drawing.Point(4, 73);
            this.tabRequestAccountRelations.Name = "tabRequestAccountRelations";
            this.tabRequestAccountRelations.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestAccountRelations.Size = new System.Drawing.Size(429, 346);
            this.tabRequestAccountRelations.TabIndex = 6;
            this.tabRequestAccountRelations.Text = "RequestAccountRelations";
            this.tabRequestAccountRelations.UseVisualStyleBackColor = true;
            // 
            // pgRequestAccountRelationsOptions
            // 
            this.pgRequestAccountRelationsOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestAccountRelationsOptions.Location = new System.Drawing.Point(7, 65);
            this.pgRequestAccountRelationsOptions.Name = "pgRequestAccountRelationsOptions";
            this.pgRequestAccountRelationsOptions.Size = new System.Drawing.Size(416, 275);
            this.pgRequestAccountRelationsOptions.TabIndex = 2;
            // 
            // btnResetRequestAccountRelationsOptions
            // 
            this.btnResetRequestAccountRelationsOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestAccountRelationsOptions.Location = new System.Drawing.Point(348, 36);
            this.btnResetRequestAccountRelationsOptions.Name = "btnResetRequestAccountRelationsOptions";
            this.btnResetRequestAccountRelationsOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetRequestAccountRelationsOptions.TabIndex = 1;
            this.btnResetRequestAccountRelationsOptions.Text = "Reset";
            this.btnResetRequestAccountRelationsOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestAccountRelationsOptions.Click += new System.EventHandler(this.btnResetRequestAccountRelationsOptions_Click);
            // 
            // btnRequestAccountRelations
            // 
            this.btnRequestAccountRelations.Location = new System.Drawing.Point(6, 6);
            this.btnRequestAccountRelations.Name = "btnRequestAccountRelations";
            this.btnRequestAccountRelations.Size = new System.Drawing.Size(169, 53);
            this.btnRequestAccountRelations.TabIndex = 0;
            this.btnRequestAccountRelations.Text = "RequestAccountRelations";
            this.btnRequestAccountRelations.UseVisualStyleBackColor = true;
            this.btnRequestAccountRelations.Click += new System.EventHandler(this.btnRequestAccountRelations_Click);
            // 
            // tabRequestAccountOffers
            // 
            this.tabRequestAccountOffers.Controls.Add(this.btnResetRequestAccountOffersOptions);
            this.tabRequestAccountOffers.Controls.Add(this.btnRequestAccountOffers);
            this.tabRequestAccountOffers.Controls.Add(this.pgRequestAccountOffersOptions);
            this.tabRequestAccountOffers.Location = new System.Drawing.Point(4, 73);
            this.tabRequestAccountOffers.Name = "tabRequestAccountOffers";
            this.tabRequestAccountOffers.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestAccountOffers.Size = new System.Drawing.Size(429, 346);
            this.tabRequestAccountOffers.TabIndex = 7;
            this.tabRequestAccountOffers.Text = "RequestAccountOffers";
            this.tabRequestAccountOffers.UseVisualStyleBackColor = true;
            // 
            // btnResetRequestAccountOffersOptions
            // 
            this.btnResetRequestAccountOffersOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestAccountOffersOptions.Location = new System.Drawing.Point(348, 22);
            this.btnResetRequestAccountOffersOptions.Name = "btnResetRequestAccountOffersOptions";
            this.btnResetRequestAccountOffersOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetRequestAccountOffersOptions.TabIndex = 2;
            this.btnResetRequestAccountOffersOptions.Text = "Reset";
            this.btnResetRequestAccountOffersOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestAccountOffersOptions.Click += new System.EventHandler(this.btnResetRequestAccountOffersOptions_Click);
            // 
            // btnRequestAccountOffers
            // 
            this.btnRequestAccountOffers.Location = new System.Drawing.Point(4, 4);
            this.btnRequestAccountOffers.Name = "btnRequestAccountOffers";
            this.btnRequestAccountOffers.Size = new System.Drawing.Size(176, 41);
            this.btnRequestAccountOffers.TabIndex = 1;
            this.btnRequestAccountOffers.Text = "RequestAccountOffers";
            this.btnRequestAccountOffers.UseVisualStyleBackColor = true;
            this.btnRequestAccountOffers.Click += new System.EventHandler(this.btnRequestAccountOffers_Click);
            // 
            // pgRequestAccountOffersOptions
            // 
            this.pgRequestAccountOffersOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestAccountOffersOptions.Location = new System.Drawing.Point(7, 51);
            this.pgRequestAccountOffersOptions.Name = "pgRequestAccountOffersOptions";
            this.pgRequestAccountOffersOptions.Size = new System.Drawing.Size(416, 289);
            this.pgRequestAccountOffersOptions.TabIndex = 0;
            // 
            // tabRequestAccountTx
            // 
            this.tabRequestAccountTx.Controls.Add(this.pgRequestAccountTxOptions);
            this.tabRequestAccountTx.Controls.Add(this.btnResetRequestAccountTxOptions);
            this.tabRequestAccountTx.Controls.Add(this.btnRequestAccountTx);
            this.tabRequestAccountTx.Location = new System.Drawing.Point(4, 73);
            this.tabRequestAccountTx.Name = "tabRequestAccountTx";
            this.tabRequestAccountTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestAccountTx.Size = new System.Drawing.Size(429, 346);
            this.tabRequestAccountTx.TabIndex = 8;
            this.tabRequestAccountTx.Text = "RequestAccountTx";
            this.tabRequestAccountTx.UseVisualStyleBackColor = true;
            // 
            // pgRequestAccountTxOptions
            // 
            this.pgRequestAccountTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestAccountTxOptions.Location = new System.Drawing.Point(7, 60);
            this.pgRequestAccountTxOptions.Name = "pgRequestAccountTxOptions";
            this.pgRequestAccountTxOptions.Size = new System.Drawing.Size(419, 280);
            this.pgRequestAccountTxOptions.TabIndex = 2;
            // 
            // btnResetRequestAccountTxOptions
            // 
            this.btnResetRequestAccountTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestAccountTxOptions.Location = new System.Drawing.Point(348, 31);
            this.btnResetRequestAccountTxOptions.Name = "btnResetRequestAccountTxOptions";
            this.btnResetRequestAccountTxOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetRequestAccountTxOptions.TabIndex = 1;
            this.btnResetRequestAccountTxOptions.Text = "Reset";
            this.btnResetRequestAccountTxOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestAccountTxOptions.Click += new System.EventHandler(this.btnResetRequestAccountTxOptions_Click);
            // 
            // btnRequestAccountTx
            // 
            this.btnRequestAccountTx.Location = new System.Drawing.Point(7, 10);
            this.btnRequestAccountTx.Name = "btnRequestAccountTx";
            this.btnRequestAccountTx.Size = new System.Drawing.Size(161, 43);
            this.btnRequestAccountTx.TabIndex = 0;
            this.btnRequestAccountTx.Text = "RequestAccountTx";
            this.btnRequestAccountTx.UseVisualStyleBackColor = true;
            this.btnRequestAccountTx.Click += new System.EventHandler(this.btnRequestAccountTx_Click);
            // 
            // tabRequestOrderBook
            // 
            this.tabRequestOrderBook.Controls.Add(this.btnResetRequestOrderBookOptions);
            this.tabRequestOrderBook.Controls.Add(this.btnRequestOrderBook);
            this.tabRequestOrderBook.Controls.Add(this.pgRequestOrderBookOptions);
            this.tabRequestOrderBook.Location = new System.Drawing.Point(4, 73);
            this.tabRequestOrderBook.Name = "tabRequestOrderBook";
            this.tabRequestOrderBook.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestOrderBook.Size = new System.Drawing.Size(429, 346);
            this.tabRequestOrderBook.TabIndex = 9;
            this.tabRequestOrderBook.Text = "RequestOrderBook";
            this.tabRequestOrderBook.UseVisualStyleBackColor = true;
            // 
            // btnResetRequestOrderBookOptions
            // 
            this.btnResetRequestOrderBookOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestOrderBookOptions.Location = new System.Drawing.Point(348, 34);
            this.btnResetRequestOrderBookOptions.Name = "btnResetRequestOrderBookOptions";
            this.btnResetRequestOrderBookOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetRequestOrderBookOptions.TabIndex = 2;
            this.btnResetRequestOrderBookOptions.Text = "Reset";
            this.btnResetRequestOrderBookOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestOrderBookOptions.Click += new System.EventHandler(this.btnResetRequestOrderBookOptions_Click);
            // 
            // btnRequestOrderBook
            // 
            this.btnRequestOrderBook.Location = new System.Drawing.Point(7, 10);
            this.btnRequestOrderBook.Name = "btnRequestOrderBook";
            this.btnRequestOrderBook.Size = new System.Drawing.Size(164, 47);
            this.btnRequestOrderBook.TabIndex = 1;
            this.btnRequestOrderBook.Text = "RequestOrderBook";
            this.btnRequestOrderBook.UseVisualStyleBackColor = true;
            this.btnRequestOrderBook.Click += new System.EventHandler(this.btnRequestOrderBook_Click);
            // 
            // pgRequestOrderBookOptions
            // 
            this.pgRequestOrderBookOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestOrderBookOptions.Location = new System.Drawing.Point(7, 63);
            this.pgRequestOrderBookOptions.Name = "pgRequestOrderBookOptions";
            this.pgRequestOrderBookOptions.Size = new System.Drawing.Size(416, 246);
            this.pgRequestOrderBookOptions.TabIndex = 0;
            // 
            // tabPathFindRequest
            // 
            this.tabPathFindRequest.Controls.Add(this.tabControlPathFindRequest);
            this.tabPathFindRequest.Location = new System.Drawing.Point(4, 28);
            this.tabPathFindRequest.Name = "tabPathFindRequest";
            this.tabPathFindRequest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPathFindRequest.Size = new System.Drawing.Size(443, 429);
            this.tabPathFindRequest.TabIndex = 3;
            this.tabPathFindRequest.Text = "PathFindRequest";
            this.tabPathFindRequest.UseVisualStyleBackColor = true;
            // 
            // tabControlPathFindRequest
            // 
            this.tabControlPathFindRequest.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlPathFindRequest.Controls.Add(this.tabRequestPathFind);
            this.tabControlPathFindRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPathFindRequest.Location = new System.Drawing.Point(3, 3);
            this.tabControlPathFindRequest.Name = "tabControlPathFindRequest";
            this.tabControlPathFindRequest.SelectedIndex = 0;
            this.tabControlPathFindRequest.Size = new System.Drawing.Size(437, 423);
            this.tabControlPathFindRequest.TabIndex = 0;
            // 
            // tabRequestPathFind
            // 
            this.tabRequestPathFind.Controls.Add(this.pgRequestPathFindOptions);
            this.tabRequestPathFind.Controls.Add(this.btnResetRequestPathFindOptions);
            this.tabRequestPathFind.Controls.Add(this.btnRequestPathFind);
            this.tabRequestPathFind.Location = new System.Drawing.Point(4, 25);
            this.tabRequestPathFind.Name = "tabRequestPathFind";
            this.tabRequestPathFind.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestPathFind.Size = new System.Drawing.Size(429, 394);
            this.tabRequestPathFind.TabIndex = 0;
            this.tabRequestPathFind.Text = "RequestPathFind";
            this.tabRequestPathFind.UseVisualStyleBackColor = true;
            // 
            // pgRequestPathFindOptions
            // 
            this.pgRequestPathFindOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgRequestPathFindOptions.Location = new System.Drawing.Point(6, 53);
            this.pgRequestPathFindOptions.Name = "pgRequestPathFindOptions";
            this.pgRequestPathFindOptions.Size = new System.Drawing.Size(417, 326);
            this.pgRequestPathFindOptions.TabIndex = 2;
            // 
            // btnResetRequestPathFindOptions
            // 
            this.btnResetRequestPathFindOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRequestPathFindOptions.Location = new System.Drawing.Point(348, 23);
            this.btnResetRequestPathFindOptions.Name = "btnResetRequestPathFindOptions";
            this.btnResetRequestPathFindOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetRequestPathFindOptions.TabIndex = 1;
            this.btnResetRequestPathFindOptions.Text = "Reset";
            this.btnResetRequestPathFindOptions.UseVisualStyleBackColor = true;
            this.btnResetRequestPathFindOptions.Click += new System.EventHandler(this.btnResetRequestPathFindOptions_Click);
            // 
            // btnRequestPathFind
            // 
            this.btnRequestPathFind.Location = new System.Drawing.Point(6, 6);
            this.btnRequestPathFind.Name = "btnRequestPathFind";
            this.btnRequestPathFind.Size = new System.Drawing.Size(139, 41);
            this.btnRequestPathFind.TabIndex = 0;
            this.btnRequestPathFind.Text = "RequestPathFind";
            this.btnRequestPathFind.UseVisualStyleBackColor = true;
            this.btnRequestPathFind.Click += new System.EventHandler(this.btnRequestPathFind_Click);
            // 
            // tabTransactions
            // 
            this.tabTransactions.Controls.Add(this.tabControlTransactions);
            this.tabTransactions.Location = new System.Drawing.Point(4, 28);
            this.tabTransactions.Name = "tabTransactions";
            this.tabTransactions.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactions.Size = new System.Drawing.Size(443, 416);
            this.tabTransactions.TabIndex = 1;
            this.tabTransactions.Text = "Transaction";
            this.tabTransactions.UseVisualStyleBackColor = true;
            // 
            // tabControlTransactions
            // 
            this.tabControlTransactions.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlTransactions.Controls.Add(this.tabBuildPaymentTx);
            this.tabControlTransactions.Controls.Add(this.tabBuildRelationTx);
            this.tabControlTransactions.Controls.Add(this.tabBuildAccountSetTx);
            this.tabControlTransactions.Controls.Add(this.tabBuildOfferCreateTx);
            this.tabControlTransactions.Controls.Add(this.tabBuildOfferCancelTx);
            this.tabControlTransactions.Controls.Add(this.DeployContractTx);
            this.tabControlTransactions.Controls.Add(this.tabCallContractTx);
            this.tabControlTransactions.Controls.Add(this.tabBuildSignTx);
            this.tabControlTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTransactions.Location = new System.Drawing.Point(3, 3);
            this.tabControlTransactions.Multiline = true;
            this.tabControlTransactions.Name = "tabControlTransactions";
            this.tabControlTransactions.SelectedIndex = 0;
            this.tabControlTransactions.Size = new System.Drawing.Size(437, 410);
            this.tabControlTransactions.TabIndex = 0;
            // 
            // tabBuildPaymentTx
            // 
            this.tabBuildPaymentTx.Controls.Add(this.splitContainer5);
            this.tabBuildPaymentTx.Controls.Add(this.btnResetBuildPaymentTxOptions);
            this.tabBuildPaymentTx.Controls.Add(this.btnBuildPaymentTx);
            this.tabBuildPaymentTx.Location = new System.Drawing.Point(4, 73);
            this.tabBuildPaymentTx.Name = "tabBuildPaymentTx";
            this.tabBuildPaymentTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuildPaymentTx.Size = new System.Drawing.Size(429, 333);
            this.tabBuildPaymentTx.TabIndex = 0;
            this.tabBuildPaymentTx.Text = "BuildPaymentTx";
            this.tabBuildPaymentTx.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer5.Location = new System.Drawing.Point(7, 61);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.pgBuildPaymentTxOptions);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.pgBuildPaymentTxSettings);
            this.splitContainer5.Size = new System.Drawing.Size(416, 266);
            this.splitContainer5.SplitterDistance = 123;
            this.splitContainer5.TabIndex = 4;
            // 
            // pgBuildPaymentTxOptions
            // 
            this.pgBuildPaymentTxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildPaymentTxOptions.HelpVisible = false;
            this.pgBuildPaymentTxOptions.Location = new System.Drawing.Point(0, 0);
            this.pgBuildPaymentTxOptions.Name = "pgBuildPaymentTxOptions";
            this.pgBuildPaymentTxOptions.Size = new System.Drawing.Size(416, 123);
            this.pgBuildPaymentTxOptions.TabIndex = 2;
            // 
            // pgBuildPaymentTxSettings
            // 
            this.pgBuildPaymentTxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildPaymentTxSettings.HelpVisible = false;
            this.pgBuildPaymentTxSettings.Location = new System.Drawing.Point(0, 0);
            this.pgBuildPaymentTxSettings.Name = "pgBuildPaymentTxSettings";
            this.pgBuildPaymentTxSettings.Size = new System.Drawing.Size(416, 139);
            this.pgBuildPaymentTxSettings.TabIndex = 3;
            // 
            // btnResetBuildPaymentTxOptions
            // 
            this.btnResetBuildPaymentTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetBuildPaymentTxOptions.Location = new System.Drawing.Point(344, 33);
            this.btnResetBuildPaymentTxOptions.Name = "btnResetBuildPaymentTxOptions";
            this.btnResetBuildPaymentTxOptions.Size = new System.Drawing.Size(79, 25);
            this.btnResetBuildPaymentTxOptions.TabIndex = 1;
            this.btnResetBuildPaymentTxOptions.Text = "Reset";
            this.btnResetBuildPaymentTxOptions.UseVisualStyleBackColor = true;
            this.btnResetBuildPaymentTxOptions.Click += new System.EventHandler(this.btnResetBuildPaymentTxOptions_Click);
            // 
            // btnBuildPaymentTx
            // 
            this.btnBuildPaymentTx.Location = new System.Drawing.Point(7, 7);
            this.btnBuildPaymentTx.Name = "btnBuildPaymentTx";
            this.btnBuildPaymentTx.Size = new System.Drawing.Size(147, 51);
            this.btnBuildPaymentTx.TabIndex = 0;
            this.btnBuildPaymentTx.Text = "BuildPaymentTx";
            this.btnBuildPaymentTx.UseVisualStyleBackColor = true;
            this.btnBuildPaymentTx.Click += new System.EventHandler(this.btnBuildPaymentTx_Click);
            // 
            // tabBuildRelationTx
            // 
            this.tabBuildRelationTx.Controls.Add(this.splitContainer6);
            this.tabBuildRelationTx.Controls.Add(this.btnResetBuildRelationTxOptions);
            this.tabBuildRelationTx.Controls.Add(this.btnBuildRelationTx);
            this.tabBuildRelationTx.Location = new System.Drawing.Point(4, 73);
            this.tabBuildRelationTx.Name = "tabBuildRelationTx";
            this.tabBuildRelationTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuildRelationTx.Size = new System.Drawing.Size(429, 333);
            this.tabBuildRelationTx.TabIndex = 1;
            this.tabBuildRelationTx.Text = "BuildRelationTx";
            this.tabBuildRelationTx.UseVisualStyleBackColor = true;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer6.Location = new System.Drawing.Point(7, 48);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.pgBuildRelationTxOptions);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.pgBuildRelationTxSettings);
            this.splitContainer6.Size = new System.Drawing.Size(416, 279);
            this.splitContainer6.SplitterDistance = 128;
            this.splitContainer6.TabIndex = 3;
            // 
            // pgBuildRelationTxOptions
            // 
            this.pgBuildRelationTxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildRelationTxOptions.HelpVisible = false;
            this.pgBuildRelationTxOptions.Location = new System.Drawing.Point(0, 0);
            this.pgBuildRelationTxOptions.Name = "pgBuildRelationTxOptions";
            this.pgBuildRelationTxOptions.Size = new System.Drawing.Size(416, 128);
            this.pgBuildRelationTxOptions.TabIndex = 2;
            // 
            // pgBuildRelationTxSettings
            // 
            this.pgBuildRelationTxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildRelationTxSettings.HelpVisible = false;
            this.pgBuildRelationTxSettings.Location = new System.Drawing.Point(0, 0);
            this.pgBuildRelationTxSettings.Name = "pgBuildRelationTxSettings";
            this.pgBuildRelationTxSettings.Size = new System.Drawing.Size(416, 147);
            this.pgBuildRelationTxSettings.TabIndex = 2;
            // 
            // btnResetBuildRelationTxOptions
            // 
            this.btnResetBuildRelationTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetBuildRelationTxOptions.Location = new System.Drawing.Point(351, 16);
            this.btnResetBuildRelationTxOptions.Name = "btnResetBuildRelationTxOptions";
            this.btnResetBuildRelationTxOptions.Size = new System.Drawing.Size(72, 26);
            this.btnResetBuildRelationTxOptions.TabIndex = 1;
            this.btnResetBuildRelationTxOptions.Text = "Reset";
            this.btnResetBuildRelationTxOptions.UseVisualStyleBackColor = true;
            this.btnResetBuildRelationTxOptions.Click += new System.EventHandler(this.btnResetBuildRelationOptions_Click);
            // 
            // btnBuildRelationTx
            // 
            this.btnBuildRelationTx.Location = new System.Drawing.Point(7, 7);
            this.btnBuildRelationTx.Name = "btnBuildRelationTx";
            this.btnBuildRelationTx.Size = new System.Drawing.Size(134, 35);
            this.btnBuildRelationTx.TabIndex = 0;
            this.btnBuildRelationTx.Text = "BuildRelationTx";
            this.btnBuildRelationTx.UseVisualStyleBackColor = true;
            this.btnBuildRelationTx.Click += new System.EventHandler(this.btnBuildRelationTx_Click);
            // 
            // tabBuildAccountSetTx
            // 
            this.tabBuildAccountSetTx.Controls.Add(this.splitContainer7);
            this.tabBuildAccountSetTx.Controls.Add(this.btnResetBuildAccountSetTxOptions);
            this.tabBuildAccountSetTx.Controls.Add(this.btnBuildAccountSetTx);
            this.tabBuildAccountSetTx.Location = new System.Drawing.Point(4, 73);
            this.tabBuildAccountSetTx.Name = "tabBuildAccountSetTx";
            this.tabBuildAccountSetTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuildAccountSetTx.Size = new System.Drawing.Size(429, 333);
            this.tabBuildAccountSetTx.TabIndex = 2;
            this.tabBuildAccountSetTx.Text = "BuildAccountSetTx";
            this.tabBuildAccountSetTx.UseVisualStyleBackColor = true;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer7.Location = new System.Drawing.Point(6, 48);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.pgBuildAccountSetTxOptions);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.pgBuildAccountSetTxSettings);
            this.splitContainer7.Size = new System.Drawing.Size(416, 279);
            this.splitContainer7.SplitterDistance = 131;
            this.splitContainer7.TabIndex = 4;
            // 
            // pgBuildAccountSetTxOptions
            // 
            this.pgBuildAccountSetTxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildAccountSetTxOptions.HelpVisible = false;
            this.pgBuildAccountSetTxOptions.Location = new System.Drawing.Point(0, 0);
            this.pgBuildAccountSetTxOptions.Name = "pgBuildAccountSetTxOptions";
            this.pgBuildAccountSetTxOptions.Size = new System.Drawing.Size(416, 131);
            this.pgBuildAccountSetTxOptions.TabIndex = 2;
            // 
            // pgBuildAccountSetTxSettings
            // 
            this.pgBuildAccountSetTxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildAccountSetTxSettings.HelpVisible = false;
            this.pgBuildAccountSetTxSettings.Location = new System.Drawing.Point(0, 0);
            this.pgBuildAccountSetTxSettings.Name = "pgBuildAccountSetTxSettings";
            this.pgBuildAccountSetTxSettings.Size = new System.Drawing.Size(416, 144);
            this.pgBuildAccountSetTxSettings.TabIndex = 2;
            // 
            // btnResetBuildAccountSetTxOptions
            // 
            this.btnResetBuildAccountSetTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetBuildAccountSetTxOptions.Location = new System.Drawing.Point(347, 16);
            this.btnResetBuildAccountSetTxOptions.Name = "btnResetBuildAccountSetTxOptions";
            this.btnResetBuildAccountSetTxOptions.Size = new System.Drawing.Size(75, 26);
            this.btnResetBuildAccountSetTxOptions.TabIndex = 1;
            this.btnResetBuildAccountSetTxOptions.Text = "Reset";
            this.btnResetBuildAccountSetTxOptions.UseVisualStyleBackColor = true;
            this.btnResetBuildAccountSetTxOptions.Click += new System.EventHandler(this.btnResetBuildAccountSetTxOptions_Click);
            // 
            // btnBuildAccountSetTx
            // 
            this.btnBuildAccountSetTx.Location = new System.Drawing.Point(7, 7);
            this.btnBuildAccountSetTx.Name = "btnBuildAccountSetTx";
            this.btnBuildAccountSetTx.Size = new System.Drawing.Size(138, 35);
            this.btnBuildAccountSetTx.TabIndex = 0;
            this.btnBuildAccountSetTx.Text = "BuildAccountSetTx";
            this.btnBuildAccountSetTx.UseVisualStyleBackColor = true;
            this.btnBuildAccountSetTx.Click += new System.EventHandler(this.btnBuildAccountSetTx_Click);
            // 
            // tabBuildOfferCreateTx
            // 
            this.tabBuildOfferCreateTx.Controls.Add(this.splitContainer8);
            this.tabBuildOfferCreateTx.Controls.Add(this.btnResetBuildOfferCreateTxOptions);
            this.tabBuildOfferCreateTx.Controls.Add(this.btnBuildOfferCreateTx);
            this.tabBuildOfferCreateTx.Location = new System.Drawing.Point(4, 73);
            this.tabBuildOfferCreateTx.Name = "tabBuildOfferCreateTx";
            this.tabBuildOfferCreateTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuildOfferCreateTx.Size = new System.Drawing.Size(429, 333);
            this.tabBuildOfferCreateTx.TabIndex = 3;
            this.tabBuildOfferCreateTx.Text = "BuildOfferCreateTx";
            this.tabBuildOfferCreateTx.UseVisualStyleBackColor = true;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer8.Location = new System.Drawing.Point(7, 48);
            this.splitContainer8.Name = "splitContainer8";
            this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.pgBuildOfferCreateTxOptions);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.pgBuildOfferCreateTxSettings);
            this.splitContainer8.Size = new System.Drawing.Size(416, 279);
            this.splitContainer8.SplitterDistance = 132;
            this.splitContainer8.TabIndex = 5;
            // 
            // pgBuildOfferCreateTxOptions
            // 
            this.pgBuildOfferCreateTxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildOfferCreateTxOptions.HelpVisible = false;
            this.pgBuildOfferCreateTxOptions.Location = new System.Drawing.Point(0, 0);
            this.pgBuildOfferCreateTxOptions.Name = "pgBuildOfferCreateTxOptions";
            this.pgBuildOfferCreateTxOptions.Size = new System.Drawing.Size(416, 132);
            this.pgBuildOfferCreateTxOptions.TabIndex = 2;
            // 
            // pgBuildOfferCreateTxSettings
            // 
            this.pgBuildOfferCreateTxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildOfferCreateTxSettings.HelpVisible = false;
            this.pgBuildOfferCreateTxSettings.Location = new System.Drawing.Point(0, 0);
            this.pgBuildOfferCreateTxSettings.Name = "pgBuildOfferCreateTxSettings";
            this.pgBuildOfferCreateTxSettings.Size = new System.Drawing.Size(416, 143);
            this.pgBuildOfferCreateTxSettings.TabIndex = 2;
            // 
            // btnResetBuildOfferCreateTxOptions
            // 
            this.btnResetBuildOfferCreateTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetBuildOfferCreateTxOptions.Location = new System.Drawing.Point(341, 18);
            this.btnResetBuildOfferCreateTxOptions.Name = "btnResetBuildOfferCreateTxOptions";
            this.btnResetBuildOfferCreateTxOptions.Size = new System.Drawing.Size(79, 24);
            this.btnResetBuildOfferCreateTxOptions.TabIndex = 1;
            this.btnResetBuildOfferCreateTxOptions.Text = "Reset";
            this.btnResetBuildOfferCreateTxOptions.UseVisualStyleBackColor = true;
            this.btnResetBuildOfferCreateTxOptions.Click += new System.EventHandler(this.btnResetBuildOfferCreateTxOptions_Click);
            // 
            // btnBuildOfferCreateTx
            // 
            this.btnBuildOfferCreateTx.Location = new System.Drawing.Point(7, 7);
            this.btnBuildOfferCreateTx.Name = "btnBuildOfferCreateTx";
            this.btnBuildOfferCreateTx.Size = new System.Drawing.Size(129, 35);
            this.btnBuildOfferCreateTx.TabIndex = 0;
            this.btnBuildOfferCreateTx.Text = "BuildOfferCreateTx";
            this.btnBuildOfferCreateTx.UseVisualStyleBackColor = true;
            this.btnBuildOfferCreateTx.Click += new System.EventHandler(this.btnBuildOfferCreateTx_Click);
            // 
            // tabBuildOfferCancelTx
            // 
            this.tabBuildOfferCancelTx.Controls.Add(this.splitContainer9);
            this.tabBuildOfferCancelTx.Controls.Add(this.btnResetBuildOfferCancelTxOptions);
            this.tabBuildOfferCancelTx.Controls.Add(this.btnBuildOfferCancelTx);
            this.tabBuildOfferCancelTx.Location = new System.Drawing.Point(4, 73);
            this.tabBuildOfferCancelTx.Name = "tabBuildOfferCancelTx";
            this.tabBuildOfferCancelTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuildOfferCancelTx.Size = new System.Drawing.Size(429, 333);
            this.tabBuildOfferCancelTx.TabIndex = 4;
            this.tabBuildOfferCancelTx.Text = "BuildOfferCancelTx";
            this.tabBuildOfferCancelTx.UseVisualStyleBackColor = true;
            // 
            // splitContainer9
            // 
            this.splitContainer9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer9.Location = new System.Drawing.Point(6, 47);
            this.splitContainer9.Name = "splitContainer9";
            this.splitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer9.Panel1
            // 
            this.splitContainer9.Panel1.Controls.Add(this.pgBuildOfferCancelTxOptions);
            // 
            // splitContainer9.Panel2
            // 
            this.splitContainer9.Panel2.Controls.Add(this.pgBuildOfferCancelTxSettings);
            this.splitContainer9.Size = new System.Drawing.Size(416, 280);
            this.splitContainer9.SplitterDistance = 132;
            this.splitContainer9.TabIndex = 8;
            // 
            // pgBuildOfferCancelTxOptions
            // 
            this.pgBuildOfferCancelTxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildOfferCancelTxOptions.HelpVisible = false;
            this.pgBuildOfferCancelTxOptions.Location = new System.Drawing.Point(0, 0);
            this.pgBuildOfferCancelTxOptions.Name = "pgBuildOfferCancelTxOptions";
            this.pgBuildOfferCancelTxOptions.Size = new System.Drawing.Size(416, 132);
            this.pgBuildOfferCancelTxOptions.TabIndex = 2;
            // 
            // pgBuildOfferCancelTxSettings
            // 
            this.pgBuildOfferCancelTxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildOfferCancelTxSettings.HelpVisible = false;
            this.pgBuildOfferCancelTxSettings.Location = new System.Drawing.Point(0, 0);
            this.pgBuildOfferCancelTxSettings.Name = "pgBuildOfferCancelTxSettings";
            this.pgBuildOfferCancelTxSettings.Size = new System.Drawing.Size(416, 144);
            this.pgBuildOfferCancelTxSettings.TabIndex = 2;
            // 
            // btnResetBuildOfferCancelTxOptions
            // 
            this.btnResetBuildOfferCancelTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetBuildOfferCancelTxOptions.Location = new System.Drawing.Point(344, 18);
            this.btnResetBuildOfferCancelTxOptions.Name = "btnResetBuildOfferCancelTxOptions";
            this.btnResetBuildOfferCancelTxOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetBuildOfferCancelTxOptions.TabIndex = 7;
            this.btnResetBuildOfferCancelTxOptions.Text = "Reset";
            this.btnResetBuildOfferCancelTxOptions.UseVisualStyleBackColor = true;
            this.btnResetBuildOfferCancelTxOptions.Click += new System.EventHandler(this.btnResetBuildOfferCancelTxOptions_Click);
            // 
            // btnBuildOfferCancelTx
            // 
            this.btnBuildOfferCancelTx.Location = new System.Drawing.Point(6, 6);
            this.btnBuildOfferCancelTx.Name = "btnBuildOfferCancelTx";
            this.btnBuildOfferCancelTx.Size = new System.Drawing.Size(129, 35);
            this.btnBuildOfferCancelTx.TabIndex = 6;
            this.btnBuildOfferCancelTx.Text = "BuildOfferCancelTx";
            this.btnBuildOfferCancelTx.UseVisualStyleBackColor = true;
            this.btnBuildOfferCancelTx.Click += new System.EventHandler(this.btnBuildOfferCancelTx_Click);
            // 
            // DeployContractTx
            // 
            this.DeployContractTx.Controls.Add(this.splitContainer4);
            this.DeployContractTx.Controls.Add(this.btnResetDeployContractTxOptions);
            this.DeployContractTx.Controls.Add(this.btnDeployContractTx);
            this.DeployContractTx.Location = new System.Drawing.Point(4, 73);
            this.DeployContractTx.Name = "DeployContractTx";
            this.DeployContractTx.Padding = new System.Windows.Forms.Padding(3);
            this.DeployContractTx.Size = new System.Drawing.Size(429, 333);
            this.DeployContractTx.TabIndex = 5;
            this.DeployContractTx.Text = "DeployContractTx";
            this.DeployContractTx.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.Location = new System.Drawing.Point(6, 48);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.pgDeployContractTxOptions);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.pgDeployContractTxSettings);
            this.splitContainer4.Size = new System.Drawing.Size(416, 279);
            this.splitContainer4.SplitterDistance = 132;
            this.splitContainer4.TabIndex = 11;
            // 
            // pgDeployContractTxOptions
            // 
            this.pgDeployContractTxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgDeployContractTxOptions.HelpVisible = false;
            this.pgDeployContractTxOptions.Location = new System.Drawing.Point(0, 0);
            this.pgDeployContractTxOptions.Name = "pgDeployContractTxOptions";
            this.pgDeployContractTxOptions.Size = new System.Drawing.Size(416, 132);
            this.pgDeployContractTxOptions.TabIndex = 2;
            // 
            // pgDeployContractTxSettings
            // 
            this.pgDeployContractTxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgDeployContractTxSettings.HelpVisible = false;
            this.pgDeployContractTxSettings.Location = new System.Drawing.Point(0, 0);
            this.pgDeployContractTxSettings.Name = "pgDeployContractTxSettings";
            this.pgDeployContractTxSettings.Size = new System.Drawing.Size(416, 143);
            this.pgDeployContractTxSettings.TabIndex = 2;
            // 
            // btnResetDeployContractTxOptions
            // 
            this.btnResetDeployContractTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetDeployContractTxOptions.Location = new System.Drawing.Point(347, 19);
            this.btnResetDeployContractTxOptions.Name = "btnResetDeployContractTxOptions";
            this.btnResetDeployContractTxOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetDeployContractTxOptions.TabIndex = 10;
            this.btnResetDeployContractTxOptions.Text = "Reset";
            this.btnResetDeployContractTxOptions.UseVisualStyleBackColor = true;
            this.btnResetDeployContractTxOptions.Click += new System.EventHandler(this.btnResetDeployContractTxOptions_Click);
            // 
            // btnDeployContractTx
            // 
            this.btnDeployContractTx.Location = new System.Drawing.Point(6, 7);
            this.btnDeployContractTx.Name = "btnDeployContractTx";
            this.btnDeployContractTx.Size = new System.Drawing.Size(129, 35);
            this.btnDeployContractTx.TabIndex = 9;
            this.btnDeployContractTx.Text = "DeployContractTx";
            this.btnDeployContractTx.UseVisualStyleBackColor = true;
            this.btnDeployContractTx.Click += new System.EventHandler(this.btnDeployContractTx_Click);
            // 
            // tabCallContractTx
            // 
            this.tabCallContractTx.Controls.Add(this.splitContainer12);
            this.tabCallContractTx.Controls.Add(this.btnResetCallContractTxOptions);
            this.tabCallContractTx.Controls.Add(this.btnCallContractTx);
            this.tabCallContractTx.Location = new System.Drawing.Point(4, 73);
            this.tabCallContractTx.Name = "tabCallContractTx";
            this.tabCallContractTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabCallContractTx.Size = new System.Drawing.Size(429, 333);
            this.tabCallContractTx.TabIndex = 6;
            this.tabCallContractTx.Text = "CallContractTx";
            this.tabCallContractTx.UseVisualStyleBackColor = true;
            // 
            // splitContainer12
            // 
            this.splitContainer12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer12.Location = new System.Drawing.Point(6, 48);
            this.splitContainer12.Name = "splitContainer12";
            this.splitContainer12.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer12.Panel1
            // 
            this.splitContainer12.Panel1.Controls.Add(this.pgCallContractTxOptions);
            // 
            // splitContainer12.Panel2
            // 
            this.splitContainer12.Panel2.Controls.Add(this.pgCallContractTxSettings);
            this.splitContainer12.Size = new System.Drawing.Size(416, 279);
            this.splitContainer12.SplitterDistance = 132;
            this.splitContainer12.TabIndex = 14;
            // 
            // pgCallContractTxOptions
            // 
            this.pgCallContractTxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgCallContractTxOptions.HelpVisible = false;
            this.pgCallContractTxOptions.Location = new System.Drawing.Point(0, 0);
            this.pgCallContractTxOptions.Name = "pgCallContractTxOptions";
            this.pgCallContractTxOptions.Size = new System.Drawing.Size(416, 132);
            this.pgCallContractTxOptions.TabIndex = 2;
            // 
            // pgCallContractTxSettings
            // 
            this.pgCallContractTxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgCallContractTxSettings.HelpVisible = false;
            this.pgCallContractTxSettings.Location = new System.Drawing.Point(0, 0);
            this.pgCallContractTxSettings.Name = "pgCallContractTxSettings";
            this.pgCallContractTxSettings.Size = new System.Drawing.Size(416, 143);
            this.pgCallContractTxSettings.TabIndex = 2;
            // 
            // btnResetCallContractTxOptions
            // 
            this.btnResetCallContractTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetCallContractTxOptions.Location = new System.Drawing.Point(347, 19);
            this.btnResetCallContractTxOptions.Name = "btnResetCallContractTxOptions";
            this.btnResetCallContractTxOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetCallContractTxOptions.TabIndex = 13;
            this.btnResetCallContractTxOptions.Text = "Reset";
            this.btnResetCallContractTxOptions.UseVisualStyleBackColor = true;
            this.btnResetCallContractTxOptions.Click += new System.EventHandler(this.btnResetCallContractTxOptions_Click);
            // 
            // btnCallContractTx
            // 
            this.btnCallContractTx.Location = new System.Drawing.Point(6, 7);
            this.btnCallContractTx.Name = "btnCallContractTx";
            this.btnCallContractTx.Size = new System.Drawing.Size(129, 35);
            this.btnCallContractTx.TabIndex = 12;
            this.btnCallContractTx.Text = "CallContractTx";
            this.btnCallContractTx.UseVisualStyleBackColor = true;
            this.btnCallContractTx.Click += new System.EventHandler(this.btnCallContractTx_Click);
            // 
            // tabBuildSignTx
            // 
            this.tabBuildSignTx.Controls.Add(this.splitContainer10);
            this.tabBuildSignTx.Controls.Add(this.btnResetBuildSignTxOptions);
            this.tabBuildSignTx.Controls.Add(this.btnBuildSignTx);
            this.tabBuildSignTx.Location = new System.Drawing.Point(4, 73);
            this.tabBuildSignTx.Name = "tabBuildSignTx";
            this.tabBuildSignTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuildSignTx.Size = new System.Drawing.Size(429, 333);
            this.tabBuildSignTx.TabIndex = 7;
            this.tabBuildSignTx.Text = "BuildSignTx";
            this.tabBuildSignTx.UseVisualStyleBackColor = true;
            // 
            // splitContainer10
            // 
            this.splitContainer10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer10.Location = new System.Drawing.Point(6, 48);
            this.splitContainer10.Name = "splitContainer10";
            this.splitContainer10.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer10.Panel1
            // 
            this.splitContainer10.Panel1.Controls.Add(this.pgBuildSignTxOptions);
            this.splitContainer10.Size = new System.Drawing.Size(416, 279);
            this.splitContainer10.SplitterDistance = 132;
            this.splitContainer10.TabIndex = 17;
            // 
            // pgBuildSignTxOptions
            // 
            this.pgBuildSignTxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBuildSignTxOptions.HelpVisible = false;
            this.pgBuildSignTxOptions.Location = new System.Drawing.Point(0, 0);
            this.pgBuildSignTxOptions.Name = "pgBuildSignTxOptions";
            this.pgBuildSignTxOptions.Size = new System.Drawing.Size(416, 132);
            this.pgBuildSignTxOptions.TabIndex = 2;
            // 
            // btnResetBuildSignTxOptions
            // 
            this.btnResetBuildSignTxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetBuildSignTxOptions.Location = new System.Drawing.Point(344, 28);
            this.btnResetBuildSignTxOptions.Name = "btnResetBuildSignTxOptions";
            this.btnResetBuildSignTxOptions.Size = new System.Drawing.Size(75, 23);
            this.btnResetBuildSignTxOptions.TabIndex = 16;
            this.btnResetBuildSignTxOptions.Text = "Reset";
            this.btnResetBuildSignTxOptions.UseVisualStyleBackColor = true;
            this.btnResetBuildSignTxOptions.Click += new System.EventHandler(this.btnResetBuildSignTxOptions_Click);
            // 
            // btnBuildSignTx
            // 
            this.btnBuildSignTx.Location = new System.Drawing.Point(6, 7);
            this.btnBuildSignTx.Name = "btnBuildSignTx";
            this.btnBuildSignTx.Size = new System.Drawing.Size(129, 35);
            this.btnBuildSignTx.TabIndex = 15;
            this.btnBuildSignTx.Text = "BuildSignTx";
            this.btnBuildSignTx.UseVisualStyleBackColor = true;
            this.btnBuildSignTx.Click += new System.EventHandler(this.btnBuildSignTx_Click);
            // 
            // tabEvents
            // 
            this.tabEvents.Controls.Add(this.tabControlEvents);
            this.tabEvents.Location = new System.Drawing.Point(4, 28);
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabEvents.Size = new System.Drawing.Size(443, 416);
            this.tabEvents.TabIndex = 4;
            this.tabEvents.Text = "Events";
            this.tabEvents.UseVisualStyleBackColor = true;
            // 
            // tabControlEvents
            // 
            this.tabControlEvents.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlEvents.Controls.Add(this.tabTransactionsEvent);
            this.tabControlEvents.Controls.Add(this.tabLedgerClosedEvent);
            this.tabControlEvents.Controls.Add(this.tabOrderBooksStub);
            this.tabControlEvents.Controls.Add(this.tabAccountStub);
            this.tabControlEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEvents.Location = new System.Drawing.Point(3, 3);
            this.tabControlEvents.Name = "tabControlEvents";
            this.tabControlEvents.SelectedIndex = 0;
            this.tabControlEvents.Size = new System.Drawing.Size(437, 410);
            this.tabControlEvents.TabIndex = 3;
            // 
            // tabTransactionsEvent
            // 
            this.tabTransactionsEvent.Controls.Add(this.ovTransactionsMessage);
            this.tabTransactionsEvent.Controls.Add(this.jtvTransactionsMessage);
            this.tabTransactionsEvent.Controls.Add(this.chkRefreshTransactions);
            this.tabTransactionsEvent.Controls.Add(this.btnOnTransactions);
            this.tabTransactionsEvent.Controls.Add(this.label6);
            this.tabTransactionsEvent.Controls.Add(this.btnUnTransactions);
            this.tabTransactionsEvent.Controls.Add(this.lblTransactionsIndex);
            this.tabTransactionsEvent.Location = new System.Drawing.Point(4, 25);
            this.tabTransactionsEvent.Name = "tabTransactionsEvent";
            this.tabTransactionsEvent.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactionsEvent.Size = new System.Drawing.Size(429, 394);
            this.tabTransactionsEvent.TabIndex = 0;
            this.tabTransactionsEvent.Text = "Transactions";
            this.tabTransactionsEvent.UseVisualStyleBackColor = true;
            // 
            // ovTransactionsMessage
            // 
            this.ovTransactionsMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ovTransactionsMessage.Location = new System.Drawing.Point(7, 232);
            this.ovTransactionsMessage.Name = "ovTransactionsMessage";
            this.ovTransactionsMessage.Size = new System.Drawing.Size(416, 156);
            this.ovTransactionsMessage.TabIndex = 5;
            // 
            // jtvTransactionsMessage
            // 
            this.jtvTransactionsMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jtvTransactionsMessage.Location = new System.Drawing.Point(6, 76);
            this.jtvTransactionsMessage.Name = "jtvTransactionsMessage";
            this.jtvTransactionsMessage.Size = new System.Drawing.Size(417, 149);
            this.jtvTransactionsMessage.TabIndex = 4;
            // 
            // chkRefreshTransactions
            // 
            this.chkRefreshTransactions.AutoSize = true;
            this.chkRefreshTransactions.Checked = true;
            this.chkRefreshTransactions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRefreshTransactions.Location = new System.Drawing.Point(267, 21);
            this.chkRefreshTransactions.Name = "chkRefreshTransactions";
            this.chkRefreshTransactions.Size = new System.Drawing.Size(84, 16);
            this.chkRefreshTransactions.TabIndex = 3;
            this.chkRefreshTransactions.Text = "Refreshing";
            this.chkRefreshTransactions.UseVisualStyleBackColor = true;
            // 
            // btnOnTransactions
            // 
            this.btnOnTransactions.Location = new System.Drawing.Point(6, 15);
            this.btnOnTransactions.Name = "btnOnTransactions";
            this.btnOnTransactions.Size = new System.Drawing.Size(117, 23);
            this.btnOnTransactions.TabIndex = 0;
            this.btnOnTransactions.Text = "+Transactions";
            this.btnOnTransactions.UseVisualStyleBackColor = true;
            this.btnOnTransactions.Click += new System.EventHandler(this.btnOnTransactions_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "Transaction Index: ";
            // 
            // btnUnTransactions
            // 
            this.btnUnTransactions.Location = new System.Drawing.Point(141, 15);
            this.btnUnTransactions.Name = "btnUnTransactions";
            this.btnUnTransactions.Size = new System.Drawing.Size(107, 23);
            this.btnUnTransactions.TabIndex = 0;
            this.btnUnTransactions.Text = "-Transactions";
            this.btnUnTransactions.UseVisualStyleBackColor = true;
            this.btnUnTransactions.Click += new System.EventHandler(this.btnUnTransactions_Click);
            // 
            // lblTransactionsIndex
            // 
            this.lblTransactionsIndex.AutoSize = true;
            this.lblTransactionsIndex.Location = new System.Drawing.Point(129, 54);
            this.lblTransactionsIndex.Name = "lblTransactionsIndex";
            this.lblTransactionsIndex.Size = new System.Drawing.Size(23, 12);
            this.lblTransactionsIndex.TabIndex = 2;
            this.lblTransactionsIndex.Text = "Off";
            // 
            // tabLedgerClosedEvent
            // 
            this.tabLedgerClosedEvent.Controls.Add(this.splitContainer11);
            this.tabLedgerClosedEvent.Controls.Add(this.chkRefreshLedgerClosed);
            this.tabLedgerClosedEvent.Controls.Add(this.btnOnLedgerClosed);
            this.tabLedgerClosedEvent.Controls.Add(this.label7);
            this.tabLedgerClosedEvent.Controls.Add(this.btnUnLedgerClosed);
            this.tabLedgerClosedEvent.Controls.Add(this.lblLedgerClosedIndex);
            this.tabLedgerClosedEvent.Location = new System.Drawing.Point(4, 25);
            this.tabLedgerClosedEvent.Name = "tabLedgerClosedEvent";
            this.tabLedgerClosedEvent.Padding = new System.Windows.Forms.Padding(3);
            this.tabLedgerClosedEvent.Size = new System.Drawing.Size(429, 394);
            this.tabLedgerClosedEvent.TabIndex = 1;
            this.tabLedgerClosedEvent.Text = "LedgerClosed";
            this.tabLedgerClosedEvent.UseVisualStyleBackColor = true;
            // 
            // splitContainer11
            // 
            this.splitContainer11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer11.Location = new System.Drawing.Point(15, 73);
            this.splitContainer11.Name = "splitContainer11";
            this.splitContainer11.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer11.Panel1
            // 
            this.splitContainer11.Panel1.Controls.Add(this.jtvLedgerClosedMessage);
            // 
            // splitContainer11.Panel2
            // 
            this.splitContainer11.Panel2.Controls.Add(this.ovLedgerClosedMessage);
            this.splitContainer11.Size = new System.Drawing.Size(408, 315);
            this.splitContainer11.SplitterDistance = 155;
            this.splitContainer11.TabIndex = 10;
            // 
            // jtvLedgerClosedMessage
            // 
            this.jtvLedgerClosedMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jtvLedgerClosedMessage.Location = new System.Drawing.Point(0, 0);
            this.jtvLedgerClosedMessage.Name = "jtvLedgerClosedMessage";
            this.jtvLedgerClosedMessage.Size = new System.Drawing.Size(408, 155);
            this.jtvLedgerClosedMessage.TabIndex = 9;
            // 
            // ovLedgerClosedMessage
            // 
            this.ovLedgerClosedMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ovLedgerClosedMessage.Location = new System.Drawing.Point(0, 0);
            this.ovLedgerClosedMessage.Name = "ovLedgerClosedMessage";
            this.ovLedgerClosedMessage.Size = new System.Drawing.Size(408, 156);
            this.ovLedgerClosedMessage.TabIndex = 0;
            // 
            // chkRefreshLedgerClosed
            // 
            this.chkRefreshLedgerClosed.AutoSize = true;
            this.chkRefreshLedgerClosed.Checked = true;
            this.chkRefreshLedgerClosed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRefreshLedgerClosed.Location = new System.Drawing.Point(276, 25);
            this.chkRefreshLedgerClosed.Name = "chkRefreshLedgerClosed";
            this.chkRefreshLedgerClosed.Size = new System.Drawing.Size(84, 16);
            this.chkRefreshLedgerClosed.TabIndex = 8;
            this.chkRefreshLedgerClosed.Text = "Refreshing";
            this.chkRefreshLedgerClosed.UseVisualStyleBackColor = true;
            // 
            // btnOnLedgerClosed
            // 
            this.btnOnLedgerClosed.Location = new System.Drawing.Point(15, 19);
            this.btnOnLedgerClosed.Name = "btnOnLedgerClosed";
            this.btnOnLedgerClosed.Size = new System.Drawing.Size(117, 23);
            this.btnOnLedgerClosed.TabIndex = 4;
            this.btnOnLedgerClosed.Text = "+LedgerClosed";
            this.btnOnLedgerClosed.UseVisualStyleBackColor = true;
            this.btnOnLedgerClosed.Click += new System.EventHandler(this.btnOnLedgerClosed_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "LedgerClosed Index: ";
            // 
            // btnUnLedgerClosed
            // 
            this.btnUnLedgerClosed.Location = new System.Drawing.Point(150, 19);
            this.btnUnLedgerClosed.Name = "btnUnLedgerClosed";
            this.btnUnLedgerClosed.Size = new System.Drawing.Size(107, 23);
            this.btnUnLedgerClosed.TabIndex = 5;
            this.btnUnLedgerClosed.Text = "-LedgerClosed";
            this.btnUnLedgerClosed.UseVisualStyleBackColor = true;
            this.btnUnLedgerClosed.Click += new System.EventHandler(this.btnUnLedgerClosed_Click);
            // 
            // lblLedgerClosedIndex
            // 
            this.lblLedgerClosedIndex.AutoSize = true;
            this.lblLedgerClosedIndex.Location = new System.Drawing.Point(138, 58);
            this.lblLedgerClosedIndex.Name = "lblLedgerClosedIndex";
            this.lblLedgerClosedIndex.Size = new System.Drawing.Size(23, 12);
            this.lblLedgerClosedIndex.TabIndex = 7;
            this.lblLedgerClosedIndex.Text = "Off";
            // 
            // tabOrderBooksStub
            // 
            this.tabOrderBooksStub.Controls.Add(this.tabCreateOrderBooksStub);
            this.tabOrderBooksStub.Controls.Add(this.chkRefreshOrderBook);
            this.tabOrderBooksStub.Controls.Add(this.btnOrderBooksStub);
            this.tabOrderBooksStub.Location = new System.Drawing.Point(4, 25);
            this.tabOrderBooksStub.Name = "tabOrderBooksStub";
            this.tabOrderBooksStub.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrderBooksStub.Size = new System.Drawing.Size(429, 394);
            this.tabOrderBooksStub.TabIndex = 2;
            this.tabOrderBooksStub.Text = "OrderBooksStub";
            this.tabOrderBooksStub.UseVisualStyleBackColor = true;
            // 
            // tabCreateOrderBooksStub
            // 
            this.tabCreateOrderBooksStub.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCreateOrderBooksStub.Controls.Add(this.tabCNYSWT);
            this.tabCreateOrderBooksStub.Controls.Add(this.tabSWTCNY);
            this.tabCreateOrderBooksStub.Location = new System.Drawing.Point(12, 50);
            this.tabCreateOrderBooksStub.Name = "tabCreateOrderBooksStub";
            this.tabCreateOrderBooksStub.SelectedIndex = 0;
            this.tabCreateOrderBooksStub.Size = new System.Drawing.Size(411, 338);
            this.tabCreateOrderBooksStub.TabIndex = 4;
            // 
            // tabCNYSWT
            // 
            this.tabCNYSWT.Controls.Add(this.ovCnySwt);
            this.tabCNYSWT.Controls.Add(this.jtvCnySwt);
            this.tabCNYSWT.Controls.Add(this.lblCnySwtIndex);
            this.tabCNYSWT.Controls.Add(this.label8);
            this.tabCNYSWT.Location = new System.Drawing.Point(4, 22);
            this.tabCNYSWT.Name = "tabCNYSWT";
            this.tabCNYSWT.Padding = new System.Windows.Forms.Padding(3);
            this.tabCNYSWT.Size = new System.Drawing.Size(403, 312);
            this.tabCNYSWT.TabIndex = 0;
            this.tabCNYSWT.Text = "CNY/SWT";
            this.tabCNYSWT.UseVisualStyleBackColor = true;
            // 
            // ovCnySwt
            // 
            this.ovCnySwt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ovCnySwt.Location = new System.Drawing.Point(6, 156);
            this.ovCnySwt.Name = "ovCnySwt";
            this.ovCnySwt.Size = new System.Drawing.Size(391, 150);
            this.ovCnySwt.TabIndex = 3;
            // 
            // jtvCnySwt
            // 
            this.jtvCnySwt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jtvCnySwt.Location = new System.Drawing.Point(6, 18);
            this.jtvCnySwt.Name = "jtvCnySwt";
            this.jtvCnySwt.Size = new System.Drawing.Size(391, 132);
            this.jtvCnySwt.TabIndex = 2;
            // 
            // lblCnySwtIndex
            // 
            this.lblCnySwtIndex.AutoSize = true;
            this.lblCnySwtIndex.Location = new System.Drawing.Point(61, 3);
            this.lblCnySwtIndex.Name = "lblCnySwtIndex";
            this.lblCnySwtIndex.Size = new System.Drawing.Size(17, 12);
            this.lblCnySwtIndex.TabIndex = 1;
            this.lblCnySwtIndex.Text = "-1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "CNY/SWT";
            // 
            // tabSWTCNY
            // 
            this.tabSWTCNY.Controls.Add(this.ovSwtCny);
            this.tabSWTCNY.Controls.Add(this.jtvSwtCny);
            this.tabSWTCNY.Controls.Add(this.label9);
            this.tabSWTCNY.Controls.Add(this.lblSwtCnyIndex);
            this.tabSWTCNY.Location = new System.Drawing.Point(4, 22);
            this.tabSWTCNY.Name = "tabSWTCNY";
            this.tabSWTCNY.Padding = new System.Windows.Forms.Padding(3);
            this.tabSWTCNY.Size = new System.Drawing.Size(403, 251);
            this.tabSWTCNY.TabIndex = 1;
            this.tabSWTCNY.Text = "SWT/CNY";
            this.tabSWTCNY.UseVisualStyleBackColor = true;
            // 
            // ovSwtCny
            // 
            this.ovSwtCny.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ovSwtCny.Location = new System.Drawing.Point(6, 167);
            this.ovSwtCny.Name = "ovSwtCny";
            this.ovSwtCny.Size = new System.Drawing.Size(391, 78);
            this.ovSwtCny.TabIndex = 3;
            // 
            // jtvSwtCny
            // 
            this.jtvSwtCny.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jtvSwtCny.Location = new System.Drawing.Point(6, 22);
            this.jtvSwtCny.Name = "jtvSwtCny";
            this.jtvSwtCny.Size = new System.Drawing.Size(391, 139);
            this.jtvSwtCny.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "SWT/CNY";
            // 
            // lblSwtCnyIndex
            // 
            this.lblSwtCnyIndex.AutoSize = true;
            this.lblSwtCnyIndex.Location = new System.Drawing.Point(71, 7);
            this.lblSwtCnyIndex.Name = "lblSwtCnyIndex";
            this.lblSwtCnyIndex.Size = new System.Drawing.Size(17, 12);
            this.lblSwtCnyIndex.TabIndex = 1;
            this.lblSwtCnyIndex.Text = "-1";
            // 
            // chkRefreshOrderBook
            // 
            this.chkRefreshOrderBook.AutoSize = true;
            this.chkRefreshOrderBook.Checked = true;
            this.chkRefreshOrderBook.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRefreshOrderBook.Location = new System.Drawing.Point(173, 17);
            this.chkRefreshOrderBook.Name = "chkRefreshOrderBook";
            this.chkRefreshOrderBook.Size = new System.Drawing.Size(84, 16);
            this.chkRefreshOrderBook.TabIndex = 3;
            this.chkRefreshOrderBook.Text = "Refreshing";
            this.chkRefreshOrderBook.UseVisualStyleBackColor = true;
            // 
            // btnOrderBooksStub
            // 
            this.btnOrderBooksStub.Location = new System.Drawing.Point(7, 7);
            this.btnOrderBooksStub.Name = "btnOrderBooksStub";
            this.btnOrderBooksStub.Size = new System.Drawing.Size(160, 35);
            this.btnOrderBooksStub.TabIndex = 0;
            this.btnOrderBooksStub.Text = "CreateOrderBooksStub";
            this.btnOrderBooksStub.UseVisualStyleBackColor = true;
            this.btnOrderBooksStub.Click += new System.EventHandler(this.btnOrderBooksStub_Click);
            // 
            // tabAccountStub
            // 
            this.tabAccountStub.Controls.Add(this.ovAccountStub);
            this.tabAccountStub.Controls.Add(this.jtvAccountStub);
            this.tabAccountStub.Controls.Add(this.label11);
            this.tabAccountStub.Controls.Add(this.lblAccountStubIndex);
            this.tabAccountStub.Controls.Add(this.chkRefreshAccountStub);
            this.tabAccountStub.Controls.Add(this.txtAccountStubAccount);
            this.tabAccountStub.Controls.Add(this.label10);
            this.tabAccountStub.Controls.Add(this.btnCreateAccountStub);
            this.tabAccountStub.Location = new System.Drawing.Point(4, 25);
            this.tabAccountStub.Name = "tabAccountStub";
            this.tabAccountStub.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccountStub.Size = new System.Drawing.Size(429, 381);
            this.tabAccountStub.TabIndex = 3;
            this.tabAccountStub.Text = "AccountStub";
            this.tabAccountStub.UseVisualStyleBackColor = true;
            // 
            // ovAccountStub
            // 
            this.ovAccountStub.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ovAccountStub.Location = new System.Drawing.Point(8, 245);
            this.ovAccountStub.Name = "ovAccountStub";
            this.ovAccountStub.Size = new System.Drawing.Size(408, 130);
            this.ovAccountStub.TabIndex = 7;
            // 
            // jtvAccountStub
            // 
            this.jtvAccountStub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jtvAccountStub.Location = new System.Drawing.Point(8, 108);
            this.jtvAccountStub.Name = "jtvAccountStub";
            this.jtvAccountStub.Size = new System.Drawing.Size(408, 131);
            this.jtvAccountStub.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 93);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "AccountSubIndex:";
            // 
            // lblAccountStubIndex
            // 
            this.lblAccountStubIndex.AutoSize = true;
            this.lblAccountStubIndex.Location = new System.Drawing.Point(141, 93);
            this.lblAccountStubIndex.Name = "lblAccountStubIndex";
            this.lblAccountStubIndex.Size = new System.Drawing.Size(17, 12);
            this.lblAccountStubIndex.TabIndex = 5;
            this.lblAccountStubIndex.Text = "-1";
            // 
            // chkRefreshAccountStub
            // 
            this.chkRefreshAccountStub.AutoSize = true;
            this.chkRefreshAccountStub.Checked = true;
            this.chkRefreshAccountStub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRefreshAccountStub.Location = new System.Drawing.Point(177, 61);
            this.chkRefreshAccountStub.Name = "chkRefreshAccountStub";
            this.chkRefreshAccountStub.Size = new System.Drawing.Size(84, 16);
            this.chkRefreshAccountStub.TabIndex = 3;
            this.chkRefreshAccountStub.Text = "Refreshing";
            this.chkRefreshAccountStub.UseVisualStyleBackColor = true;
            // 
            // txtAccountStubAccount
            // 
            this.txtAccountStubAccount.Location = new System.Drawing.Point(8, 19);
            this.txtAccountStubAccount.Name = "txtAccountStubAccount";
            this.txtAccountStubAccount.Size = new System.Drawing.Size(415, 21);
            this.txtAccountStubAccount.TabIndex = 2;
            this.txtAccountStubAccount.Text = "jB7rxgh43ncbTX4WeMoeadiGMfmfqY2xLZ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "Account:";
            // 
            // btnCreateAccountStub
            // 
            this.btnCreateAccountStub.Location = new System.Drawing.Point(6, 46);
            this.btnCreateAccountStub.Name = "btnCreateAccountStub";
            this.btnCreateAccountStub.Size = new System.Drawing.Size(152, 35);
            this.btnCreateAccountStub.TabIndex = 0;
            this.btnCreateAccountStub.Text = "CreateAccountStub";
            this.btnCreateAccountStub.UseVisualStyleBackColor = true;
            this.btnCreateAccountStub.Click += new System.EventHandler(this.btnCreateAccountStub_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.txtException);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(403, 448);
            this.splitContainer2.SplitterDistance = 106;
            this.splitContainer2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Exception:";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.jtvResultMessage);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.label3);
            this.splitContainer3.Panel2.Controls.Add(this.ovResult);
            this.splitContainer3.Size = new System.Drawing.Size(403, 338);
            this.splitContainer3.SplitterDistance = 162;
            this.splitContainer3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Json Message:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Result:";
            // 
            // ovResult
            // 
            this.ovResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ovResult.Location = new System.Drawing.Point(0, 26);
            this.ovResult.Name = "ovResult";
            this.ovResult.Size = new System.Drawing.Size(400, 134);
            this.ovResult.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 448);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlRemote.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabRequests.ResumeLayout(false);
            this.tabControlRequests.ResumeLayout(false);
            this.tabRequestServerInfo.ResumeLayout(false);
            this.tabRequestLedgerClosed.ResumeLayout(false);
            this.tabRequestLedger.ResumeLayout(false);
            this.tabRequestTx.ResumeLayout(false);
            this.tabRequestAccountInfo.ResumeLayout(false);
            this.tabRequestAccountTums.ResumeLayout(false);
            this.tabRequestAccountRelations.ResumeLayout(false);
            this.tabRequestAccountOffers.ResumeLayout(false);
            this.tabRequestAccountTx.ResumeLayout(false);
            this.tabRequestOrderBook.ResumeLayout(false);
            this.tabPathFindRequest.ResumeLayout(false);
            this.tabControlPathFindRequest.ResumeLayout(false);
            this.tabRequestPathFind.ResumeLayout(false);
            this.tabTransactions.ResumeLayout(false);
            this.tabControlTransactions.ResumeLayout(false);
            this.tabBuildPaymentTx.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.tabBuildRelationTx.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.tabBuildAccountSetTx.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.tabBuildOfferCreateTx.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.tabBuildOfferCancelTx.ResumeLayout(false);
            this.splitContainer9.Panel1.ResumeLayout(false);
            this.splitContainer9.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).EndInit();
            this.splitContainer9.ResumeLayout(false);
            this.DeployContractTx.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tabCallContractTx.ResumeLayout(false);
            this.splitContainer12.Panel1.ResumeLayout(false);
            this.splitContainer12.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).EndInit();
            this.splitContainer12.ResumeLayout(false);
            this.tabBuildSignTx.ResumeLayout(false);
            this.splitContainer10.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).EndInit();
            this.splitContainer10.ResumeLayout(false);
            this.tabEvents.ResumeLayout(false);
            this.tabControlEvents.ResumeLayout(false);
            this.tabTransactionsEvent.ResumeLayout(false);
            this.tabTransactionsEvent.PerformLayout();
            this.tabLedgerClosedEvent.ResumeLayout(false);
            this.tabLedgerClosedEvent.PerformLayout();
            this.splitContainer11.Panel1.ResumeLayout(false);
            this.splitContainer11.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).EndInit();
            this.splitContainer11.ResumeLayout(false);
            this.tabOrderBooksStub.ResumeLayout(false);
            this.tabOrderBooksStub.PerformLayout();
            this.tabCreateOrderBooksStub.ResumeLayout(false);
            this.tabCNYSWT.ResumeLayout(false);
            this.tabCNYSWT.PerformLayout();
            this.tabSWTCNY.ResumeLayout(false);
            this.tabSWTCNY.PerformLayout();
            this.tabAccountStub.ResumeLayout(false);
            this.tabAccountStub.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtException;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private JsonViewer jtvResultMessage;
        private ObjectViewer ovResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControlRemote;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnWalletGenerate;
        private System.Windows.Forms.Button btnFromSecret;
        private System.Windows.Forms.TextBox txtServerUrlsList;
        private System.Windows.Forms.TextBox txtServerUrl;
        private System.Windows.Forms.CheckBox chkLocalSign;
        private System.Windows.Forms.Label lblServerUrl;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabPage tabRequests;
        private System.Windows.Forms.TabControl tabControlRequests;
        private System.Windows.Forms.TabPage tabRequestServerInfo;
        private System.Windows.Forms.Button btnRequestServerInfo;
        private System.Windows.Forms.TabPage tabRequestLedgerClosed;
        private System.Windows.Forms.Button btnRequestLedgerClosed;
        private System.Windows.Forms.TabPage tabRequestLedger;
        private System.Windows.Forms.Button btnResetRequestLedgerOptions;
        private System.Windows.Forms.PropertyGrid pgRequestLedgerOptions;
        private System.Windows.Forms.Button btnRequestLedger;
        private System.Windows.Forms.TabPage tabRequestTx;
        private System.Windows.Forms.Button btnResetRequestTxOptions;
        private System.Windows.Forms.Button btnRequestTx;
        private System.Windows.Forms.PropertyGrid pgRequestTxOptions;
        private System.Windows.Forms.TabPage tabRequestAccountInfo;
        private System.Windows.Forms.PropertyGrid pgRequestAccountInfoOptions;
        private System.Windows.Forms.Button btnResetRequestAccountInfoOptions;
        private System.Windows.Forms.Button btnRequestAccountInfo;
        private System.Windows.Forms.TabPage tabRequestAccountTums;
        private System.Windows.Forms.Button btnResetRequestAccountTumsOptions;
        private System.Windows.Forms.Button btnRequestAccountTums;
        private System.Windows.Forms.PropertyGrid pgRequestAccountTumsOptions;
        private System.Windows.Forms.TabPage tabRequestAccountRelations;
        private System.Windows.Forms.PropertyGrid pgRequestAccountRelationsOptions;
        private System.Windows.Forms.Button btnResetRequestAccountRelationsOptions;
        private System.Windows.Forms.Button btnRequestAccountRelations;
        private System.Windows.Forms.TabPage tabRequestAccountOffers;
        private System.Windows.Forms.Button btnResetRequestAccountOffersOptions;
        private System.Windows.Forms.Button btnRequestAccountOffers;
        private System.Windows.Forms.PropertyGrid pgRequestAccountOffersOptions;
        private System.Windows.Forms.TabPage tabRequestAccountTx;
        private System.Windows.Forms.PropertyGrid pgRequestAccountTxOptions;
        private System.Windows.Forms.Button btnResetRequestAccountTxOptions;
        private System.Windows.Forms.Button btnRequestAccountTx;
        private System.Windows.Forms.TabPage tabRequestOrderBook;
        private System.Windows.Forms.Button btnResetRequestOrderBookOptions;
        private System.Windows.Forms.Button btnRequestOrderBook;
        private System.Windows.Forms.PropertyGrid pgRequestOrderBookOptions;
        private System.Windows.Forms.TabPage tabPathFindRequest;
        private System.Windows.Forms.TabControl tabControlPathFindRequest;
        private System.Windows.Forms.TabPage tabRequestPathFind;
        private System.Windows.Forms.PropertyGrid pgRequestPathFindOptions;
        private System.Windows.Forms.Button btnResetRequestPathFindOptions;
        private System.Windows.Forms.Button btnRequestPathFind;
        private System.Windows.Forms.TabPage tabTransactions;
        private System.Windows.Forms.TabControl tabControlTransactions;
        private System.Windows.Forms.TabPage tabBuildPaymentTx;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.PropertyGrid pgBuildPaymentTxOptions;
        private System.Windows.Forms.PropertyGrid pgBuildPaymentTxSettings;
        private System.Windows.Forms.Button btnResetBuildPaymentTxOptions;
        private System.Windows.Forms.Button btnBuildPaymentTx;
        private System.Windows.Forms.TabPage tabBuildRelationTx;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.PropertyGrid pgBuildRelationTxOptions;
        private System.Windows.Forms.PropertyGrid pgBuildRelationTxSettings;
        private System.Windows.Forms.Button btnResetBuildRelationTxOptions;
        private System.Windows.Forms.Button btnBuildRelationTx;
        private System.Windows.Forms.TabPage tabBuildAccountSetTx;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.PropertyGrid pgBuildAccountSetTxOptions;
        private System.Windows.Forms.PropertyGrid pgBuildAccountSetTxSettings;
        private System.Windows.Forms.Button btnResetBuildAccountSetTxOptions;
        private System.Windows.Forms.Button btnBuildAccountSetTx;
        private System.Windows.Forms.TabPage tabBuildOfferCreateTx;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.PropertyGrid pgBuildOfferCreateTxOptions;
        private System.Windows.Forms.PropertyGrid pgBuildOfferCreateTxSettings;
        private System.Windows.Forms.Button btnResetBuildOfferCreateTxOptions;
        private System.Windows.Forms.Button btnBuildOfferCreateTx;
        private System.Windows.Forms.TabPage tabBuildOfferCancelTx;
        private System.Windows.Forms.SplitContainer splitContainer9;
        private System.Windows.Forms.PropertyGrid pgBuildOfferCancelTxOptions;
        private System.Windows.Forms.PropertyGrid pgBuildOfferCancelTxSettings;
        private System.Windows.Forms.Button btnResetBuildOfferCancelTxOptions;
        private System.Windows.Forms.Button btnBuildOfferCancelTx;
        private System.Windows.Forms.TabPage DeployContractTx;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.PropertyGrid pgDeployContractTxOptions;
        private System.Windows.Forms.PropertyGrid pgDeployContractTxSettings;
        private System.Windows.Forms.Button btnResetDeployContractTxOptions;
        private System.Windows.Forms.Button btnDeployContractTx;
        private System.Windows.Forms.TabPage tabCallContractTx;
        private System.Windows.Forms.SplitContainer splitContainer12;
        private System.Windows.Forms.PropertyGrid pgCallContractTxOptions;
        private System.Windows.Forms.PropertyGrid pgCallContractTxSettings;
        private System.Windows.Forms.Button btnResetCallContractTxOptions;
        private System.Windows.Forms.Button btnCallContractTx;
        private System.Windows.Forms.TabPage tabBuildSignTx;
        private System.Windows.Forms.SplitContainer splitContainer10;
        private System.Windows.Forms.PropertyGrid pgBuildSignTxOptions;
        private System.Windows.Forms.Button btnResetBuildSignTxOptions;
        private System.Windows.Forms.Button btnBuildSignTx;
        private System.Windows.Forms.TabPage tabEvents;
        private System.Windows.Forms.TabControl tabControlEvents;
        private System.Windows.Forms.TabPage tabTransactionsEvent;
        private ObjectViewer ovTransactionsMessage;
        private JsonViewer jtvTransactionsMessage;
        private System.Windows.Forms.CheckBox chkRefreshTransactions;
        private System.Windows.Forms.Button btnOnTransactions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUnTransactions;
        private System.Windows.Forms.Label lblTransactionsIndex;
        private System.Windows.Forms.TabPage tabLedgerClosedEvent;
        private System.Windows.Forms.SplitContainer splitContainer11;
        private JsonViewer jtvLedgerClosedMessage;
        private ObjectViewer ovLedgerClosedMessage;
        private System.Windows.Forms.CheckBox chkRefreshLedgerClosed;
        private System.Windows.Forms.Button btnOnLedgerClosed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnUnLedgerClosed;
        private System.Windows.Forms.Label lblLedgerClosedIndex;
        private System.Windows.Forms.TabPage tabOrderBooksStub;
        private System.Windows.Forms.TabControl tabCreateOrderBooksStub;
        private System.Windows.Forms.TabPage tabCNYSWT;
        private ObjectViewer ovCnySwt;
        private JsonViewer jtvCnySwt;
        private System.Windows.Forms.Label lblCnySwtIndex;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabSWTCNY;
        private ObjectViewer ovSwtCny;
        private JsonViewer jtvSwtCny;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSwtCnyIndex;
        private System.Windows.Forms.CheckBox chkRefreshOrderBook;
        private System.Windows.Forms.Button btnOrderBooksStub;
        private System.Windows.Forms.TabPage tabAccountStub;
        private ObjectViewer ovAccountStub;
        private JsonViewer jtvAccountStub;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAccountStubIndex;
        private System.Windows.Forms.CheckBox chkRefreshAccountStub;
        private System.Windows.Forms.TextBox txtAccountStubAccount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCreateAccountStub;
    }
}

