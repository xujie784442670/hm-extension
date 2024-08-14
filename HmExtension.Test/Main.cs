using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmExtension.Test
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormWebSocketTest().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormQrCodeTest().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FormCameraTest().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new FormFaceTest().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new FormWebSocketClientTest().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new FormMqttTest().Show();
        }
    }
}
