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
        //     "大约 7% 的错误更正能力",
        //     "大约 15% 的错误更正能力",
        //     "大约 25% 的错误更正能力",
        //     "大约 30% 的错误更正能力"
        // };

        private void Form1_Load(object sender, EventArgs e)
        {
            
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