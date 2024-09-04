using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntdUI;
using HmExtension.DataSimulation;
using HmExtension.DataSimulation.simulator;
using Button = AntdUI.Button;

namespace HmExtension.Test.simulator
{
    public partial class FormTemperatureSimulator : Form
    {
        private IDataSimulation dataSimulation = new BaseSimulation();

        public double Increment
        {
            get
            {
                double.TryParse(toolStripTextBox1.Text, out var value);
                return value;
            }
            set => toolStripTextBox1.Text = $"{value}";
        }

        public int MaxDataCount
        {
            get
            {
                int.TryParse(toolStripTextBox2.Text, out var value);
                return value;
            }
            set => toolStripTextBox2.Text = $"{value}";
        }

        public double CurrentValue { get; set; } = 0;


        public FormTemperatureSimulator()
        {
            InitializeComponent();
        }

        private void FormTemperatureSimulator_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            // 设置X轴时间格式
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss.SSS";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var value = dataSimulation.SuperpositionValue(CurrentValue);
            if (chart1.Series[0].Points.Count == MaxDataCount)
            {
                for (var i = 0; i < chart1.Series[0].Points.Count-1; i++)
                {
                    chart1.Series[0].Points[i].XValue = chart1.Series[0].Points[i + 1].XValue;
                    chart1.Series[0].Points[i].YValues[0] = chart1.Series[0].Points[i + 1].YValues[0];
                }
                chart1.Series[0].Points[MaxDataCount - 1].XValue = DateTime.Now.ToOADate();
                chart1.Series[0].Points[MaxDataCount - 1].YValues[0] = value;
            }
            else
            {
                chart1.Series[0].Points.AddXY(DateTime.Now, value);
            }

            CurrentValue += Increment;
            if (CurrentValue >= double.MaxValue)
            {
                CurrentValue = 0;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton btn)
            {
                btn.Text = timer1.Enabled ? "开始" : "停止";
                timer1.Enabled = !timer1.Enabled;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var form = new FormSelectFunction(simulation =>
            {
                dataSimulation.SuperpositionSimulations.Add(simulation);
                var item = new ListViewItem($"{simulation.DisplayName}({simulation.Name})");
                simulation.OnValueChanged += value =>
                {
                    item.Text = $"{simulation.DisplayName}({simulation.Name})";
                    // 更新界面
                    listBox1.Update();
                };
                listBox1.Items.Add(item);
            });
            form.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            dataSimulation.SuperpositionSimulations.Clear();
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {
        }
    }
}