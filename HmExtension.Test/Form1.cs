using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntdUI;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using HmExtension.Commons.Extensions;
using HmExtension.Extensions;
using HmExtension.utils;
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
                Bitmap icon = null;
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
                    icon = new Bitmap(iconPathTb.Text);
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
    }
}