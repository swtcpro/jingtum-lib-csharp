namespace TestApiApp
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
            this.btnGetBalances = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.lvBalances = new System.Windows.Forms.ListView();
            this.Currency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Issuer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Freezed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.lvBuy = new System.Windows.Forms.ListView();
            this.btnConnect = new System.Windows.Forms.Button();
            this.Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueSWTC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvSell = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnGetBalances
            // 
            this.btnGetBalances.Location = new System.Drawing.Point(161, 72);
            this.btnGetBalances.Name = "btnGetBalances";
            this.btnGetBalances.Size = new System.Drawing.Size(135, 42);
            this.btnGetBalances.TabIndex = 0;
            this.btnGetBalances.Text = "GetBalances";
            this.btnGetBalances.UseVisualStyleBackColor = true;
            this.btnGetBalances.Click += new System.EventHandler(this.btnGetBalances_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Url:";
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Location = new System.Drawing.Point(106, 13);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.Size = new System.Drawing.Size(315, 21);
            this.txtServerUrl.TabIndex = 2;
            this.txtServerUrl.Text = "wss://s.jingtum.com:5020";
            // 
            // lvBalances
            // 
            this.lvBalances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Currency,
            this.Issuer,
            this.Value,
            this.Freezed});
            this.lvBalances.Location = new System.Drawing.Point(27, 120);
            this.lvBalances.Name = "lvBalances";
            this.lvBalances.Size = new System.Drawing.Size(473, 303);
            this.lvBalances.TabIndex = 3;
            this.lvBalances.UseCompatibleStateImageBehavior = false;
            this.lvBalances.View = System.Windows.Forms.View.Details;
            // 
            // Currency
            // 
            this.Currency.Text = "Currency";
            // 
            // Issuer
            // 
            this.Issuer.Text = "Issuer";
            this.Issuer.Width = 227;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 75;
            // 
            // Freezed
            // 
            this.Freezed.Text = "Freezed";
            this.Freezed.Width = 71;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Account:";
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(106, 45);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(315, 21);
            this.txtAccount.TabIndex = 2;
            this.txtAccount.Text = "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT";
            // 
            // lvBuy
            // 
            this.lvBuy.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.ValueSWTC,
            this.Price});
            this.lvBuy.Location = new System.Drawing.Point(506, 12);
            this.lvBuy.Name = "lvBuy";
            this.lvBuy.Size = new System.Drawing.Size(436, 196);
            this.lvBuy.TabIndex = 4;
            this.lvBuy.UseCompatibleStateImageBehavior = false;
            this.lvBuy.View = System.Windows.Forms.View.Details;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(27, 72);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(128, 42);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Index
            // 
            this.Index.Text = "#";
            // 
            // ValueSWTC
            // 
            this.ValueSWTC.Text = "Value (SWTC)";
            this.ValueSWTC.Width = 116;
            // 
            // Price
            // 
            this.Price.Text = "Price (CNT)";
            this.Price.Width = 141;
            // 
            // lvSell
            // 
            this.lvSell.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvSell.Location = new System.Drawing.Point(506, 214);
            this.lvSell.Name = "lvSell";
            this.lvSell.Size = new System.Drawing.Size(436, 209);
            this.lvSell.TabIndex = 4;
            this.lvSell.UseCompatibleStateImageBehavior = false;
            this.lvSell.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value (SWTC)";
            this.columnHeader2.Width = 133;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Price (CNT)";
            this.columnHeader3.Width = 128;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 435);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lvSell);
            this.Controls.Add(this.lvBuy);
            this.Controls.Add(this.lvBalances);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.txtServerUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetBalances);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetBalances;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerUrl;
        private System.Windows.Forms.ListView lvBalances;
        private System.Windows.Forms.ColumnHeader Currency;
        private System.Windows.Forms.ColumnHeader Issuer;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.ColumnHeader Freezed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.ListView lvBuy;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader ValueSWTC;
        private System.Windows.Forms.ColumnHeader Price;
        private System.Windows.Forms.ListView lvSell;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

