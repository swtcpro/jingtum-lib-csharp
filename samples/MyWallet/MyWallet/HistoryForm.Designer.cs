namespace MyWallet
{
    partial class HistoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.cDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cCounterParty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cCurrency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cIssuer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cMemos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cDate,
            this.cResult,
            this.cType,
            this.cCounterParty,
            this.cAmount,
            this.cCurrency,
            this.cIssuer,
            this.cMemos});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(800, 450);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // cDate
            // 
            this.cDate.Text = "Date";
            // 
            // cResult
            // 
            this.cResult.DisplayIndex = 6;
            this.cResult.Text = "Result";
            // 
            // cType
            // 
            this.cType.DisplayIndex = 1;
            this.cType.Text = "Type";
            // 
            // cCounterParty
            // 
            this.cCounterParty.DisplayIndex = 2;
            this.cCounterParty.Text = "Counter Party";
            // 
            // cAmount
            // 
            this.cAmount.DisplayIndex = 3;
            this.cAmount.Text = "Amount";
            // 
            // cCurrency
            // 
            this.cCurrency.DisplayIndex = 4;
            this.cCurrency.Text = "Currency";
            // 
            // cIssuer
            // 
            this.cIssuer.DisplayIndex = 5;
            this.cIssuer.Text = "Issuer";
            // 
            // cMemos
            // 
            this.cMemos.Text = "Memos";
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Name = "HistoryForm";
            this.Text = "HistoryForm";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader cDate;
        private System.Windows.Forms.ColumnHeader cResult;
        private System.Windows.Forms.ColumnHeader cType;
        private System.Windows.Forms.ColumnHeader cCounterParty;
        private System.Windows.Forms.ColumnHeader cAmount;
        private System.Windows.Forms.ColumnHeader cCurrency;
        private System.Windows.Forms.ColumnHeader cIssuer;
        private System.Windows.Forms.ColumnHeader cMemos;
    }
}