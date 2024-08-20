namespace HmExtension.Test
{
    partial class FormOpcTest
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
            this.button1 = new AntdUI.Button();
            this.treeViewEx1 = new OpcUaHelper.Forms.TreeViewEx();
            this.nameTb = new AntdUI.Input();
            this.fullPathTb = new AntdUI.Input();
            this.valueTb = new AntdUI.Input();
            this.button2 = new AntdUI.Button();
            this.select1 = new AntdUI.Select();
            this.input1 = new AntdUI.Input();
            this.subscriptionCb = new AntdUI.Checkbox();
            this.button3 = new AntdUI.Button();
            this.button4 = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(575, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "连接";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeViewEx1
            // 
            this.treeViewEx1.Location = new System.Drawing.Point(12, 58);
            this.treeViewEx1.Name = "treeViewEx1";
            this.treeViewEx1.Size = new System.Drawing.Size(471, 380);
            this.treeViewEx1.TabIndex = 1;
            this.treeViewEx1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewEx1_NodeMouseClick);
            this.treeViewEx1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewEx1_NodeMouseDoubleClick);
            // 
            // nameTb
            // 
            this.nameTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTb.Location = new System.Drawing.Point(489, 69);
            this.nameTb.Name = "nameTb";
            this.nameTb.PrefixText = "Name:";
            this.nameTb.Size = new System.Drawing.Size(441, 40);
            this.nameTb.TabIndex = 2;
            // 
            // fullPathTb
            // 
            this.fullPathTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fullPathTb.Location = new System.Drawing.Point(489, 115);
            this.fullPathTb.Name = "fullPathTb";
            this.fullPathTb.PrefixText = "FullPath:";
            this.fullPathTb.Size = new System.Drawing.Size(441, 40);
            this.fullPathTb.TabIndex = 2;
            // 
            // valueTb
            // 
            this.valueTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTb.Location = new System.Drawing.Point(489, 253);
            this.valueTb.Name = "valueTb";
            this.valueTb.PrefixText = "Value:";
            this.valueTb.Size = new System.Drawing.Size(368, 40);
            this.valueTb.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(866, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 35);
            this.button2.TabIndex = 3;
            this.button2.Text = "写入";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // select1
            // 
            this.select1.Location = new System.Drawing.Point(268, 12);
            this.select1.Name = "select1";
            this.select1.PrefixText = "服务列表:";
            this.select1.Size = new System.Drawing.Size(287, 39);
            this.select1.TabIndex = 4;
            // 
            // input1
            // 
            this.input1.Location = new System.Drawing.Point(14, 12);
            this.input1.Name = "input1";
            this.input1.PrefixText = "主机地址:";
            this.input1.Size = new System.Drawing.Size(254, 38);
            this.input1.TabIndex = 5;
            this.input1.Text = "127.0.0.1";
            this.input1.Leave += new System.EventHandler(this.input1_Leave);
            // 
            // subscriptionCb
            // 
            this.subscriptionCb.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.subscriptionCb.Location = new System.Drawing.Point(497, 169);
            this.subscriptionCb.Name = "subscriptionCb";
            this.subscriptionCb.Size = new System.Drawing.Size(431, 32);
            this.subscriptionCb.TabIndex = 6;
            this.subscriptionCb.Text = "订阅";
            this.subscriptionCb.CheckedChanged += new AntdUI.BoolEventHandler(this.subscriptionCb_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(492, 310);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 35);
            this.button3.TabIndex = 3;
            this.button3.Text = "异步读";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(575, 310);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 35);
            this.button4.TabIndex = 3;
            this.button4.Text = "异步写";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FormOpcTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 450);
            this.Controls.Add(this.subscriptionCb);
            this.Controls.Add(this.input1);
            this.Controls.Add(this.select1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.valueTb);
            this.Controls.Add(this.fullPathTb);
            this.Controls.Add(this.nameTb);
            this.Controls.Add(this.treeViewEx1);
            this.Controls.Add(this.button1);
            this.Name = "FormOpcTest";
            this.Text = "FormOpcTest";
            this.Load += new System.EventHandler(this.FormOpcTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Button button1;
        private OpcUaHelper.Forms.TreeViewEx treeViewEx1;
        private AntdUI.Input nameTb;
        private AntdUI.Input fullPathTb;
        private AntdUI.Input valueTb;
        private AntdUI.Button button2;
        private AntdUI.Select select1;
        private AntdUI.Input input1;
        private AntdUI.Checkbox subscriptionCb;
        private AntdUI.Button button3;
        private AntdUI.Button button4;
    }
}