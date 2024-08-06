using System;
using System.Runtime.InteropServices;
using HmExtension.Commons;
using HmExtension.Commons.Events;

namespace HmExtension.WindowApi;
/// <summary>
/// 系统消息钩子
/// </summary>
public class MessageHook:AbstractHook<MessageHook.MessageHookType>
{
    /// <summary>
    /// 消息事件参数
    /// </summary>
    public event EventHandler<MessageEventArgs> OnMessage; 
    /// <summary>
    /// 消息钩子类型
    /// </summary>
    public enum MessageHookType
    {
       /// <summary>
       /// 安装一个挂钩过程，该挂钩过程监视消息在消息队列中的所有消息。有关详细信息，请参阅 GetMsgProc 挂钩过程。
       /// </summary>
       GETMESSAGE = HookType.WH_GETMESSAGE
    }

    /// <summary>
    /// 钩子
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
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