namespace HmExtension.Test
{
    partial class FormQrCodeTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQrCodeTest));
            this.button2 = new AntdUI.Button();
            this.levelSelect = new AntdUI.Select();
            this.drawQuietZonesSelect = new AntdUI.Select();
            this.imageSizeLabel = new AntdUI.Label();
            this.iconBorderWidthTb = new AntdUI.InputNumber();
            this.qrModelSizeTb = new AntdUI.InputNumber();
            this.image3D1 = new AntdUI.Image3D();
            this.iconPathTb = new AntdUI.Input();
            this.qrContent = new AntdUI.Input();
            this.button1 = new AntdUI.Button();
            this.label2 = new AntdUI.Label();
            this.darkColorCp = new AntdUI.ColorPicker();
            this.label4 = new AntdUI.Label();
            this.iconBackgroundColorCp = new AntdUI.ColorPicker();
            this.panel4 = new AntdUI.Panel();
            this.label3 = new AntdUI.Label();
            this.lightColorCp = new AntdUI.ColorPicker();
            this.panel2 = new AntdUI.Panel();
            this.label1 = new AntdUI.Label();
            this.iconSizePercentTb = new AntdUI.Slider();
            this.panel3 = new AntdUI.Panel();
            this.panel1 = new AntdUI.Panel();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 24F);
            this.button2.ImageSvg = resources.GetString("button2.ImageSvg");
            this.button2.Location = new System.Drawing.Point(333, 283);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 36);
            this.button2.TabIndex = 35;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // levelSelect
            // 
            this.levelSelect.Location = new System.Drawing.Point(30, 199);
            this.levelSelect.Name = "levelSelect";
            this.levelSelect.PrefixText = "纠错等级:";
            this.levelSelect.Size = new System.Drawing.Size(336, 36);
            this.levelSelect.TabIndex = 33;
            // 
            // drawQuietZonesSelect
            // 
            this.drawQuietZonesSelect.Location = new System.Drawing.Point(32, 241);
            this.drawQuietZonesSelect.Name = "drawQuietZonesSelect";
            this.drawQuietZonesSelect.PrefixText = "静止区尺寸:";
            this.drawQuietZonesSelect.Size = new System.Drawing.Size(336, 36);
            this.drawQuietZonesSelect.TabIndex = 34;
            // 
            // imageSizeLabel
            // 
            this.imageSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageSizeLabel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imageSizeLabel.Location = new System.Drawing.Point(375, 11);
            this.imageSizeLabel.Name = "imageSizeLabel";
            this.imageSizeLabel.Size = new System.Drawing.Size(742, 46);
            this.imageSizeLabel.TabIndex = 32;
            this.imageSizeLabel.Text = "";
            this.imageSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconBorderWidthTb
            // 
            this.iconBorderWidthTb.Font = new System.Drawing.Font("宋体", 12F);
            this.iconBorderWidthTb.Location = new System.Drawing.Point(30, 412);
            this.iconBorderWidthTb.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.iconBorderWidthTb.Name = "iconBorderWidthTb";
            this.iconBorderWidthTb.PrefixText = "水印边框大小";
            this.iconBorderWidthTb.Size = new System.Drawing.Size(334, 36);
            this.iconBorderWidthTb.TabIndex = 30;
            this.iconBorderWidthTb.Text = "1";
            this.iconBorderWidthTb.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // qrModelSizeTb
            // 
            this.qrModelSizeTb.Font = new System.Drawing.Font("宋体", 12F);
            this.qrModelSizeTb.Location = new System.Drawing.Point(32, 73);
            this.qrModelSizeTb.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.qrModelSizeTb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.qrModelSizeTb.Name = "qrModelSizeTb";
            this.qrModelSizeTb.PrefixText = "宽高:";
            this.qrModelSizeTb.Size = new System.Drawing.Size(334, 36);
            this.qrModelSizeTb.TabIndex = 31;
            this.qrModelSizeTb.Text = "200";
            this.qrModelSizeTb.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // image3D1
            // 
            this.image3D1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.image3D1.BackColor = System.Drawing.Color.Gainsboro;
            this.image3D1.Location = new System.Drawing.Point(372, 57);
            this.image3D1.Name = "image3D1";
            this.image3D1.Size = new System.Drawing.Size(745, 549);
            this.image3D1.TabIndex = 29;
            this.image3D1.Text = "image3D1";
            // 
            // iconPathTb
            // 
            this.iconPathTb.Font = new System.Drawing.Font("宋体", 12F);
            this.iconPathTb.Location = new System.Drawing.Point(30, 283);
            this.iconPathTb.Name = "iconPathTb";
            this.iconPathTb.PrefixText = "水印图标:";
            this.iconPathTb.Size = new System.Drawing.Size(297, 36);
            this.iconPathTb.TabIndex = 26;
            // 
            // qrContent
            // 
            this.qrContent.Font = new System.Drawing.Font("宋体", 12F);
            this.qrContent.Location = new System.Drawing.Point(30, 34);
            this.qrContent.Name = "qrContent";
            this.qrContent.PrefixText = "二维码内容:";
            this.qrContent.Size = new System.Drawing.Size(336, 36);
            this.qrContent.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 454);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(334, 30);
            this.button1.TabIndex = 28;
            this.button1.Text = "生成二维码";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 36);
            this.label2.TabIndex = 6;
            this.label2.Text = "暗色:";
            // 
            // darkColorCp
            // 
            this.darkColorCp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.darkColorCp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.darkColorCp.Location = new System.Drawing.Point(48, 7);
            this.darkColorCp.Name = "darkColorCp";
            this.darkColorCp.Round = true;
            this.darkColorCp.ShowText = true;
            this.darkColorCp.Size = new System.Drawing.Size(281, 36);
            this.darkColorCp.TabIndex = 5;
            this.darkColorCp.Text = "colorPicker1";
            this.darkColorCp.Value = System.Drawing.Color.Black;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 36);
            this.label4.TabIndex = 6;
            this.label4.Text = "水印背景色:";
            // 
            // iconBackgroundColorCp
            // 
            this.iconBackgroundColorCp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iconBackgroundColorCp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.iconBackgroundColorCp.Location = new System.Drawing.Point(77, 7);
            this.iconBackgroundColorCp.Name = "iconBackgroundColorCp";
            this.iconBackgroundColorCp.Round = true;
            this.iconBackgroundColorCp.ShowText = true;
            this.iconBackgroundColorCp.Size = new System.Drawing.Size(252, 36);
            this.iconBackgroundColorCp.TabIndex = 5;
            this.iconBackgroundColorCp.Text = "colorPicker1";
            this.iconBackgroundColorCp.Value = System.Drawing.Color.Black;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.iconBackgroundColorCp);
            this.panel4.Location = new System.Drawing.Point(30, 359);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(332, 46);
            this.panel4.TabIndex = 37;
            this.panel4.Text = "panel1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 36);
            this.label3.TabIndex = 6;
            this.label3.Text = "亮色:";
            // 
            // lightColorCp
            // 
            this.lightColorCp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightColorCp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lightColorCp.Location = new System.Drawing.Point(45, 7);
            this.lightColorCp.Name = "lightColorCp";
            this.lightColorCp.Round = true;
            this.lightColorCp.ShowText = true;
            this.lightColorCp.Size = new System.Drawing.Size(281, 36);
            this.lightColorCp.TabIndex = 5;
            this.lightColorCp.Text = "colorPicker1";
            this.lightColorCp.Value = System.Drawing.Color.White;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lightColorCp);
            this.panel2.Location = new System.Drawing.Point(35, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(326, 46);
            this.panel2.TabIndex = 36;
            this.panel2.Text = "panel1";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 36);
            this.label1.TabIndex = 6;
            this.label1.Text = "水印比例:";
            // 
            // iconSizePercentTb
            // 
            this.iconSizePercentTb.Location = new System.Drawing.Point(75, 3);
            this.iconSizePercentTb.Name = "iconSizePercentTb";
            this.iconSizePercentTb.ShowValue = true;
            this.iconSizePercentTb.Size = new System.Drawing.Size(207, 36);
            this.iconSizePercentTb.TabIndex = 10;
            this.iconSizePercentTb.Text = "slider1";
            this.iconSizePercentTb.Value = 15;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.iconSizePercentTb);
            this.panel3.Location = new System.Drawing.Point(32, 325);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(333, 39);
            this.panel3.TabIndex = 39;
            this.panel3.Text = "panel3";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.darkColorCp);
            this.panel1.Location = new System.Drawing.Point(32, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 46);
            this.panel1.TabIndex = 38;
            this.panel1.Text = "panel1";
            // 
            // FormQrCodeTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 618);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.levelSelect);
            this.Controls.Add(this.drawQuietZonesSelect);
            this.Controls.Add(this.imageSizeLabel);
            this.Controls.Add(this.iconBorderWidthTb);
            this.Controls.Add(this.qrModelSizeTb);
            this.Controls.Add(this.image3D1);
            this.Controls.Add(this.iconPathTb);
            this.Controls.Add(this.qrContent);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "FormQrCodeTest";
            this.Text = "FormQrCodeTest";
            this.Load += new System.EventHandler(this.FormQrCodeTest_Load);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Button button2;
        private AntdUI.Select levelSelect;
        private AntdUI.Select drawQuietZonesSelect;
        private AntdUI.Label imageSizeLabel;
        private AntdUI.InputNumber iconBorderWidthTb;
        private AntdUI.InputNumber qrModelSizeTb;
        private AntdUI.Image3D image3D1;
        private AntdUI.Input iconPathTb;
        private AntdUI.Input qrContent;
        private AntdUI.Button button1;
        private AntdUI.Label label2;
        private AntdUI.ColorPicker darkColorCp;
        private AntdUI.Label label4;
        private AntdUI.ColorPicker iconBackgroundColorCp;
        private AntdUI.Panel panel4;
        private AntdUI.Label label3;
        private AntdUI.ColorPicker lightColorCp;
        private AntdUI.Panel panel2;
        private AntdUI.Label label1;
        private AntdUI.Slider iconSizePercentTb;
        private AntdUI.Panel panel3;
        private AntdUI.Panel panel1;
    }
}