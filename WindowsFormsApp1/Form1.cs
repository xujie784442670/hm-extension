using System;
using System.Windows.Forms;
using HmExtension.Standard;
using HmExtension.Standard.Events;
using HmExtension.Standard.Extensions;
using HmExtension.Standard.utils;
using HmExtension.Standard.WindowApi;
using KeyEventArgs = HmExtension.Standard.Events.KeyEventArgs;
using Keys = HmExtension.Standard.Commons.Keys;
using MouseEventArgs = HmExtension.Standard.Events.MouseEventArgs;

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