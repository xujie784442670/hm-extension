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
using HmExtension.Face;
using System.Diagnostics;
using ViewFaceCore;
using ViewFaceCore.Core;

namespace HmExtension.Test
{
    public partial class FormFaceTest : Form
    {
        public FormFaceTest()
        {
            InitializeComponent();
        }

        private void FormFaceTest_Load(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            FaceHelper.RegisterFace((Image)pictureBox1.Image.Clone(), "测试1");
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            // var bitmap = new Bitmap("test.jpg");
            // var faceInfos =await FaceHelper.Detect(bitmap);
            // var faceMarkPoints = FaceHelper.FaceMark(bitmap, faceInfos[0]);
            // var extract = FaceHelper.Extract(bitmap, faceMarkPoints);
            var findFaceInfo = FaceHelper.Match((Image)pictureBox1.Image.Clone());
            var stop = new Stopwatch();
            stop.Start();
            findFaceInfo.ContinueWith(t =>
            {
                stop.Stop();
                MessageBox.Show($"识别结果: {t.Result} 耗时: {stop.Elapsed}");
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BeginInvoke(async () =>
            {
                var readFame = CameraHelper.ReadFame();
                var bitmap = readFame.ToBitmap();

                try
                {
                    using FaceDetector fd = new FaceDetector();
                    var faceInfos = await fd.DetectAsync(bitmap.ToFaceImage());
                    if (faceInfos is { Length: > 0 })
                    {
                        foreach (var faceInfo in faceInfos)
                        {
                            bitmap.DrawRect(new Rectangle(faceInfo.Location.X, faceInfo.Location.Y, faceInfo.Location.Width,
                                faceInfo.Location.Height));
                            bitmap.DrawText(faceInfo.Score + "",
                                new Point(faceInfo.Location.X + (faceInfo.Location.Width / 2),
                                    faceInfo.Location.Y - 20));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " " + e.StackTrace);
                }
                pictureBox1.Image = bitmap;
            });
        }
    }
}
