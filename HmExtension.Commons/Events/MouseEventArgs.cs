using System;
using System.Windows.Forms;
using HmExtension.Commons.Commons;
using static HmExtension.Commons.WindowApi.MouseHook;

namespace HmExtension.Commons.Events;

/// <summary>
/// 鼠标事件参数
/// </summary>
public class MouseEventArgs
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public readonly MouseEventType EventType;

    /// <summary>
    /// 鼠标坐标
    /// </summary>
    public Point<int> Point;
    /// <summary>
    ///  窗口的句柄，该窗口将接收与鼠标事件对应的鼠标消息。
    /// </summary>
    public int Hwnd;
    /// <summary>
    /// 命中测试值
    /// </summary>
    public HitTest HitTestCode;

    /// <summary>
    /// 滚轮滚动量
    /// </summary>
    public int Delta;

    /// <summary>
    /// 点击次数
    /// </summary>
    public int Clicks;

    /// <summary>
    /// 按钮
    /// </summary>
    public MouseButton Button;

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp;

    /// <summary>
    /// 附加信息
    /// </summary>
    public uint ExtraInfo;

    /// <summary>
    /// 是否注入
    /// </summary>
    public bool Injected;

    /// <summary>
    /// 是否低完整性级别注入
    /// </summary>
    public bool LowerIlInjected;

    /// <summary>
    /// 初始化一个新的实例
    /// </summary>
    /// <param name="msgType">事件类型</param>
    /// <param name="mhs">钩子数据</param>
    public MouseEventArgs(int msgType, MsllHookStruct mhs)
    {
        Point = new Point<int>(mhs.pt.x, mhs.pt.y);
        Delta = mhs.mouseData >> 16;
        EventType = ParseEventType(msgType);
        Clicks = ParseMouseClick(EventType);
        Button = ParseMouseButton(msgType, mhs.mouseData);
        Timestamp = mhs.time;
        ExtraInfo = mhs.dwExtraInfo;
        Injected = (mhs.flags & LLMHF_INJECTED) != 0;
        LowerIlInjected = (mhs.flags & LLMHF_LOWER_IL_INJECTED) != 0;
    }
    /// <summary>
    /// 初始化一个新的实例
    /// </summary>
    /// <param name="msgType">事件类型</param>
    /// <param name="mhs">钩子数据</param>
    public MouseEventArgs(int msgType, MouseHookStruct mhs)
    {
        Point = new Point<int>(mhs.pt.x, mhs.pt.y);
        EventType = ParseEventType(msgType);
        Clicks = ParseMouseClick(EventType);
        Button = ParseMouseButton(msgType, 0);
        Timestamp = Environment.TickCount;
        ExtraInfo = mhs.dwExtraInfo;
        Injected = false;
        LowerIlInjected = false;
        HitTestCode = (HitTest)mhs.wHitTestCode;
    }
    /// <summary>
    /// 初始化一个新的实例
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="point">鼠标坐标</param>
    /// <param name="delta">滚轮滚动量</param>
    /// <param name="clicks">点击次数</param>
    /// <param name="button">鼠标按钮</param>
    /// <param name="timestamp">事件</param>
    /// <param name="extraInfo">额外信息</param>
    /// <param name="injected">是否注入</param>
    /// <param name="lowerIlInjected">是否低完整性级别注入</param>
    /// <param name="hitTestCode">命中测试类型</param>
    /// <param name="hwnd">窗口句柄</param>
    public MouseEventArgs(
        MouseEventType eventType,
        Point<int> point,
        int delta,
        int clicks,
        MouseButton button,
        int timestamp,
        uint extraInfo,
        bool injected=false,
        bool lowerIlInjected = false,
        HitTest hitTestCode = HitTest.NONE,
        int hwnd=0)
    {
        this.EventType = eventType;
        this.Point = point;
        this.Delta = delta;
        this.Clicks = clicks;
        this.Button = button;
        this.Timestamp = timestamp;
        this.ExtraInfo = extraInfo;
        this.Injected = injected;
        this.LowerIlInjected = lowerIlInjected;
        this.HitTestCode = hitTestCode;
        this.Hwnd = hwnd;
    }

    private static int ParseMouseClick(MouseEventType type)
    {
        return type switch
        {
            MouseEventType.LButtondblclk => 2,
            MouseEventType.RButtondblclk => 2,
            MouseEventType.MButtondblclk => 2,
            MouseEventType.XButtondblclk => 2,
            MouseEventType.NcxButtondblclk => 2,
            _ => 1
        };
    }

    private static MouseButton ParseMouseButton(int msgType, int mouseData)
    {
        var btn = mouseData >> 16;
        return msgType switch
        {
            WindowsMessage.WM_LBUTTONDOWN => MouseButton.LEFT,
            WindowsMessage.WM_LBUTTONUP => MouseButton.LEFT,
            WindowsMessage.WM_LBUTTONDBLCLK => MouseButton.LEFT,
            WindowsMessage.WM_RBUTTONDOWN => MouseButton.RIGHT,
            WindowsMessage.WM_RBUTTONUP => MouseButton.RIGHT,
            WindowsMessage.WM_RBUTTONDBLCLK => MouseButton.RIGHT,
            WindowsMessage.WM_MBUTTONDOWN => MouseButton.MIDDLE,
            WindowsMessage.WM_MBUTTONUP => MouseButton.MIDDLE,
            WindowsMessage.WM_MBUTTONDBLCLK => MouseButton.MIDDLE,
            WindowsMessage.WM_XBUTTONDOWN => btn switch
            {
                1 => MouseButton.XBUTTON1,
                2 => MouseButton.XBUTTON2,
                _ => MouseButton.NONE
            },
            WindowsMessage.WM_XBUTTONUP => btn switch
            {
                1 => MouseButton.XBUTTON1,
                2 => MouseButton.XBUTTON2,
                _ => MouseButton.NONE
            },
            WindowsMessage.WM_XBUTTONDBLCLK => btn switch
            {
                1 => MouseButton.XBUTTON1,
                2 => MouseButton.XBUTTON2,
                _ => MouseButton.NONE
            },
            WindowsMessage.WM_NCXBUTTONDOWN => btn switch
            {
                1 => MouseButton.XBUTTON1,
                2 => MouseButton.XBUTTON2,
                _ => MouseButton.NONE
            },
            WindowsMessage.WM_NCXBUTTONUP => btn switch
            {
                1 => MouseButton.XBUTTON1,
                2 => MouseButton.XBUTTON2,
                _ => MouseButton.NONE
            },
            WindowsMessage.WM_NCXBUTTONDBLCLK => btn switch
            {
                1 => MouseButton.XBUTTON1,
                2 => MouseButton.XBUTTON2,
                _ => MouseButton.NONE
            },
            _ => MouseButton.NONE
        };
    }

    private static MouseEventType ParseEventType(int msgType)
    {
        return msgType switch
        {
            WindowsMessage.WM_LBUTTONDOWN => MouseEventType.LButtonDown,
            WindowsMessage.WM_LBUTTONUP => MouseEventType.LButtonUp,
            WindowsMessage.WM_LBUTTONDBLCLK => MouseEventType.LButtondblclk,
            WindowsMessage.WM_RBUTTONDOWN => MouseEventType.RButtonDown,
            WindowsMessage.WM_RBUTTONUP => MouseEventType.RButtonUp,
            WindowsMessage.WM_RBUTTONDBLCLK => MouseEventType.RButtondblclk,
            WindowsMessage.WM_MBUTTONDOWN => MouseEventType.MButtonDown,
            WindowsMessage.WM_MBUTTONUP => MouseEventType.MButtonUp,
            WindowsMessage.WM_MBUTTONDBLCLK => MouseEventType.MButtondblclk,
            WindowsMessage.WM_MOUSEWHEEL => MouseEventType.Mousewheel,
            WindowsMessage.WM_XBUTTONDOWN => MouseEventType.XButtonDown,
            WindowsMessage.WM_XBUTTONUP => MouseEventType.XButtonUp,
            WindowsMessage.WM_XBUTTONDBLCLK => MouseEventType.XButtondblclk,
            WindowsMessage.WM_NCXBUTTONDBLCLK => MouseEventType.NcxButtondblclk,
            WindowsMessage.WM_NCXBUTTONDOWN => MouseEventType.NcxButtonDown,
            WindowsMessage.WM_NCXBUTTONUP => MouseEventType.NcxButtonUp,
            _ => MouseEventType.Mousemove
        };
    }
}
/// <summary>
/// 鼠标事件类型
/// </summary>
public enum MouseEventType
{
    /// <summary>
    /// 鼠标移动
    /// </summary>
    Mousemove,
    /// <summary>
    /// 鼠标左键按下
    /// </summary>
    LButtonDown,
    /// <summary>
    /// 鼠标左键弹起
    /// </summary>
    LButtonUp,
    /// <summary>
    /// 鼠标左键双击
    /// </summary>
    LButtondblclk,
    /// <summary>
    /// 鼠标右键按下
    /// </summary>
    RButtonDown,
    /// <summary>
    /// 鼠标右键弹起
    /// </summary>
    RButtonUp,
    /// <summary>
    /// 鼠标右键双击
    /// </summary>
    RButtondblclk,
    /// <summary>
    /// 鼠标中键按下
    /// </summary>
    MButtonDown,
    /// <summary>
    /// 鼠标中键弹起
    /// </summary>
    MButtonUp,
    /// <summary>
    /// 鼠标中键双击
    /// </summary>
    MButtondblclk,
    /// <summary>
    /// 鼠标滚轮滚动
    /// </summary>
    Mousewheel,
    /// <summary>
    /// 鼠标X键按下
    /// </summary>
    XButtonDown,
    /// <summary>
    /// 鼠标X键弹起
    /// </summary>
    XButtonUp,
    /// <summary>
    /// 鼠标X键双击
    /// </summary>
    XButtondblclk,
    /// <summary>
    /// 鼠标非客户区按下
    /// </summary>
    NcxButtondblclk,
    /// <summary>
    /// 鼠标非客户区按下
    /// </summary>
    NcxButtonDown,
    /// <summary>
    /// 鼠标非客户区弹起
    /// </summary>
    NcxButtonUp
}

