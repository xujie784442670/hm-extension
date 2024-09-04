using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HmExtension.DataSimulation;
using HmExtension.DataSimulation.simulator;

namespace HmExtension.Test.simulator
{
    public partial class FormSelectFunction : Form
    {

        private Func<double,double,IDataSimulation>[] _functions = [
            (increment,initValue)=>new SinSimulator(increment,initValue),
            (increment,initValue)=>new CosSimulator(increment,initValue),
            (increment,initValue)=>new TanSimulator(increment,initValue),
        ];

        public double Increment
        {
            get => (double)inputNumber2.Value;
            set => inputNumber2.Value = (decimal)value;
        }

        public double InitValue
        {
            get => (double)inputNumber1.Value;
            set => inputNumber1.Value = (decimal)value;
        }

        private readonly Action<IDataSimulation> _retuenFunc;

        private readonly object[] _functionNames =
        [
            "正弦",
            "余弦",
            "正切",
        ];

        public FormSelectFunction(Action<IDataSimulation> retuenFunc)
        {
            _retuenFunc = retuenFunc;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (select1.SelectedIndex < 0)
            {
                MessageBox.Show("请选择一个函数");
                return;
            }

            var index = select1.SelectedIndex;
            _retuenFunc(_functions[index](Increment, InitValue));
            Close();
        }

        private void FormSelectFunction_Load(object sender, EventArgs e)
        {
            select1.Items.Clear();
            select1.Items.AddRange(_functionNames);
        }
    }
}
