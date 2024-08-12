namespace HmExtension.Test
{
    partial class FormWebSocketClientTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.urlTb = new AntdUI.Input();
            this.button1 = new AntdUI.Button();
            this.usernameTb = new AntdUI.Input();
            this.passwordTb = new AntdUI.Input();
            this.label1 = new AntdUI.Label();
            this.switch1 = new AntdUI.Switch();
            this.chatList1 = new AntdUI.Chat.ChatList();
            this.contentTb = new AntdUI.Input();
            this.button2 = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // urlTb
            // 
            this.urlTb.Location = new System.Drawing.Point(19, 15);
            this.urlTb.Name = "urlTb";
            this.urlTb.PrefixText = "URL:";
            this.urlTb.Size = new System.Drawing.Size(218, 45);
            this.urlTb.TabIndex = 0;
            this.urlTb.Text = "ws://127.0.0.1:7896/ws";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 51);
            this.button1.TabIndex = 1;
            this.button1.Text = "连接";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // usernameTb
            // 
            this.usernameTb.Location = new System.Drawing.Point(19, 66);
            this.usernameTb.Name = "usernameTb";
            this.usernameTb.PrefixText = "用户名:";
            this.usernameTb.Size = new System.Drawing.Size(216, 42);
            this.usernameTb.TabIndex = 2;
            // 
            // passwordTb
            // 
            this.passwordTb.Location = new System.Drawing.Point(19, 114);
            this.passwordTb.Name = "passwordTb";
            this.passwordTb.PrefixText = "密码:";
            this.passwordTb.Size = new System.Drawing.Size(216, 42);
            this.passwordTb.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Post请求:";
            // 
            // switch1
            // 
            this.switch1.Location = new System.Drawing.Point(107, 181);
            this.switch1.Name = "switch1";
            this.switch1.Size = new System.Drawing.Size(116, 39);
            this.switch1.TabIndex = 4;
            this.switch1.Text = "switch1";
            // 
            // chatList1
            // 
            this.chatList1.Location = new System.Drawing.Point(258, 15);
            this.chatList1.Name = "chatList1";
            this.chatList1.Size = new System.Drawing.Size(530, 313);
            this.chatList1.TabIndex = 5;
            this.chatList1.Text = "chatList1";
            // 
            // contentTb
            // 
            this.contentTb.Location = new System.Drawing.Point(258, 334);
            this.contentTb.Multiline = true;
            this.contentTb.Name = "contentTb";
            this.contentTb.Size = new System.Drawing.Size(427, 104);
            this.contentTb.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(699, 336);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 101);
            this.button2.TabIndex = 7;
            this.button2.Text = "发送";
            this.button2.Type = AntdUI.TTypeMini.Primary;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormWebSocketClientTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.contentTb);
            this.Controls.Add(this.chatList1);
            this.Controls.Add(this.switch1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTb);
            this.Controls.Add(this.usernameTb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.urlTb);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWebSocketClientTest";
            this.Text = "FormWebSocketClientTest";
            this.Load += new System.EventHandler(this.FormWebSocketClientTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Input urlTb;
        private AntdUI.Button button1;
        private AntdUI.Input usernameTb;
        private AntdUI.Input passwordTb;
        private AntdUI.Label label1;
        private AntdUI.Switch switch1;
        private AntdUI.Chat.ChatList chatList1;
        private AntdUI.Input contentTb;
        private AntdUI.Button button2;
    }
}