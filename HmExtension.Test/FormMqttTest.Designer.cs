namespace HmExtension.Test
{
    partial class FormMqttTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMqttTest));
            this.input1 = new AntdUI.Input();
            this.input2 = new AntdUI.Input();
            this.inputNumber1 = new AntdUI.InputNumber();
            this.input3 = new AntdUI.Input();
            this.input4 = new AntdUI.Input();
            this.button1 = new AntdUI.Button();
            this.chatList1 = new AntdUI.Chat.ChatList();
            this.input5 = new AntdUI.Input();
            this.button2 = new AntdUI.Button();
            this.select1 = new AntdUI.Select();
            this.input6 = new AntdUI.Input();
            this.button3 = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // input1
            // 
            this.input1.Location = new System.Drawing.Point(19, 17);
            this.input1.Name = "input1";
            this.input1.PrefixText = "主机:";
            this.input1.Size = new System.Drawing.Size(474, 56);
            this.input1.TabIndex = 0;
            this.input1.Text = "47.119.162.143";
            // 
            // input2
            // 
            this.input2.Location = new System.Drawing.Point(19, 209);
            this.input2.Name = "input2";
            this.input2.PrefixText = "用户名:";
            this.input2.Size = new System.Drawing.Size(474, 56);
            this.input2.TabIndex = 2;
            this.input2.Text = "admin";
            // 
            // inputNumber1
            // 
            this.inputNumber1.Location = new System.Drawing.Point(19, 113);
            this.inputNumber1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.inputNumber1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.inputNumber1.Name = "inputNumber1";
            this.inputNumber1.PrefixText = "端口:";
            this.inputNumber1.Size = new System.Drawing.Size(474, 56);
            this.inputNumber1.TabIndex = 1;
            this.inputNumber1.Text = "1883";
            this.inputNumber1.Value = new decimal(new int[] {
            1883,
            0,
            0,
            0});
            // 
            // input3
            // 
            this.input3.AllowClear = true;
            this.input3.Location = new System.Drawing.Point(19, 305);
            this.input3.Name = "input3";
            this.input3.PrefixText = "密码:";
            this.input3.Size = new System.Drawing.Size(474, 56);
            this.input3.TabIndex = 3;
            this.input3.Text = "123";
            this.input3.UseSystemPasswordChar = true;
            // 
            // input4
            // 
            this.input4.AllowClear = true;
            this.input4.Location = new System.Drawing.Point(19, 497);
            this.input4.Name = "input4";
            this.input4.PrefixText = "订阅主题:";
            this.input4.Size = new System.Drawing.Size(474, 56);
            this.input4.TabIndex = 4;
            this.input4.Text = "/test";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 593);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(474, 56);
            this.button1.TabIndex = 2;
            this.button1.Text = "连接";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chatList1
            // 
            this.chatList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatList1.Location = new System.Drawing.Point(499, 23);
            this.chatList1.Name = "chatList1";
            this.chatList1.Size = new System.Drawing.Size(662, 461);
            this.chatList1.TabIndex = 3;
            this.chatList1.Text = "chatList1";
            // 
            // input5
            // 
            this.input5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input5.Location = new System.Drawing.Point(499, 543);
            this.input5.Multiline = true;
            this.input5.Name = "input5";
            this.input5.Size = new System.Drawing.Size(558, 99);
            this.input5.TabIndex = 4;
            this.input5.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input5_KeyUp);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1072, 545);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 98);
            this.button2.TabIndex = 5;
            this.button2.Text = "发送";
            this.button2.Type = AntdUI.TTypeMini.Primary;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // select1
            // 
            this.select1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.select1.Location = new System.Drawing.Point(499, 486);
            this.select1.Name = "select1";
            this.select1.PrefixText = "推送主题:";
            this.select1.Size = new System.Drawing.Size(670, 53);
            this.select1.TabIndex = 6;
            // 
            // input6
            // 
            this.input6.AllowClear = true;
            this.input6.Location = new System.Drawing.Point(19, 401);
            this.input6.Name = "input6";
            this.input6.PrefixText = "客户端ID:";
            this.input6.Size = new System.Drawing.Size(407, 56);
            this.input6.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.IconRatio = 2F;
            this.button3.ImageSvg = resources.GetString("button3.ImageSvg");
            this.button3.Location = new System.Drawing.Point(432, 402);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(54, 55);
            this.button3.TabIndex = 7;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormMqttTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1178, 654);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.select1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.input5);
            this.Controls.Add(this.chatList1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.inputNumber1);
            this.Controls.Add(this.input6);
            this.Controls.Add(this.input4);
            this.Controls.Add(this.input3);
            this.Controls.Add(this.input2);
            this.Controls.Add(this.input1);
            this.MinimumSize = new System.Drawing.Size(1200, 710);
            this.Name = "FormMqttTest";
            this.Text = "MQTT测试工具";
            this.Load += new System.EventHandler(this.FormMqttTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Input input1;
        private AntdUI.Input input2;
        private AntdUI.InputNumber inputNumber1;
        private AntdUI.Input input3;
        private AntdUI.Input input4;
        private AntdUI.Button button1;
        private AntdUI.Chat.ChatList chatList1;
        private AntdUI.Input input5;
        private AntdUI.Button button2;
        private AntdUI.Select select1;
        private AntdUI.Input input6;
        private AntdUI.Button button3;
    }
}