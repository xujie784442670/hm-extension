using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntdUI;
using ArcFaceProSDK4net.Models;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using HmExtension.Camera;
using HmExtension.Commons;
using HmExtension.Commons.Extensions;
using HmExtension.Drawing;
using HmExtension.Face;
using HmExtension.QRCode;
using ViewFaceCore;
using ViewFaceCore.Core;
using ViewFaceCore.Model;
using 摄像头枚举工具;
using Button = AntdUI.Button;
using Message = AntdUI.Message;

namespace HmExtension.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Loading = true;
                System.Drawing.Bitmap icon = null;
                ZXing.QrCode.Internal.ErrorCorrectionLevel level = (ZXing.QrCode.Internal.ErrorCorrectionLevel)levelSelect.SelectedValue;
                if (!string.IsNullOrEmpty(iconPathTb.Text))
                {
                    // 检查文件是否存在
                    if (!File.Exists(iconPathTb.Text))
                    {
                        Message.error(this, "图标不存在");
                        iconPathTb.Status = TType.Error;
                        return;
                    }

                    iconPathTb.Status = TType.None;
                    icon = new System.Drawing.Bitmap(iconPathTb.Text);
                }

                Task.Run(() =>
                {
                    var bitmap = qrContent.Text.ToQrCode(
                        level: level,
                        size: (int)qrModelSizeTb.Value,
                        iconSizePercent: (int)iconSizePercentTb.Value,
                        icon: icon,
                        iconBackgroundColor: iconBackgroundColorCp.Value,
                        iconBorderWidth: (int)iconBorderWidthTb.Value,
                        drawQuietZonesSize: 10);
                    imageSizeLabel.Text = $"宽度:{bitmap.Width},高度:{bitmap.Height}";

                    bitmap.DecodeQrCode().Println("识别结果:");

                    image3D1.Image = bitmap.ResizeImage(image3D1.Width, image3D1.Height, true);
                }).ContinueWith(t => { btn.Loading = false; }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        // L,
        // M,
        // Q,
        // H
        // private string[] LevelNames =
        // {
        //     "大约 7% 的错误更正能力",
        //     "大约 15% 的错误更正能力",
        //     "大约 25% 的错误更正能力",
        //     "大约 30% 的错误更正能力"
        // };

        private void Form1_Load(object sender, EventArgs e)
        {
            drawQuietZonesSelect.Items.Clear();
            drawQuietZonesSelect.Items.Add(1);
            drawQuietZonesSelect.Items.Add(2);
            drawQuietZonesSelect.Items.Add(3);
            drawQuietZonesSelect.Items.Add(4);
            drawQuietZonesSelect.Items.Add(5);
            drawQuietZonesSelect.SelectedValue = 2;


            levelSelect.Items.Clear();
            levelSelect.Items.Add(ErrorCorrectionLevel.L);
            levelSelect.Items.Add(ErrorCorrectionLevel.M);
            levelSelect.Items.Add(ErrorCorrectionLevel.Q);
            levelSelect.Items.Add(ErrorCorrectionLevel.H);
            levelSelect.SelectedIndex = 2;

            cameraList.Items.AddRange(CameraHelper.CameraNames);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog();
            // 默认目录设置为当前目录
            fd.InitialDirectory = Environment.CurrentDirectory;
            // 设置文件类型
            fd.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp";
            // 显示
            if (fd.ShowDialog() == DialogResult.OK)
            {
                iconPathTb.Text = fd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // WinApi.SetSystemCursor(Cursors.Default.CopyHandle(), OcrType.OCR_NORMAL);
            
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            // 修改桌面鼠标指针
            // WinApi.SetSystemCursor(Cursors.Arrow.Handle, OcrType.OCR_HAND);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // WinApi.SystemParametersInfo(87, 0, IntPtr.Zero, 2);
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            // WinApi.ResetSystemCursor();
            // var hander = WinApi.WindowFromPoint(new Point(e.X, e.Y));
            // hander.Println("窗口句柄: ");
            // var thandler = WinApi.GetWindow(hander,WindowType.GW_HWNDPREV);
            // if (thandler != null)
            // {
            //     hander = thandler;
            // }
            // WinApi.GetWindowText(hander).Println("窗口标题:");
            // WinApi.GetClassName(hander).Println("窗口类名:");
            
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
                
                try
                {
                    using FaceDetector fd = new FaceDetector();
                    var faceInfos = await fd.DetectAsync(bitmap);
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
                catch(Exception e)
                {
                    Console.WriteLine(e.Message+" "+e.StackTrace);
                }
                pictureBox1.Image = bitmap;
            });

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FaceHelper.RegisterFace((Image)pictureBox1.Image.Clone(),"测试1");
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
    }
}