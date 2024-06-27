using HmExtension.Standard.Events;
using System;
using System.Runtime.InteropServices;
using static HmExtension.Standard.WindowApi.MessageHook;

namespace HmExtension.Standard.WindowApi;
/// <summary>
/// 系统消息钩子
/// </summary>
public class MessageHook:AbstractHook<MessageHookType>
{
    public event EventHandler<MessageEventArgs> OnMessage; 
    public enum MessageHookType
    {
       GETMESSAGE = HookType.WH_GETMESSAGE
    }

    public override int Hook(MessageHookType type = MessageHookType.GETMESSAGE)
    {
        return Hook((HookType)type, Handler, IntPtr.Zero, 0);
    }

    private void Handler(int arg1, IntPtr arg2)
    {
        var msg = Marshal.PtrToStructure<WinApi.TagMSG>(arg2);
        MessageEventArgs args = new MessageEventArgs(msg, arg1==1);
        OnMessage?.Invoke(this, args);
    }
}