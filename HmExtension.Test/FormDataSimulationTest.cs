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
using HmExtension.Test.simulator;

namespace HmExtension.Test
{
    public partial class FormDataSimulationTest : Form
    {
        public FormDataSimulationTest()
        {
            InitializeComponent();
        }

       
        private void FormDataSimulationTest_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormTemperatureSimulator().Show();
        }
    }
}
