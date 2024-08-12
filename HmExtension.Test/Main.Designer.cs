namespace HmExtension.Test
{
    partial class Main
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
            this.button2 = new AntdUI.Button();
            this.button3 = new AntdUI.Button();
            this.button4 = new AntdUI.Button();
            this.button5 = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 71);
            this.button1.TabIndex = 0;
            this.button1.Text = "WebSocket服务端测试";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(30, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 71);
            this.button2.TabIndex = 0;
            this.button2.Text = "二维码测试";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(230, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(180, 71);
            this.button3.TabIndex = 0;
            this.button3.Text = "摄像头测试";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(426, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(180, 71);
            this.button4.TabIndex = 0;
            this.button4.Text = "人脸识别测试";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(230, 139);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(180, 71);
            this.button5.TabIndex = 0;
            this.button5.Text = "WebSocket客户端测试";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Button button1;
        private AntdUI.Button button2;
        private AntdUI.Button button3;
        private AntdUI.Button button4;
        private AntdUI.Button button5;
    }
}