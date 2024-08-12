namespace HmExtension.Test
{
    partial class FormFaceTest
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button7 = new AntdUI.Button();
            this.button6 = new AntdUI.Button();
            this.button5 = new AntdUI.Button();
            this.cameraList = new AntdUI.Select();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(11, 123);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1042, 475);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(587, 27);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(136, 46);
            this.button7.TabIndex = 10;
            this.button7.Text = "识别人脸";
            this.button7.Type = AntdUI.TTypeMini.Primary;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(432, 28);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(136, 46);
            this.button6.TabIndex = 11;
            this.button6.Text = "注册人脸";
            this.button6.Type = AntdUI.TTypeMini.Primary;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(262, 27);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(136, 46);
            this.button5.TabIndex = 12;
            this.button5.Text = "打开摄像头";
            this.button5.Type = AntdUI.TTypeMini.Primary;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cameraList
            // 
            this.cameraList.Location = new System.Drawing.Point(11, 27);
            this.cameraList.Margin = new System.Windows.Forms.Padding(2);
            this.cameraList.Name = "cameraList";
            this.cameraList.Size = new System.Drawing.Size(227, 47);
            this.cameraList.TabIndex = 9;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormFaceTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 624);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.cameraList);
            this.Name = "FormFaceTest";
            this.Text = "FormFaceTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.FormFaceTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private AntdUI.Button button7;
        private AntdUI.Button button6;
        private AntdUI.Button button5;
        private AntdUI.Select cameraList;
        private System.Windows.Forms.Timer timer1;
    }
}