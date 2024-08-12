namespace HmExtension.Test
{
    partial class FormWebSocketTest
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
            this.portTb = new AntdUI.InputNumber();
            this.button1 = new AntdUI.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.chatList1 = new AntdUI.Chat.ChatList();
            this.checkbox1 = new AntdUI.Checkbox();
            this.usernameTb = new AntdUI.Input();
            this.passwordTb = new AntdUI.Input();
            this.contentTb = new AntdUI.Input();
            this.button2 = new AntdUI.Button();
            this.switch1 = new AntdUI.Switch();
            this.SuspendLayout();
            // 
            // urlTb
            // 
            this.urlTb.Location = new System.Drawing.Point(12, 15);
            this.urlTb.Name = "urlTb";
            this.urlTb.PrefixText = "URL:";
            this.urlTb.Size = new System.Drawing.Size(207, 53);
            this.urlTb.TabIndex = 0;
            this.urlTb.Text = "/ws";
            // 
            // portTb
            // 
            this.portTb.Location = new System.Drawing.Point(12, 74);
            this.portTb.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portTb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.portTb.Name = "portTb";
            this.portTb.PrefixText = "端口:";
            this.portTb.Size = new System.Drawing.Size(207, 51);
            this.portTb.TabIndex = 1;
            this.portTb.Text = "7896";
            this.portTb.Value = new decimal(new int[] {
            7896,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "启动";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.DisplayMember = "Name";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 338);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(207, 316);
            this.listBox1.TabIndex = 3;
            // 
            // chatList1
            // 
            this.chatList1.Location = new System.Drawing.Point(225, 15);
            this.chatList1.Name = "chatList1";
            this.chatList1.Size = new System.Drawing.Size(639, 516);
            this.chatList1.TabIndex = 4;
            this.chatList1.Text = "chatList1";
            // 
            // checkbox1
            // 
            this.checkbox1.Location = new System.Drawing.Point(17, 144);
            this.checkbox1.Name = "checkbox1";
            this.checkbox1.Size = new System.Drawing.Size(201, 26);
            this.checkbox1.TabIndex = 5;
            this.checkbox1.Text = "开启验证";
            this.checkbox1.CheckedChanged += new AntdUI.BoolEventHandler(this.checkbox1_CheckedChanged);
            // 
            // usernameTb
            // 
            this.usernameTb.Location = new System.Drawing.Point(11, 176);
            this.usernameTb.Name = "usernameTb";
            this.usernameTb.PrefixText = "用户名:";
            this.usernameTb.Size = new System.Drawing.Size(207, 43);
            this.usernameTb.TabIndex = 0;
            // 
            // passwordTb
            // 
            this.passwordTb.Location = new System.Drawing.Point(11, 225);
            this.passwordTb.Name = "passwordTb";
            this.passwordTb.PrefixText = "密码:";
            this.passwordTb.Size = new System.Drawing.Size(207, 35);
            this.passwordTb.TabIndex = 0;
            // 
            // contentTb
            // 
            this.contentTb.Location = new System.Drawing.Point(225, 537);
            this.contentTb.Multiline = true;
            this.contentTb.Name = "contentTb";
            this.contentTb.Size = new System.Drawing.Size(504, 117);
            this.contentTb.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(747, 585);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 68);
            this.button2.TabIndex = 7;
            this.button2.Text = "群发消息";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // switch1
            // 
            this.switch1.Checked = true;
            this.switch1.Location = new System.Drawing.Point(759, 537);
            this.switch1.Name = "switch1";
            this.switch1.Size = new System.Drawing.Size(84, 35);
            this.switch1.TabIndex = 8;
            this.switch1.Text = "switch1";
            this.switch1.CheckedChanged += new AntdUI.BoolEventHandler(this.switch1_CheckedChanged);
            // 
            // FormWebSocketTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 665);
            this.Controls.Add(this.switch1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.contentTb);
            this.Controls.Add(this.checkbox1);
            this.Controls.Add(this.chatList1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.portTb);
            this.Controls.Add(this.passwordTb);
            this.Controls.Add(this.usernameTb);
            this.Controls.Add(this.urlTb);
            this.Name = "FormWebSocketTest";
            this.Text = "FormWebSocketTest";
            this.Load += new System.EventHandler(this.FormWebSocketTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Input urlTb;
        private AntdUI.InputNumber portTb;
        private AntdUI.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private AntdUI.Chat.ChatList chatList1;
        private AntdUI.Checkbox checkbox1;
        private AntdUI.Input usernameTb;
        private AntdUI.Input passwordTb;
        private AntdUI.Input contentTb;
        private AntdUI.Button button2;
        private AntdUI.Switch switch1;
    }
}