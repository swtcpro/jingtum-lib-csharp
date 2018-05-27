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

namespace MyWallet
{
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            var req = Global.Remote.RequestAccountTx(new AccountTxOptions
            {
                Account = Global.Account,
            });

            req.Submit(result =>
            {
                this.SafeInvoke(()=> {
                    UpdateList(result.Result);
                });
            });
        }

        private void UpdateList(AccountTxResponse response)
        {
            listView1.Items.Clear();
            var transactions = response.Transactions ?? new TxResult[] { };
            foreach(var transaction in transactions)
            {
                var item = new ListViewItem();
                var date = transaction.Date.ToString("yyyy/MM/dd hh:mm");
                item.SubItems.Add(date);
                var type = transaction.Type.ToString();
                item.SubItems.Add(type);
                var counterParty = "";
                item.SubItems.Add(counterParty);
                var amount = "";
                item.SubItems.Add(amount);
                var currency = "";
                item.SubItems.Add(currency);
                var issuer = "";
                item.SubItems.Add(issuer);
                var result = "";
                item.SubItems.Add(result);
                var memos = "";
                item.SubItems.Add(memos);
                listView1.Items.Add(item);
            }
        }
    }
}
