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
    public partial class SelectForm : Form
    {
        public SelectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selected = listView1.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if(selected == null)
            {
                MessageBox.Show("Please select one item.");
                return;
            }

            SelectedValue = selected.Text;
            DialogResult = DialogResult.OK;
        }

        public string SelectedValue { get; set; }

        public DialogResult ShowDialog(IEnumerable<KeyValuePair<string,string>> list)
        {
            listView1.Items.Clear();
            foreach(var item in list)
            {
                var value = item.Key ?? "";
                var remark = item.Value ?? "";
                var listItem = new ListViewItem();
                listItem.SubItems[0].Text = value;
                listItem.SubItems.Add(remark);
                listView1.Items.Add(listItem);
            }

            return ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
