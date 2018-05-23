using System;
using System.Windows.Forms;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace TestApp
{
    public partial class ObjectViewer : UserControl
    {
        public ObjectViewer()
        {
            InitializeComponent();
        }

        public void ShowObject(object obj, int maxLevel = 10)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            Fill(treeView1.Nodes, "result", obj, maxLevel);
            treeView1.EndUpdate();
        }

        private void Fill(TreeNodeCollection nodes, object obj, int maxLevel)
        {
            if (maxLevel < 0) return;
            if (obj == null) return;

            foreach(var property in obj.GetType().GetProperties())
            {
                var name = property.Name;
                var value = property.GetValue(obj);
                Fill(nodes, name, value, maxLevel);
            }
        }

        private void Fill(TreeNodeCollection nodes, string name, object value, int maxLevel)
        {
            if (value == null)
            {
                nodes.Add(new TreeNode(name + ": [null]"));
            }
            else if(value.GetType() == typeof(DBNull))
            {
                nodes.Add(new TreeNode(name + ": [dbnull]"));
            }
            else if (value.GetType().IsValueType
                || value.GetType().Name.StartsWith("Nullable~"))
            {
                nodes.Add(new TreeNode(name + ": " + value.ToString()));
            }
            else if(value.GetType() == typeof(string))
            {
                nodes.Add(new TreeNode(name + ": \"" + value.ToString() + "\""));
            }
            else if(value is JObject)
            {
                nodes.Add(new TreeNode(name + ": '" + value.ToString() + "'"));
            }
            else if (typeof(IList).IsAssignableFrom(value.GetType()))
            {
                var node = new TreeNode(name + ": [Array]");
                nodes.Add(node);

                var list = value as IList;
                var index = 0;
                foreach (var item in list)
                {
                    Fill(node.Nodes, "[" + index++ + "]", item, maxLevel - 1);
                }
            }
            else
            {
                var node = new TreeNode(name + ": [" + value.ToString() + "]");
                nodes.Add(node);
                Fill(node.Nodes, value, maxLevel - 1);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null) return;

            Clipboard.SetText(node.Text);
        }
    }
}
