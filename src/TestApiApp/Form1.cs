using JingTum.Api;
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

namespace TestApiApp
{
    public partial class Form1 : Form
    {
        Remote _remote;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _remote = new Remote(txtServerUrl.Text);
            _remote.Connect(_=>
            {
                _remote.Transactions += _remote_Transactions;
                RefreshOffers();
            });
        }

        private void _remote_Transactions(object sender, TransactionsEventArgs e)
        {
            RefreshOffers();
        }

        private void btnGetBalances_Click(object sender, EventArgs e)
        {
            var options = new Balances.Options();
            options.Account = txtAccount.Text;
            Balances.GetBalance(_remote, options, r =>
            {
                UpdateBalances(r.Result);
            });
        }

        private void UpdateBalances(Balances.Response response)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Balances.Response>(UpdateBalancesImp), response);
            }
            else
            {
                UpdateBalancesImp(response);
            }
        }

        private void UpdateBalancesImp(Balances.Response response)
        {
            lvBalances.Items.Clear();
            foreach(var balance in response.Balances)
            {
                var item = new ListViewItem(balance.Currency);
                item.SubItems.Add(balance.Issuer);
                item.SubItems.Add(balance.Value);
                item.SubItems.Add(balance.Freezed);
                lvBalances.Items.Add(item);
            }
        }

        private void RefreshOffers()
        {
            var optionsBuy = new OrderBookOptions();
            optionsBuy.Limit = 10;
            optionsBuy.Gets = new Amount { Currency = "SWT", Issuer = "" };
            optionsBuy.Pays = new Amount { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" };
            _remote.RequestOrderBook(optionsBuy).Submit(r =>
            {
                RefreshOrderBook(lvBuy, r.Result);
            });

            var optionsSell = new OrderBookOptions();
            optionsSell.Limit = 10;
            optionsSell.Pays = new Amount { Currency = "SWT", Issuer = "" };
            optionsSell.Gets = new Amount { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" };
            _remote.RequestOrderBook(optionsSell).Submit(r =>
            {
                RefreshOrderBook(lvSell, r.Result);
            });
        }

        private void RefreshOrderBook(ListView lv, OrderBookResponse r)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ListView, OrderBookResponse>(RefreshOrderBookImp), lv, r);
            }
            else
            {
                RefreshOrderBookImp(lv, r);
            }
        }

        private void RefreshOrderBookImp(ListView lv, OrderBookResponse r)
        {
            lv.Items.Clear();
            var index = 1;
            foreach(var offer in r.Offers)
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
    }
}