/// <summary>
/// 鼠标按钮
/// </summary>
public enum MouseButton
{
    /// <summary>
    /// 无
    /// </summary>
    NONE,
    /// <summary>
    /// X键2
    /// </summary>
    XBUTTON2,
    /// <summary>
    /// X键1
    /// </summary>
    XBUTTON1,
    /// <summary>
    /// 鼠标左键
    /// </summary>
    LEFT,
    /// <summary>
    /// 鼠标右键
    /// </summary>
    RIGHT,
    /// <summary>
    /// 鼠标中键
    /// </summary>
    MIDDLE
}
/// <summary>
/// 命中测试值
/// </summary>
public enum HitTest
{
    /// <summary>
    /// 无效
    /// </summary>
    NONE = 0,
    /// <summary>
    /// 在没有大小调整边框的窗口边框中。
    /// </summary>
    HTBORDER = 18,
    /// <summary>
    /// 在可调整大小的窗口的下水平边框中（用户可以单击鼠标以垂直调整窗口大小）
    /// </summary>
    HTBOTTOM = 15,
    /// <summary>
    /// 在可调整大小的窗口的边框左下角（用户可以单击鼠标以对角线调整窗口大小）。
    /// </summary>
    HTBOTTOMLEFT = 16,
    /// <summary>
    /// 在可调整大小的窗口的边框右下角（用户可以单击鼠标以对角线调整窗口大小）。
    /// </summary>
    HTBOTTOMRIGHT = 17,
    /// <summary>
    /// 在标题栏中。
    /// </summary>
    HTCAPTION = 2,
    /// <summary>
    /// 在工作区中。
    /// </summary>
    HTCLIENT = 1,
    /// <summary>
    /// 在“关闭”按钮中。
    /// </summary>
    HTCLOSE = 20,
    /// <summary>
    /// 在屏幕背景上或窗口之间的分割线上（与 HTNOWHERE 相同，只是 DefWindowProc 函数会生成系统蜂鸣音以指示错误）。
    /// </summary>
    HTERROR = -2,
    /// <summary>
    /// 在大小框中（与 HTSIZE 相同）。
    /// </summary>
    HTGROWBOX = 4,
    /// <summary>
    /// 在“帮助”按钮中。
    /// </summary>
    HTHELP = 21,
    /// <summary>
    /// 在水平滚动条中。
    /// </summary>
    HTHSCROLL = 6,
    /// <summary>
    /// 在可调整大小的窗口的左边框中（用户可以单击鼠标以水平调整窗口大小）。
    /// </summary>
    HTLEFT = 10,
    /// <summary>
    /// 在“最大化”按钮中。
    /// </summary>
    HTMAXBUTTON = 9,
    /// <summary>
    /// 在菜单中。
    /// </summary>
    HTMENU = 5,
    /// <summary>
    /// 在“最小化”按钮中。
    /// </summary>
    HTMINBUTTON = 8,
    /// <summary>
    /// 在屏幕背景上，或在窗口之间的分隔线上。
    /// </summary>
    HTNOWHERE = 0,
    /// <summary>
    /// 在窗口菜单或子窗口的关闭按钮中。
    /// </summary>
    HTSYSMENU = 3,
    /// <summary>
    /// 在“最小化”按钮中。
    /// </summary>
    HTREDUCE = 8,
    /// <summary>
    /// 在可调整大小的窗口的右左边框中（用户可以单击鼠标以水平调整窗口大小）。
    /// </summary>
    HTRIGHT = 11,
    /// <summary>
    /// 在大小框中（与 HTGROWBOX 相同）。
    /// </summary>
    HTSIZE = 4,
    /// <summary>
    /// 在窗口的上水平边框中。
    /// </summary>
    HTTOP = 12,
    /// <summary>
    /// 在窗口边框的左上角。
    /// </summary>
    HTTOPLEFT = 13,
    /// <summary>
    /// 在窗口边框的右上角。
    /// </summary>
    HTTOPRIGHT = 14,
    /// <summary>
    /// 在同一线程当前由另一个窗口覆盖的窗口中（消息将发送到同一线程中的基础窗口，直到其中一个窗口返回不是 HTTRANSPARENT 的代码）。
    /// </summary>
    HTTRANSPARENT = -1,
    /// <summary>
    /// 在垂直滚动条中
    /// </summary>
    HTVSCROLL = 7,
    /// <summary>
    /// 在最大化按钮中
    /// </summary>
    HTZOOM = 9,
}