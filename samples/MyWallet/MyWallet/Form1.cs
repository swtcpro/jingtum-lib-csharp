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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Global.ConfigRefreshed += GlobalConfigRefreshed;
            Global.RemoteTransactions += GlobalRemoteTransactions;
            Global.Config();
        }

        private void GlobalRemoteTransactions(object sender, TransactionsEventArgs e)
        {
            RefreshOffers();
        }

        private void RefreshOffers()
        {
            var optionsBuy = new OrderBookOptions();
            optionsBuy.Limit = 10;
            optionsBuy.Gets = new Amount { Currency = "SWT", Issuer = "" };
            optionsBuy.Pays = new Amount { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" };
            Global.Remote.RequestOrderBook(optionsBuy).Submit(r =>
            {
                this.SafeInvoke(() => RefreshOrderBook(lvBuy, r.Result));
            });

            var optionsSell = new OrderBookOptions();
            optionsSell.Limit = 10;
            optionsSell.Pays = new Amount { Currency = "SWT", Issuer = "" };
            optionsSell.Gets = new Amount { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" };
            Global.Remote.RequestOrderBook(optionsSell).Submit(r =>
            {
                this.SafeInvoke( ()=> RefreshOrderBook(lvSell, r.Result));
            });
        }

        private void RefreshOrderBook(ListView lv, OrderBookResponse r)
        {
            lv.Items.Clear();
            var index = 1;
            foreach (var offer in r.Offers)
            {
                var text = (offer.IsSell ? "Sell " : "Buy ") + (index++).ToString();
                var value = offer.IsSell ? offer.TakerGets.Value : offer.TakerPays.Value;
                var price = offer.Price;
                var item = new ListViewItem(text);
                item.SubItems.Add(value);
                item.SubItems.Add(price);
                lv.Items.Add(item);
            }
        }

        private void GlobalConfigRefreshed(object sender, EventArgs e)
        {
            label4.Text = "Current account: " + Global.Account;
            var req = Global.Remote.RequestAccountInfo(new AccountInfoOptions { Account = Global.Account });
            req.Submit(result =>{
                RefreshBalances();
            });

            RefreshOffers();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.Config();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshBalances();
        }

        private void RefreshBalances()
        {
            var options = new Balances.Options();
            options.Account = Global.Account;
            Balances.GetBalance(Global.Remote, options, r =>
            {
                this.SafeInvoke(()=>UpdateBalances(r.Result));
            });
        }

        private void UpdateBalances(Balances.Response result)
        {
            lvBalances.Items.Clear();
            foreach (var balance in result.Balances)
            {
                var item = new ListViewItem(balance.Currency);
                item.SubItems.Add(balance.Issuer);
                item.SubItems.Add(balance.Value);
                item.SubItems.Add(balance.Freezed);
                lvBalances.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new HistoryForm()).ShowDialog();
        }
    }
}
