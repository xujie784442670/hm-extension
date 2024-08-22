namespace HmExtension.Test
{
    partial class FormXingHuoTest
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
            this.input1 = new AntdUI.Input();
            this.input2 = new AntdUI.Input();
            this.chatList1 = new AntdUI.Chat.ChatList();
            this.contentTb = new AntdUI.Input();
            this.button1 = new AntdUI.Button();
            this.input3 = new AntdUI.Input();
            this.systemTb = new AntdUI.Input();
            this.button2 = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // input1
            // 
            this.input1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input1.Location = new System.Drawing.Point(8, 11);
            this.input1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.input1.Name = "input1";
            this.input1.PrefixText = "ApiUrl:";
            this.input1.Size = new System.Drawing.Size(760, 41);
            this.input1.TabIndex = 0;
            this.input1.Text = "https://spark-api-open.xf-yun.com/v1/chat/completions";
            // 
            // input2
            // 
            this.input2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input2.Location = new System.Drawing.Point(8, 56);
            this.input2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.input2.Name = "input2";
            this.input2.PrefixText = "AppSecret:";
            this.input2.Size = new System.Drawing.Size(760, 41);
            this.input2.TabIndex = 0;
            this.input2.Text = "ZTZlZDNkZGQ0NjNjMWQ2MTJmYmYwMWRl";
            // 
            // chatList1
            // 
            this.chatList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatList1.BackColor = System.Drawing.Color.White;
            this.chatList1.Location = new System.Drawing.Point(8, 242);
            this.chatList1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chatList1.Name = "chatList1";
            this.chatList1.Size = new System.Drawing.Size(759, 308);
            this.chatList1.TabIndex = 1;
            this.chatList1.Text = "chatList1";
            // 
            // contentTb
            // 
            this.contentTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentTb.Location = new System.Drawing.Point(8, 554);
            this.contentTb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.contentTb.Multiline = true;
            this.contentTb.Name = "contentTb";
            this.contentTb.Size = new System.Drawing.Size(760, 102);
            this.contentTb.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(715, 619);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 37);
            this.button1.TabIndex = 3;
            this.button1.Text = "发送";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // input3
            // 
            this.input3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input3.Location = new System.Drawing.Point(8, 101);
            this.input3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.input3.Name = "input3";
            this.input3.PrefixText = "AppId:";
            this.input3.Size = new System.Drawing.Size(760, 41);
            this.input3.TabIndex = 0;
            this.input3.Text = "0a44293377d0c44d9dda6db6fcf3d4dd";
            // 
            // systemTb
            // 
            this.systemTb.Location = new System.Drawing.Point(8, 147);
            this.systemTb.Multiline = true;
            this.systemTb.Name = "systemTb";
            this.systemTb.PrefixText = "身份信息:";
            this.systemTb.Size = new System.Drawing.Size(759, 90);
            this.systemTb.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(712, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 45);
            this.button2.TabIndex = 5;
            this.button2.Text = "确定";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormOpenAiTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 664);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.systemTb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.contentTb);
            this.Controls.Add(this.chatList1);
            this.Controls.Add(this.input3);
            this.Controls.Add(this.input2);
            this.Controls.Add(this.input1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormOpenAiTest";
            this.Text = "FormOpenAiTest";
            this.Load += new System.EventHandler(this.FormOpenAiTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Input input1;
        private AntdUI.Input input2;
        private AntdUI.Chat.ChatList chatList1;
        private AntdUI.Input contentTb;
        private AntdUI.Button button1;
        private AntdUI.Input input3;
        private AntdUI.Input systemTb;
        private AntdUI.Button button2;
    }
}