using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntdUI;
using AntdUI.Chat;
using HmExtension.WebSocket;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace HmExtension.Test
{
    public partial class FormWebSocketClientTest : Form
    {

        public string URL
        {
            get
            {
                var url = urlTb.Text;
                var username = usernameTb.Text;
                var password = passwordTb.Text;
                if (!string.IsNullOrEmpty(username))
                {
                    if (url.IndexOf("?") == -1)
                    {
                        url += "?";
                    }
                    url += $"&username={username}&password={password}";
                }
                return url;
            }
        }

        private HmWebSocketClient client=new();

        public FormWebSocketClientTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
            {
                client.DisConnected("主动断开");
            }
            else
            {
                client.Url = URL;
                client.Connect(switch1.Checked);
                client.OnTextReceived += (c, data) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        chatList1.AddToBottom(new TextChatItem(data.ToText(Encoding.UTF8), null, c.Name));
                    }));
                };
            }
        }

        private void FormWebSocketClientTest_Load(object sender, EventArgs e)
        {
            client.OnChangedConnected += (b) =>
            {
                this.Invoke(new Action(() =>
                {
                    button1.Text = b ? "断开" : "连接";
                    button1.Type = b ? TTypeMini.Error : TTypeMini.Primary;
                    urlTb.Enabled = !b;
                    usernameTb.Enabled = !b;
                    passwordTb.Enabled = !b;
                    switch1.Enabled = !b;
                }));
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.Send(contentTb.Text);
            chatList1.AddToBottom(new TextChatItem(contentTb.Text,null,"我")
            {
                Me = true
            });
            contentTb.Clear();
        }
    }
}
