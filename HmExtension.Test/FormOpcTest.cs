using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using AntdUI;
using GodSharp.Opc.Da;
using GodSharp.Opc.Da.Options;
using HmExtension.Opc;
using HmExtension.Opc.da;
using HmExtension.Opc.ua;
using Opc.Ua;
using TouchSocket.Sockets;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using IClient = HmExtension.Opc.IClient;
using Message = AntdUI.Message;
using TreeNode = System.Windows.Forms.TreeNode;

namespace HmExtension.Test
{
    public partial class FormOpcTest : Form
    {
        private IClient client;

        public string Host
        {
            get => input1.Text;
            set => input1.Text = value;
        }

        public string Server
        {
            get => select1.SelectedValue.ToString();
            set => select1.SelectedValue = value;
        }

        public FormOpcTest()
        {
            InitializeComponent();
        }

        public void RefreshServers()
        {
            Message.loading(this, "正在加载服务列表", config =>
            {
                if (client != null)
                {
                    var servers = client?.GetServers(Host) ?? [];
                    select1.Items.Clear();
                    select1.Items.AddRange(servers);
                    config.OK("加载完成");
                }
            });
        }


        private void FormOpcTest_Load(object sender, EventArgs e)
        {
            client = new HmOpcDaClient();
            RefreshServers();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var node = treeViewEx1.SelectedNode.Tag as OpcDaNode;
            node?.WriteValue(short.Parse(valueTb.Text));
        }

        private void input1_Leave(object sender, EventArgs e)
        {
            RefreshServers();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            client.Host = Host;
            client.Server = Server;
            await client.Connect();
            Message.success(this, "连接成功");
            InitTreeView();
        }


        private void InitTreeView()
        {
            var trees = client.Browses(includeChild: false);
            foreach (var tree in trees)
            {
                treeViewEx1.Nodes.Add(CreateNode(tree));
            }
        }

        private void LoadNodes(TreeNode treeNode)
        {
            var node = treeNode.Tag as IOpcNode;
            var browses = client.Browses(node.ItemName, false);
            foreach (var browse in browses)
            {
                treeNode.Nodes.Add(CreateNode(browse));
            }
            treeNode.Expand();
        }

        private TreeNode CreateNode(IOpcNode opcNode)
        {
            var node = new TreeNode(opcNode.Name)
            {
                Tag = opcNode
            };
            node.Checked = true;
            foreach (var childNode in opcNode)
            {
                node.Nodes.Add(CreateNode(childNode));
            }

            opcNode.OnDataChange += (s, oldValue, newValue) =>
            {
                valueTb.Text = newValue.Value.ToString();
            };
            return node;
        }

        private void treeViewEx1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var treeNode = e.Node;
            var node = treeNode.Tag as IOpcNode;
            nameTb.Text = node.Name;
            fullPathTb.Text = node.ItemName;
            subscriptionCb.Tag = node;
            subscriptionCb.Checked = node.IsSubscription;
            valueTb.Text = node.ReadValue<short>().ToString();
        }

        private void treeViewEx1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            LoadNodes(e.Node);
        }

        private void subscriptionCb_CheckedChanged(object sender, bool value)
        {
            if ((sender as Checkbox)?.Tag is IOpcNode node && node.IsSubscription != value)
            {
                if (value) node.Subscription();
                else
                    node.UnSubscription();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var node = treeViewEx1.SelectedNode.Tag as OpcDaNode;
            node.WriteValueAsync(short.Parse(valueTb.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var node = treeViewEx1.SelectedNode.Tag as OpcDaNode;
            node.ReadValueAsync<short>();
        }
    }
}