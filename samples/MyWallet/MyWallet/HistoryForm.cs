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
            Refresh();
        }

        private void Refresh()
        {
            var req = Global.Remote.RequestAccountTx(new AccountTxOptions
            {
                Account = Global.Account,
            });

            req.Submit(result =>
            {
                this.SafeInvoke(() => {
                    UpdateList(result.Result);
                });
            });
        }

        private void UpdateList(AccountTxResponse response)
        {
            listView1.Items.Clear();
            var transactions = response.Transactions ?? new TxResult[] { };
            var num = 1;
            foreach(var transaction in transactions)
            {
                var item = new ListViewItem();
                var ignore = false;
                var counterParty = "";
                var amount = "";
                var currency = "";
                var issuer = "";
                var type = transaction.Type.ToString();
                var desc = "";
                switch (transaction.Type)
                {
                    case TxResultType.OfferNew:
                        var offerNew = transaction as OfferNewTxResult;
                        if (offerNew.OfferType == OfferType.Sell)
                        {
                            amount = offerNew.Gets.Value;
                            currency = offerNew.Gets.Currency;
                            issuer = offerNew.Gets.Issuer;
                            type += "-Sell";
                            desc = $"Create sell offer {currency}:{amount}.";
                        }
                        else
                        {
                            amount = offerNew.Pays.Value;
                            currency = offerNew.Pays.Currency;
                            issuer = offerNew.Pays.Issuer;
                            type += "-Buy";
                            desc = $"Create buy offer {currency}:{amount}.";
                        }
                        item.BackColor = Color.LightGreen;
                        break;
                    case TxResultType.OfferCancel:
                        var offerCancel = transaction as OfferCancelTxResult;
                        if(offerCancel.Gets != null)
                        {
                            amount = offerCancel.Gets.Value;
                            currency = offerCancel.Gets.Currency;
                            issuer = offerCancel.Gets.Issuer;
                            type += "-Sell";
                        }
                        else if (offerCancel.Pays != null)
                        {
                            amount = offerCancel.Pays.Value;
                            currency = offerCancel.Pays.Currency;
                            issuer = offerCancel.Pays.Issuer;
                            type += "-Buy";
                        }
                        item.BackColor = Color.LightGray;
                        desc = $"Cancel offer {offerCancel.Hash}.";
                        break;
                    case TxResultType.Received:
                        var received = transaction as ReceivedTxResult;
                        amount = received.Amount.Value;
                        currency = received.Amount.Currency;
                        issuer = received.Amount.Issuer;
                        counterParty = received.CounterParty;
                        item.BackColor = Color.LightSkyBlue;
                        desc = $"Received {currency}:{amount} from {counterParty}.";
                        break;
                    case TxResultType.Sent:
                        var sent = transaction as SentTxResult;
                        amount = sent.Amount.Value;
                        currency = sent.Amount.Currency;
                        issuer = sent.Amount.Issuer;
                        counterParty = sent.CounterParty;
                        item.BackColor = Color.LightSkyBlue;
                        desc = $"Sent {currency}:{amount} to {counterParty}.";
                        break;
                    case TxResultType.OfferEffect:
                        item.BackColor = Color.LightPink;
                        var offerEffect = transaction as OfferEffectTxResult;
                        foreach(var nodeEffect in (offerEffect.Effects ?? new NodeEffect[] { }))
                        {
                            switch(nodeEffect.Effect)
                            {
                                case EffectType.OfferBought:
                                    var offerBought = nodeEffect as OfferBoughtEffect;
                                    counterParty = offerBought.CounterParty.Account;
                                    if (offerBought.Got != null)
                                    {
                                        amount = offerBought.Got.Value;
                                        currency = offerBought.Got.Currency;
                                        issuer = offerBought.Got.Issuer;
                                        desc = $"Trading success: got {currency}:{amount}.";
                                    }
                                    else if (offerBought.Paid != null)
                                    {
                                        amount = offerBought.Paid.Value;
                                        currency = offerBought.Paid.Currency;
                                        issuer = offerBought.Paid.Issuer;
                                        desc = $"Trading success: paid {currency}:{amount}.";
                                    }
                                    break;
                                case EffectType.OfferFunded:
                                    var offerFunded = nodeEffect as OfferFundedEffect;
                                    counterParty = offerFunded.CounterParty.Account;
                                    if (offerFunded.Got != null)
                                    {
                                        amount = offerFunded.Got.Value;
                                        currency = offerFunded.Got.Currency;
                                        issuer = offerFunded.Got.Issuer;
                                        desc = $"Trading success: got {currency}:{amount}.";
                                    }
                                    else if (offerFunded.Paid != null)
                                    {
                                        amount = offerFunded.Paid.Value;
                                        currency = offerFunded.Paid.Currency;
                                        issuer = offerFunded.Paid.Issuer;
                                        desc = $"Trading success: paid {currency}:{amount}.";
                                    }
                                    break;
                                case EffectType.OfferPartiallyFunded:
                                    var offerPartiallyFunded = nodeEffect as OfferPartiallyFundedEffect;
                                    counterParty = offerPartiallyFunded.CounterParty.Account;
                                    if (offerPartiallyFunded.Got != null)
                                    {
                                        amount = offerPartiallyFunded.Got.Value;
                                        currency = offerPartiallyFunded.Got.Currency;
                                        issuer = offerPartiallyFunded.Got.Issuer;
                                        desc = $"Complete part of the deal: got {currency}:{amount}.";
                                    }
                                    else if (offerPartiallyFunded.Paid != null)
                                    {
                                        amount = offerPartiallyFunded.Paid.Value;
                                        currency = offerPartiallyFunded.Paid.Currency;
                                        issuer = offerPartiallyFunded.Paid.Issuer;
                                        desc = $"Complete part of the deal: paid {currency}:{amount}.";
                                    }

                                    if(!offerPartiallyFunded.Remaining)
                                    {
                                        desc += " No remaining.";
                                    }
                                    break;
                                default:
                                    ignore = true;
                                    break;
                            }
                        }
                        break;
                    default:
                        ignore = true;
                        break;
                }

                if(ignore)
                {
                    continue;
                }

                var date = transaction.Date.ToString("yyyy/MM/dd hh:mm");
                var memos = string.Join(",", transaction.Memos ?? new string[] { });
                if(!string.IsNullOrEmpty(memos))
                {
                    desc += " " + memos;
                }

                var fee = transaction.Fee ?? "";
                item.SubItems[0].Text = (num++).ToString();
                item.SubItems.Add(type);
                item.SubItems.Add(date);
                item.SubItems.Add(desc);
                item.SubItems.Add(counterParty);
                item.SubItems.Add(amount);
                item.SubItems.Add(currency);
                item.SubItems.Add(issuer);
                item.SubItems.Add(fee);
                item.SubItems.Add(memos);
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
