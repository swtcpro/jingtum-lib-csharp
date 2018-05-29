using JingTum.Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWallet
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            if(Global.Remote != null)
            {
                textBox1.Text = Global.Remote.Url;
            }
#if DEBUG
            else
            {
                textBox1.Text = "ws://123.57.219.57:5020";
            }
#endif

            if (Global.Wallet != null)
            {
                textBox2.Text = Global.Wallet.Secret;
            }
#if DEBUG
            else
            {
                //textBox2.Text = "snyGVDPMPBrPA3rKWFX8G5S5syBvf";
            }
#endif

            if (Global.Account != null)
            {
                textBox3.Text = Global.Account;
            }
#if DEBUG
            else
            {
                textBox3.Text = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            }
#endif
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var secret = textBox2.Text;
            var address = textBox3.Text;
            var url = textBox1.Text;
            if(string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Please input host url.");
                return;
            }

            try
            {
                Wallet wallet = null;
                if (!string.IsNullOrEmpty(secret))
                {
                    wallet = Wallet.FromSecret(secret);
                    Global.Wallet = wallet;
                }
                else if(string.IsNullOrEmpty(address))
                {
                    MessageBox.Show("Please input account address.");
                    return;
                }
                else
                {
                    Global.Account = address;
                }

                var remote = new Remote(url, false);
                var task = new Task(() => { });
                Exception connectEx = null;
                remote.Connect(result=> {
                    if (result.Exception != null)
                    {
                        connectEx = result.Exception;
                    }
                    else
                    {
                        Global.Connection = result.Result;
                    }
                    task.Start();
                });

                task.Wait();
                if(connectEx != null)
                {
                    throw connectEx;
                }

                var old = Global.Remote;
                Global.Remote = remote;
                old?.Dispose();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(@"ws://123.57.219.57:5020", @"test"),
                new KeyValuePair<string, string>(@"ws://ts5.jingtum.com:5020", @"test"),
                new KeyValuePair<string, string>(@"wws://139.129.194.175:5020", @"test contract"),
                new KeyValuePair<string, string>(@"wss://s.jingtum.com:5020", @"real"),
                new KeyValuePair<string, string>(@"wss://c05.jingtum.com:5020", @"real"),
            };

            var selectForm = new SelectForm();
            if(selectForm.ShowDialog(list) == DialogResult.OK)
            {
                textBox1.Text = selectForm.SelectedValue;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(@"j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", @"test"),
                new KeyValuePair<string, string>(@"jaEuc5ito3DyhnYoSSJev6G5Gm6CU71gC4", @"real"),
                new KeyValuePair<string, string>(@"jB9eHCFeCaoxw6d9V9pBx5hiKUGW9K2fbs", @"real"),
            };

            var selectForm = new SelectForm();
            if (selectForm.ShowDialog(list) == DialogResult.OK)
            {
                textBox3.Text = selectForm.SelectedValue;
            }

        }
    }
}
