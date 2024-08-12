using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AntdUI.Chat;
using HmExtension.Commons.Extensions;
using HmExtension.Drawing;
using HmExtension.QRCode;
using HmExtension.WebSocket;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using ZXing.QrCode.Internal;
using Button = AntdUI.Button;
using Message = AntdUI.Message;
using System.Web.UI.WebControls;

namespace HmExtension.Test;

public partial class FormWebSocketTest : Form
{
    private WebSocketServer server = new();

    public FormWebSocketTest()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (server.IsRunning)
        {
            server.Stop();
        }
        else
        {
            server.Url = urlTb.Text;
            server.Port = (int)portTb.Value;
            server.IsConsoleLogger = true;
            if (checkbox1.Checked)
            {
                server.VerifyHandler = (client, context) => context.Request.Query.Get("username") == usernameTb.Text &&
                                                            context.Request.Query.Get("password") == passwordTb.Text;
            }

            server.Start();
        }
    }

    private void FormWebSocketTest_Load(object sender, EventArgs e)
    {
        server.OnConnectedChanged += (isConnected) =>
        {
            button1.Text = isConnected ? "停止" : "启动";
            button1.Type = isConnected ? TTypeMini.Error : TTypeMini.Primary;
        };
        server.OnPreConnected += (client, e) =>
        {
            Console.WriteLine($"OnPreConnected: {client.IP}:{client.Port}/{e.Context.Request.URL}");
        };
        server.OnConnected += (client, e) =>
        {
            this.Invoke(() =>
            {
                listBox1.Items.Add(client);
            });
        };

        server.OnReceived += (client, e) =>
        {
            TextChatItem item = new TextChatItem(e.DataFrame.ToText(Encoding.UTF8), null, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {client.Name}");
            // 滚动到最下面
            chatList1.AddToBottom(item);
        };

        server.OnClosing += (client, e) =>
        {
            // 移除客户端
            this.Invoke(() =>
            {
                listBox1.Items.Remove($"{client.WebSocket.Client.IP}:{client.WebSocket.Client.Port}");
            });
        };
    }

    private void checkbox1_CheckedChanged(object sender, bool value)
    {
        usernameTb.Enabled = value;
        passwordTb.Enabled = value;
    }

    private void switch1_CheckedChanged(object sender, bool value)
    {
        button2.Text = value ? "群发消息" : "私发消息";
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (switch1.Checked)
        {
            server.SendAll(contentTb.Text);
        }
        else
        {
            var client = (HmWebSocketClient)listBox1.SelectedItem;
            if(client == null)
            {
                Message.error(this, "请选择一个客户端");
                return;
            }
            client.Send(contentTb.Text);
        }

        TextChatItem item = new TextChatItem(contentTb.Text, null, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 我")
        {
            Me = true
        };
        chatList1.Items.Add(item);
        contentTb.Clear();
    }
}

