using AntdUI;
using HmExtension.Commons.Extensions;
using HmExtension.Standard.Extensions;
using HmExtension.Standard.utils;
using ZXing.QrCode.Internal;
using Button = AntdUI.Button;
using Message = AntdUI.Message;

namespace HmExtension.Standard.Test
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
                ErrorCorrectionLevel? level = (ErrorCorrectionLevel)levelSelect.SelectedValue!;
                if (!string.IsNullOrEmpty(iconPathTb.Text))
                {
                    // ����ļ��Ƿ����
                    if (!File.Exists(iconPathTb.Text))
                    {
                        Message.error(this, "ͼ�겻����");
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
                    imageSizeLabel.Text = $"���:{bitmap.Width},�߶�:{bitmap.Height}";

                    // bitmap.DecodeQrCode().Println("ʶ����:");
                    //
                    // image3D1.Image = bitmap.ResizeImage(image3D1.Width, image3D1.Height, true);
                }).ContinueWith(t => { btn.Loading = false; }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        // L,
        // M,
        // Q,
        // H
        // private string[] LevelNames =
        // {
        //     "��Լ 7% �Ĵ����������",
        //     "��Լ 15% �Ĵ����������",
        //     "��Լ 25% �Ĵ����������",
        //     "��Լ 30% �Ĵ����������"
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
            // Ĭ��Ŀ¼����Ϊ��ǰĿ¼
            fd.InitialDirectory = Environment.CurrentDirectory;
            // �����ļ�����
            fd.Filter = "ͼƬ�ļ�|*.jpg;*.jpeg;*.png;*.bmp";
            // ��ʾ
            if (fd.ShowDialog() == DialogResult.OK)
            {
                iconPathTb.Text = fd.FileName;
            }
        }
    }
}