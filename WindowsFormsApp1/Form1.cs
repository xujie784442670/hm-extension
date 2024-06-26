using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HmExtension.Standard;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var intPtr = this.Handle;
            Task.Run(() =>
            {
                while (true)
                {
                    var b = WinAPI.GetMessage(out var msg, new IntPtr(), 0, 0);
                    Console.WriteLine($"GetMessage: {b}, {msg}");
                }
            });
        }
    }
}
