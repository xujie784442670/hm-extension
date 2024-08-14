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
using HmExtension.Commons.Extensions;
using HmExtension.Mqtt;
using TouchSocket.Sockets;
using Button = AntdUI.Button;
using Message = AntdUI.Message;

namespace HmExtension.Test
{
    public partial class FormMqttTest : Form
    {
        private HmMqttClient client = new HmMqttClient();

        public string Host
        {
            get => input1.Text;
            set => input1.Text = value;
        }

        public int Port
        {
            get => (int)inputNumber1.Value;
            set => inputNumber1.Value = value;
        }

        public string Username
        {
            get => input2.Text;
            set => input2.Text = value;
        }

        public string Password
        {
            get => input3.Text;
            set => input3.Text = value;
        }

        public List<string> Topics
        {
            get => input4.Text.Split(',').Select(s => s.Trim()).ToList();
            set => input4.Text = string.Join(",", value);
        }

        public string ClientId
        {
            get => input6.Text;
            set => input6.Text = value;
        }

        public FormMqttTest()
        {
            InitializeComponent();
        }

        private void RefreshClientId()
        {
            ClientId = Guid.NewGuid().ToString();
        }

        private void FormMqttTest_Load(object sender, EventArgs e)
        {
            client.IsPrintLogger = true;
            RefreshClientId();
            client.OnApplicationMessageReceived += e =>
            {
                var msg = e.ApplicationMessage;
                if (msg != null)
                {
                    var content = msg.PayloadSegment.Array.FromString(Encoding.UTF8);
                    chatList1.AddToBottom(new TextChatItem(content,null,$"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{e.ClientId}]{msg.Topic}"));
                }
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.Loading = true;
            if (client.IsConnected)
            {
                button.Text = "正在断开连接...";
                await client.Disconnected("自动断开");
                button.Text = "连接";
                button.Type = TTypeMini.Primary;
            }
            else
            {
                button.Text = "正在连接...";
                await client.ConnectAsync(Host, Username, Password, Port, ClientId);
                if (client.IsConnected)
                {
                    select1.Items.Clear();
                    select1.Items.AddRange(Topics.ToArray());
                    await client.SubscribesAsync(Topics.ToArray());
                    Message.success(this, "连接成功");
                    button.Text = "断开";
                    button.Type = TTypeMini.Error;
                }
            }
            button.Loading = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshClientId();
        }

        private async Task PushMessage()
        {
            if (select1.SelectedValue == null)
            {
                MessageBox.Show("请选择需要发送的目标主题");
                return;
            }
            var topic = await client.SubscribeAsync(select1.SelectedValue.ToString());
            topic.Push(input5.Text);
            chatList1.AddToBottom(new TextChatItem(input5.Text, null, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 我")
            {
                Me = true
            });
            input5.Clear();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await PushMessage();
        }

        private async void input5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                await PushMessage();
            }
        }
    }
}