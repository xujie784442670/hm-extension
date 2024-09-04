namespace HmExtension.Test.simulator
{
    partial class FormSelectFunction
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
            this.select1 = new AntdUI.Select();
            this.inputNumber1 = new AntdUI.InputNumber();
            this.button1 = new AntdUI.Button();
            this.inputNumber2 = new AntdUI.InputNumber();
            this.SuspendLayout();
            // 
            // select1
            // 
            this.select1.Items.AddRange(new object[] {
            "Sin",
            "Cos"});
            this.select1.Location = new System.Drawing.Point(15, 12);
            this.select1.Name = "select1";
            this.select1.PrefixText = "函数:";
            this.select1.Size = new System.Drawing.Size(266, 42);
            this.select1.TabIndex = 0;
            // 
            // inputNumber1
            // 
            this.inputNumber1.DecimalPlaces = 2;
            this.inputNumber1.Location = new System.Drawing.Point(15, 60);
            this.inputNumber1.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.inputNumber1.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.inputNumber1.Name = "inputNumber1";
            this.inputNumber1.PrefixText = "初始值:";
            this.inputNumber1.Size = new System.Drawing.Size(266, 42);
            this.inputNumber1.TabIndex = 1;
            this.inputNumber1.Text = "0.00";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 44);
            this.button1.TabIndex = 2;
            this.button1.Text = "添加";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // inputNumber2
            // 
            this.inputNumber2.DecimalPlaces = 2;
            this.inputNumber2.Location = new System.Drawing.Point(15, 111);
            this.inputNumber2.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.inputNumber2.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.inputNumber2.Name = "inputNumber2";
            this.inputNumber2.PrefixText = "增量:";
            this.inputNumber2.Size = new System.Drawing.Size(266, 42);
            this.inputNumber2.TabIndex = 1;
            this.inputNumber2.Text = "0.00";
            // 
            // FormSelectFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 229);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.inputNumber2);
            this.Controls.Add(this.inputNumber1);
            this.Controls.Add(this.select1);
            this.Name = "FormSelectFunction";
            this.Text = "添加函数";
            this.Load += new System.EventHandler(this.FormSelectFunction_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Select select1;
        private AntdUI.InputNumber inputNumber1;
        private AntdUI.Button button1;
        private AntdUI.InputNumber inputNumber2;
    }
}