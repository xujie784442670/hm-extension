using System;
using System.Windows.Forms;
using HmExtension.Standard;
using HmExtension.Commons.Events;
using HmExtension.Commons.Extensions;
using HmExtension.Commons.utils;
using HmExtension.Standard.utils;
using HmExtension.Commons.WindowApi;
using KeyEventArgs = HmExtension.Commons.Events.KeyEventArgs;
using Keys = HmExtension.Commons.Commons.Keys;
using MouseEventArgs = HmExtension.Commons.Events.MouseEventArgs;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MouseHook mh;
        private void Form1_Load(object sender, EventArgs e)
        {
            // mh = new MouseHook();
            // mh.OnMouse += mh_MouseMoveEvent;
            // mh.Hook();
            HotKeyHelper.RegisterHotKey(this.Handle, 1,HotKeyHelper.KeyModifiers.Ctrl,Keys.A);

           MessageHook mh = new MessageHook();
           mh.OnMessage += MhOnOnMessage;
           mh.Hook();
        }

        private void MhOnOnMessage(object sender, MessageEventArgs e)
        {
            e.Message.Println();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            // mh.UnHook();
        }
    }
}