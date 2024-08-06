using AntdUI;
using HmExtension.Commons.Extensions;
using HmExtension.Standard.Extensions;
using HmExtension.Standard.utils;
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