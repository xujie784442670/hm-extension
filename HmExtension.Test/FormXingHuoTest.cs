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
using AntdUI.Chat;
using HmExtension.AIChat;
using HmExtension.AIChat.commons;
using HmExtension.AIChat.xinghuo;
using Button = AntdUI.Button;

namespace HmExtension.Test
{
    public partial class FormXingHuoTest : Form
    {
        public string ApiUrl => input1.Text;

        public string AppSecret => input2.Text;

        public string AppId => input3.Text;

        private HmAiChatClient<XinHuoModel> _client; 

        public FormXingHuoTest()
        {
            InitializeComponent();
        }

        private void SendMessage()
        {

        }


        private async void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(ApiUrl) || string.IsNullOrWhiteSpace(AppSecret) || string.IsNullOrWhiteSpace(AppId))
            {
                MessageBox.Show("请填写完整信息");
                return;
            }

            if (string.IsNullOrWhiteSpace(contentTb.Text))
            {
                MessageBox.Show("请输入内容");
                return;
            }

            var button = sender as Button;
            button.Loading = true;
            contentTb.Enabled = false;
            _client.Option.ApiSecret = AppSecret;
            _client.Option.ApiUrl = ApiUrl;
            ((XingHuoChatOption)_client.Option).AppId = AppId;
            try
            {
                var msg = await _client.SendMessageAsync(XinHuoModel.Lite, contentTb.Text);
                chatList1.Items.Add(new TextChatItem(contentTb.Text, null, "我") { Me = true });
                chatList1.Items.Add(new TextChatItem(msg, null, "ai"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误:{ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
            contentTb.Enabled = true;
            button.Loading = false;
            contentTb.Clear();
        }

        private void FormOpenAiTest_Load(object sender, EventArgs e)
        {
            _client = HmAiChatClientFactory.Create(new XingHuoChatOption()
            {
                ApiUrl = ApiUrl,
                ApiSecret = AppSecret
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _client.SetSystemMessage(systemTb.Text);
        }
    }
}
