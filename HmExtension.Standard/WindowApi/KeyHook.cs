using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HmExtension.Standard.Events;
using HmExtension.Standard.Extensions;
using static HmExtension.Standard.WindowApi.KeyHook;
using KeyEventArgs = HmExtension.Standard.Events.KeyEventArgs;
using Keys = HmExtension.Standard.Commons.Keys;

namespace HmExtension.Standard.WindowApi;

public class KeyHook:AbstractHook<KeyHookType>
{
    [StructLayout(LayoutKind.Sequential)]
    public class KBDLLHOOKSTRUCT
    {
        /// <summary>
        /// 虚拟按键代码。 代码必须是 1 到 254 范围内的值。
        /// </summary>
        public int vkCode;
        /// <summary>
        /// 按键的硬件扫描代码。
        /// </summary>
        public int scanCode;
        /// <summary>
        /// 扩展键标志、事件注入标志、上下文代码和转换状态标志。 此成员指定如下。 应用程序可以使用以下值来测试击键标志。 测试LLKHF_INJECTED (位 4) 将告知是否已注入事件。 如果是，则测试LLKHF_LOWER_IL_INJECTED (位 1) 会告诉你事件是否是从以较低完整性级别运行的进程注入的。
        /// </summary>
        public int flags;
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time;
        /// <summary>
        /// 额外信息
        /// </summary>
        public ulong dwExtraInfo;
    }


    /// <summary>
    /// 按键钩子类型
    /// </summary>
    public enum KeyHookType
    {
        /// <summary>
        /// 安装监视击键消息的挂钩过程。 有关详细信息，请参阅 KeyboardProc 挂钩过程。
        /// </summary>
        WH_KEYBOARD = HookType.WH_KEYBOARD,
        /// <summary>
        /// 安装用于监视低级别鼠标输入事件的挂钩过程。 有关详细信息，请参阅 LowLevelMouseProc 挂钩过程。
        /// </summary>
        WH_KEYBOARD_LL = HookType.WH_KEYBOARD_LL
    }

    private KeyHookType _hookType;

    public event EventHandler<KeyEventArgs> OnKey;

    public override int Hook(KeyHookType type = KeyHookType.WH_KEYBOARD_LL)
    {
        _hookType = KeyHookType.WH_KEYBOARD_LL;
        return Hook(HookType.WH_KEYBOARD_LL, KeyHookHandler, IntPtr.Zero, 0);
    }

    private void KeyHookHandler(int msgType, IntPtr lparam)
    {
        var eventType = (KeyEventType)msgType;
        KeyEventArgs eventArgs = null;
        if (_hookType == KeyHookType.WH_KEYBOARD_LL)
        {
            KBDLLHOOKSTRUCT hookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lparam, typeof(KBDLLHOOKSTRUCT));
            eventArgs = new KeyEventArgs(eventType, hookStruct);
        }
        else
        {
            eventArgs = new KeyEventArgs(msgType, lparam.ToInt32());
        }

        KeyEventArgs.IsLeftAlt = (WinApi.GetAsyncKeyState(Keys.LMenu) & 0x8000) != 0;
        KeyEventArgs.IsRightAlt = (WinApi.GetAsyncKeyState(Keys.RMenu) & 0x8000) != 0;
        KeyEventArgs.IsLeftCtrl = (WinApi.GetAsyncKeyState(Keys.LControlKey) & 0x8000) != 0;
        KeyEventArgs.IsRightCtrl = (WinApi.GetAsyncKeyState(Keys.RControlKey) & 0x8000) != 0;
        KeyEventArgs.IsLeftShift = (WinApi.GetAsyncKeyState(Keys.LShiftKey) & 0x8000) != 0;
        KeyEventArgs.IsRightShift = (WinApi.GetAsyncKeyState(Keys.RShiftKey) & 0x8000) != 0;
        KeyEventArgs.IsWin = (WinApi.GetAsyncKeyState(Keys.LWin) & 0x8000) != 0;
        KeyEventArgs.IsApps = (WinApi.GetAsyncKeyState(Keys.Apps) & 0x8000) != 0;

        OnKey?.Invoke(this, eventArgs);
    }
}
