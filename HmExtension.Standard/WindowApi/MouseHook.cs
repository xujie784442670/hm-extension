using System;
using System.Runtime.InteropServices;
using HmExtension.Standard.Events;
using static HmExtension.Standard.WindowApi.MouseHook;

namespace HmExtension.Standard.WindowApi;

/// <summary>
/// 鼠标钩子
/// </summary>
public class MouseHook : AbstractHook<MouseHookType>
{
    /// <summary>
    /// 从任何进程(标志测试事件注入)
    /// </summary>
    public const int LLMHF_INJECTED = 0x00000001;

    /// <summary>
    /// 从以较低完整性级别(标志运行的进程测试事件注入 )
    /// </summary>
    public const int LLMHF_LOWER_IL_INJECTED = 0x00000002;

    /// <summary>
    /// 鼠标事件参数
    /// </summary>
    public enum MouseHookType
    {
        /// <summary>
        /// 安装监视鼠标消息的挂钩过程。 有关详细信息，请参阅<a href="https://learn.microsoft.com/zh-cn/windows/win32/winmsg/mouseproc">MouseProc</a>挂钩过程。
        /// </summary>
        WH_MOUSE = 7,

        /// <summary>
        /// 安装用于监视低级别鼠标输入事件的挂钩过程。 有关详细信息，请参阅<a href="https://learn.microsoft.com/zh-cn/windows/win32/winmsg/lowlevelmouseproc">LowLevelMouseProc </a> 挂钩过程。
        /// </summary>
        WH_MOUSE_LL = 14
    }

    /// <summary>
    /// 鼠标事件参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class MouseHookStruct
    {
        /// <summary>
        /// 光标的 x 和 y 坐标（以屏幕坐标表示）
        /// </summary>
        public POINT pt;

        /// <summary>
        /// 窗口的句柄，该窗口将接收与鼠标事件对应的鼠标消息。
        /// </summary>
        public int hwnd;

        /// <summary>
        /// 命中测试值。 有关命中测试值的列表，请参阅 <a href="https://learn.microsoft.com/zh-cn/windows/win32/inputdev/wm-nchittest">WM_NCHITTEST</a>  消息的说明。
        /// </summary>
        public int wHitTestCode;

        /// <summary>
        /// 与消息关联的其他信息。
        /// </summary>
        public uint dwExtraInfo;
    }

    /// <summary>
    /// 鼠标事件参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class MsllHookStruct
    {
        /// <summary>
        /// 光标的 x 和 y 坐标，按 <a href="https://learn.microsoft.com/zh-cn/windows/win32/api/shellscalingapi/ne-shellscalingapi-process_dpi_awareness">监视器感知</a> 的屏幕坐标。
        /// </summary>
        public POINT pt;

        /// <summary>
        /// 如果消息 <see cref="WindowsMessage.WM_MOUSEWHEEL">WM_MOUSEWHEEL</see>，则此成员的高序字是滚轮增量。 保留低序字。 正值表示滚轮向前旋转（远离用户）；负值表示滚轮向后旋转（朝向用户）。 一键滚轮定义为 WHEEL_DELTA，即 120。
        /// 如果消息
        /// <see cref="WindowsMessage.WM_XBUTTONDOWN">WM_XBUTTONDOWN</see>、
        /// <see cref="WindowsMessage.WM_XBUTTONUP">WM_XBUTTONUP</see>、
        /// <see cref="WindowsMessage.WM_XBUTTONDBLCLK">WM_XBUTTONDBLCLK</see>、
        /// <see cref="WindowsMessage.WM_NCXBUTTONDOWN">WM_NCXBUTTONDOWN</see>、
        /// <see cref="WindowsMessage.WM_NCXBUTTONUP">WM_NCXBUTTONUP</see>或
        /// <see cref="WindowsMessage.WM_NCXBUTTONDBLCLK">WM_NCXBUTTONDBLCLK</see>，则高序单词指定按下或释放的 X 按钮，并且保留低序字。 此值可以是以下一个或多个值。 否则，不使用 mouseData 。
        /// </summary>
        public int mouseData;

        /// <summary>
        /// 事件注入的标志。 应用程序可以使用以下值来测试标志。 测试<see cref="LLMHF_INJECTED">LLMHF_INJECTED</see> 将告知是否已注入事件。 如果是，则测试<see cref="LLMHF_LOWER_IL_INJECTED">LLMHF_LOWER_IL_INJECTED</see> 将告诉你事件是否是从以较低完整性级别运行的进程注入的。
        /// </summary>
        public int flags;

        /// <summary>
        /// 此消息的时间戳。
        /// </summary>
        public long time;

        /// <summary>
        /// 与消息关联的其他信息。
        /// </summary>
        public uint dwExtraInfo;
    }

    /// <summary>
    /// 鼠标事件
    /// </summary>
    public event EventHandler<MouseEventArgs> OnMouse;

    private MouseHookType _hookType;
    /// <summary>
    /// 钩子
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public override int Hook(MouseHookType type = MouseHookType.WH_MOUSE_LL)
    {
        _hookType = type;
        return Hook(type switch
        {
            MouseHookType.WH_MOUSE => HookType.WH_MOUSE,
            MouseHookType.WH_MOUSE_LL => HookType.WH_MOUSE_LL,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        }, MouseHookProc, IntPtr.Zero, 0);
    }

    private void MouseHookProc(int msgType, IntPtr lparam)
    {
        MouseEventArgs eventArgs = null;
        if (_hookType == MouseHookType.WH_MOUSE_LL)
        {
            MsllHookStruct hookStruct = (MsllHookStruct)Marshal.PtrToStructure(lparam, typeof(MsllHookStruct));
            eventArgs = new MouseEventArgs(msgType, hookStruct);
        }
        else
        {
            MouseHookStruct hookStruct = (MouseHookStruct)Marshal.PtrToStructure(lparam, typeof(MouseHookStruct));
            eventArgs = new MouseEventArgs(msgType, hookStruct);
        }
        OnMouse?.Invoke(this, eventArgs);
    }
}