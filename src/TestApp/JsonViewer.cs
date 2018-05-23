using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace TestApp
{
    public partial class JsonViewer : UserControl
    {
        public JsonViewer()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;

                treeView1.Nodes.Clear();
                if (!string.IsNullOrEmpty(value))
                {
                    var jObj = Newtonsoft.Json.JsonConvert.DeserializeObject(value) as JObject;
                    var node = Json2Tree(jObj);
                    treeView1.Nodes.Add(node);
                }
            }
        }

        public void ShowJson(string json)
        {
            Text = json;
        }

        private TreeNode Json2Tree(JObject obj)
        {
            //create the parent node
            TreeNode parent = new TreeNode();
            parent.Text = "root";

            //loop through the obj. all token should be pair<key, value>
            foreach (var token in obj)
            {
                //create the child node
                TreeNode child = new TreeNode();
                child.Text = token.Key.ToString();
                //check if the value is of type obj recall the method
                if (token.Value.Type.ToString() == "Object")
                {
                    // child.Text = token.Key.ToString();
                    //create a new JObject using the the Token.value
                    JObject o = (JObject)token.Value;
                    //recall the method
                    child = Json2Tree(o);
                    child.Text = token.Key.ToString();
                    //add the child to the parentNode
                    parent.Nodes.Add(child);
                }
                //if type is of array
                else if (token.Value.Type.ToString() == "Array")
                {
                    int ix = -1;
                    //  child.Text = token.Key.ToString();
                    //loop though the array
                    foreach (var itm in token.Value)
                    {
                        //check if value is an Array of objects
                        if (itm.Type.ToString() == "Object")
                        {
                            TreeNode objTN = new TreeNode();
                            //child.Text = token.Key.ToString();
                            //call back the method
                            ix++;

                            JObject o = (JObject)itm;
                            objTN = Json2Tree(o);
                            objTN.Text = token.Key.ToString() + "[" + ix + "]";
                            child.Nodes.Add(objTN);
                            //parent.Nodes.Add(child);
                        }
                        //regular array string, int, etc
                        else if (itm.Type.ToString() == "Array")
                        {
                            ix++;
                            TreeNode dataArray = new TreeNode();
                            foreach (var data in itm)
                            {
                                dataArray.Text = token.Key.ToString() + "[" + ix + "]";
                                dataArray.Nodes.Add(data.ToString());
                            }
                            child.Nodes.Add(dataArray);
                        }

                        else
                        {
                            child.Nodes.Add(itm.ToString());
                        }
                    }
                    parent.Nodes.Add(child);
                }
                else if (token.Value.Type.ToString() == "String")
                {
                    child.Text += ": '" + token.Value.ToString() + "'";
                    parent.Nodes.Add(child);
                }
                else
                {
                    //if token.Value is not nested
                    // child.Text = token.Key.ToString();
                    //change the value into N/A if value == null or an empty string 
                    if (token.Value.ToString() == "")
                        child.Text += ": " + "N/A";
                    else
                        child.Text += ": " + token.Value.ToString();

                    parent.Nodes.Add(child);
                }
            }
            return parent;

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var node = treeView1.SelectedNode;
            copyToolStripMenuItem.Enabled = node != null;
            copyNameToolStripMenuItem.Enabled = copyValueToolStripMenuItem.Enabled = node != null && node.Text.Contains(":");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null) return;

            Clipboard.SetText(node.Text);
        }

        private void copyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null) return;

            Clipboard.SetText(node.Text.Split(':')[0].Trim());
        }

        private void copyValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null) return;

            Clipboard.SetText(node.Text.Split(':')[1].Trim(' ', '\''));
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Text);
        }
    }
}
