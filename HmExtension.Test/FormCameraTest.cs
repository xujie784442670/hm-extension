using HmExtension.Camera;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HmExtension.Drawing;
using ViewFaceCore.Core;

namespace HmExtension.Test
{
    public partial class FormCameraTest : Form
    {
        public FormCameraTest()
        {
            InitializeComponent();
        }

        private void FormCameraTest_Load(object sender, EventArgs e)
        {
            cameraList.Items.AddRange(CameraHelper.CameraNames);
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CameraHelper.CloseCamera();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CameraHelper.OpenCamera(cameraList.SelectedIndex);
            timer1.Enabled = true;
            timer1.Interval = 1000 / 30;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BeginInvoke(async () =>
            {
                var readFame = CameraHelper.ReadFame();
                var bitmap = readFame.ToBitmap();
                pictureBox1.Image = bitmap;
            });

        }

      
    }
}
