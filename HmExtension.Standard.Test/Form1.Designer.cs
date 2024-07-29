namespace HmExtension.Standard.Test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.qrContent = new AntdUI.Input();
            this.button1 = new AntdUI.Button();
            this.image3D1 = new AntdUI.Image3D();
            this.tabs1 = new AntdUI.Tabs();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new AntdUI.Panel();
            this.label3 = new AntdUI.Label();
            this.lightColorCp = new AntdUI.ColorPicker();
            this.panel1 = new AntdUI.Panel();
            this.label2 = new AntdUI.Label();
            this.darkColorCp = new AntdUI.ColorPicker();
            this.button2 = new AntdUI.Button();
            this.levelSelect = new AntdUI.Select();
            this.drawQuietZonesSelect = new AntdUI.Select();
            this.imageSizeLabel = new AntdUI.Label();
            this.qrModelSizeTb = new AntdUI.InputNumber();
            this.iconPathTb = new AntdUI.Input();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.iconSizePercentTb = new AntdUI.Slider();
            this.panel3 = new AntdUI.Panel();
            this.label1 = new AntdUI.Label();
            this.panel4 = new AntdUI.Panel();
            this.label4 = new AntdUI.Label();
            this.iconBackgroundColorCp = new AntdUI.ColorPicker();
            this.iconBorderWidthTb = new AntdUI.InputNumber();
            this.tabs1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // qrContent
            // 
            this.qrContent.Font = new System.Drawing.Font("宋体", 12F);
            this.qrContent.Location = new System.Drawing.Point(6, 26);
            this.qrContent.Name = "qrContent";
            this.qrContent.PrefixText = "二维码内容:";
            this.qrContent.Size = new System.Drawing.Size(336, 36);
            this.qrContent.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 446);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(334, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "生成二维码";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // image3D1
            // 
            this.image3D1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.image3D1.BackColor = System.Drawing.Color.Gainsboro;
            this.image3D1.Location = new System.Drawing.Point(348, 49);
            this.image3D1.Name = "image3D1";
            this.image3D1.Size = new System.Drawing.Size(770, 573);
            this.image3D1.TabIndex = 2;
            this.image3D1.Text = "image3D1";
            // 
            // tabs1
            // 
            this.tabs1.Controls.Add(this.tabPage1);
            this.tabs1.Controls.Add(this.tabPage2);
            this.tabs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs1.Location = new System.Drawing.Point(0, 0);
            this.tabs1.Name = "tabs1";
            this.tabs1.SelectedIndex = 0;
            this.tabs1.Size = new System.Drawing.Size(1134, 660);
            this.tabs1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.levelSelect);
            this.tabPage1.Controls.Add(this.drawQuietZonesSelect);
            this.tabPage1.Controls.Add(this.imageSizeLabel);
            this.tabPage1.Controls.Add(this.iconBorderWidthTb);
            this.tabPage1.Controls.Add(this.qrModelSizeTb);
            this.tabPage1.Controls.Add(this.image3D1);
            this.tabPage1.Controls.Add(this.iconPathTb);
            this.tabPage1.Controls.Add(this.qrContent);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1126, 630);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "二维码";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lightColorCp);
            this.panel2.Location = new System.Drawing.Point(11, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(326, 46);
            this.panel2.TabIndex = 9;
            this.panel2.Text = "panel1";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.darkColorCp);
            this.panel1.Location = new System.Drawing.Point(8, 149);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 46);
            this.panel1.TabIndex = 9;
            this.panel1.Text = "panel1";
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
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 24F);
            this.button2.ImageSvg = resources.GetString("button2.ImageSvg");
            this.button2.Location = new System.Drawing.Point(309, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 36);
            this.button2.TabIndex = 8;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // levelSelect
            // 
            this.levelSelect.Location = new System.Drawing.Point(6, 191);
            this.levelSelect.Name = "levelSelect";
            this.levelSelect.PrefixText = "纠错等级:";
            this.levelSelect.Size = new System.Drawing.Size(336, 36);
            this.levelSelect.TabIndex = 7;
            // 
            // drawQuietZonesSelect
            // 
            this.drawQuietZonesSelect.Location = new System.Drawing.Point(8, 233);
            this.drawQuietZonesSelect.Name = "drawQuietZonesSelect";
            this.drawQuietZonesSelect.PrefixText = "静止区尺寸:";
            this.drawQuietZonesSelect.Size = new System.Drawing.Size(336, 36);
            this.drawQuietZonesSelect.TabIndex = 7;
            // 
            // imageSizeLabel
            // 
            this.imageSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageSizeLabel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imageSizeLabel.Location = new System.Drawing.Point(351, 3);
            this.imageSizeLabel.Name = "imageSizeLabel";
            this.imageSizeLabel.Size = new System.Drawing.Size(767, 46);
            this.imageSizeLabel.TabIndex = 4;
            this.imageSizeLabel.Text = "";
            this.imageSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // qrModelSizeTb
            // 
            this.qrModelSizeTb.Font = new System.Drawing.Font("宋体", 12F);
            this.qrModelSizeTb.Location = new System.Drawing.Point(8, 65);
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
            this.qrModelSizeTb.TabIndex = 3;
            this.qrModelSizeTb.Text = "200";
            this.qrModelSizeTb.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // iconPathTb
            // 
            this.iconPathTb.Font = new System.Drawing.Font("宋体", 12F);
            this.iconPathTb.Location = new System.Drawing.Point(6, 275);
            this.iconPathTb.Name = "iconPathTb";
            this.iconPathTb.PrefixText = "水印图标:";
            this.iconPathTb.Size = new System.Drawing.Size(297, 36);
            this.iconPathTb.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1126, 630);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.panel3.Location = new System.Drawing.Point(8, 317);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(333, 39);
            this.panel3.TabIndex = 11;
            this.panel3.Text = "panel3";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 36);
            this.label1.TabIndex = 6;
            this.label1.Text = "水印比例:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.iconBackgroundColorCp);
            this.panel4.Location = new System.Drawing.Point(6, 351);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(332, 46);
            this.panel4.TabIndex = 9;
            this.panel4.Text = "panel1";
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
            // iconBorderWidthTb
            // 
            this.iconBorderWidthTb.Font = new System.Drawing.Font("宋体", 12F);
            this.iconBorderWidthTb.Location = new System.Drawing.Point(6, 404);
            this.iconBorderWidthTb.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.iconBorderWidthTb.Name = "iconBorderWidthTb";
            this.iconBorderWidthTb.PrefixText = "水印边框大小";
            this.iconBorderWidthTb.Size = new System.Drawing.Size(334, 36);
            this.iconBorderWidthTb.TabIndex = 3;
            this.iconBorderWidthTb.Text = "1";
            this.iconBorderWidthTb.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 660);
            this.Controls.Add(this.tabs1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabs1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private AntdUI.Input qrContent;
        private AntdUI.Button button1;
        private AntdUI.Image3D image3D1;
        private AntdUI.Tabs tabs1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private AntdUI.InputNumber qrModelSizeTb;
        private AntdUI.Label imageSizeLabel;
        private AntdUI.ColorPicker lightColorCp;
        private AntdUI.Label label2;
        private AntdUI.ColorPicker darkColorCp;
        private AntdUI.Select drawQuietZonesSelect;
        private AntdUI.Input iconPathTb;
        private AntdUI.Button button2;
        private AntdUI.Select levelSelect;
        private AntdUI.Panel panel1;
        private AntdUI.Panel panel2;
        private AntdUI.Label label3;
        private AntdUI.Panel panel3;
        private AntdUI.Label label1;
        private AntdUI.Slider iconSizePercentTb;
        private AntdUI.Panel panel4;
        private AntdUI.Label label4;
        private AntdUI.ColorPicker iconBackgroundColorCp;
        private AntdUI.InputNumber iconBorderWidthTb;
    }
}
