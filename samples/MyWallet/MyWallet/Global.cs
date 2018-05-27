using JingTum.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWallet
{
    internal static class Global
    {

        public const int Timeout = 1000 * 60;
        public const string Currency = "SWT";
        public const string AmountFormat = "0.######";
        public const double FreezedReserved = 20.0;
        public const double FreezedEachFreezed = 5.0;

        private static Remote _remote;
        private static string _account;

        public static string Account
        {
            get
            {
                return Wallet?.Address ?? _account;
            }
            set
            {
                _account = value;
            }
        }

        public static Wallet Wallet
        {
            get;set;
        }

        public static Remote Remote
        {
            get
            {
                return _remote;
            }
            set
            {
                if(_remote != null)
                {
                    _remote.Disconnected -= RemoteDisconnected;
                    _remote.Transactions -= HandleRemoteTransactions;
                }

                _remote = value;
                if(_remote != null)
                {
                    _remote.Disconnected += RemoteDisconnected;
                    _remote.Transactions += HandleRemoteTransactions;
                }
            }
        }

        public static event EventHandler<TransactionsEventArgs> RemoteTransactions;
        private static void HandleRemoteTransactions(object sender, TransactionsEventArgs e)
        {
            RemoteTransactions?.Invoke(sender, e);
        }

        private static void RemoteDisconnected(object sender, EventArgs e)
        {
            MessageBox.Show("Server is disconnected. Please try to connect the server again.");
            Config();
        }

        public static ConnectResponse Connection
        {
            get; set;
        }

        public static void Config()
        {
            var form = new ConfigForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                OnConfigRefreshed();
            }
            else
            {
                if(Remote == null || !Remote.IsConnected || Connection == null)
                {
                    Application.Exit();
                }
            }
        }

        public static event EventHandler ConfigRefreshed;
        private static void OnConfigRefreshed()
        {
            ConfigRefreshed?.Invoke(Remote, new EventArgs());
        }

        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
