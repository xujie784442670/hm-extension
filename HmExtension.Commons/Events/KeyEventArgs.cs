using HmExtension.Commons.WindowApi;

namespace HmExtension.Commons.Events;
/// <summary>
/// 按键事件参数
/// </summary>
public class KeyEventArgs
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public KeyEventType EventType { get; set; }
    /// <summary>
    /// 按键次数
    /// </summary>
    public int Count;
    /// <summary>
    /// 是否是扩展键(如功能键,数字键盘上的键)
    /// </summary>
    public bool IsExtend;
    /// <summary>
    /// 是否抬起
    /// </summary>
    public  bool IsUp;
    /// <summary>
    /// 是否按下
    /// </summary>
    public bool IsDown;

    /// <summary>
    /// 虚拟按键代码。 代码必须是 1 到 254 范围内的值。
    /// </summary>
    public int Code;
    /// <summary>
    /// 按键的硬件扫描代码。
    /// </summary>
    public int ScanCode;

    /// <summary>
    /// 按键事件
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="hookStruct"></param>
    public KeyEventArgs(KeyEventType eventType, KeyHook.KBDLLHOOKSTRUCT hookStruct)
    {
        EventType = eventType;
        Count = 1;
        IsExtend = (hookStruct.flags & 1) == 1;
        IsUp = eventType == KeyEventType.KEYUP || eventType == KeyEventType.SYSKEYUP;
        IsDown = eventType == KeyEventType.KEYDOWN || eventType == KeyEventType.SYSKEYDOWN;
        Code = hookStruct.vkCode;
        ScanCode = hookStruct.scanCode;
    }
    /// <summary>
    /// 按键事件
    /// </summary>
    /// <param name="code"></param>
    /// <param name="hookStruct"></param>
    public KeyEventArgs(int code, int hookStruct)
    {
        EventType = ((hookStruct >> 31) & 1) == 1 ? KeyEventType.KEYUP: KeyEventType.KEYDOWN;
        Count = hookStruct & 0xFFFF;
        IsExtend = (hookStruct >> 24 & 1) == 1;
        IsUp = EventType == KeyEventType.KEYUP;
        IsDown = EventType == KeyEventType.KEYDOWN;
        Code = code;
        ScanCode = hookStruct >> 16 & 0xFF;
    }

    /// <summary>
    /// 是否按下左ALT键
    /// </summary>
    public static bool IsLeftAlt { get;internal set; }
    /// <summary>
    /// 是否按下右ALT键
    /// </summary>
    public static bool IsRightAlt { get; internal set; }
    /// <summary>
    /// 是否按下ALT键
    /// </summary>
    public static bool IsAlt => IsLeftAlt || IsRightAlt;
    /// <summary>
    /// 是否按下左Ctrl键
    /// </summary>
    public static bool IsLeftCtrl { get; internal set; }
    /// <summary>
    /// 是否按下右Ctrl键
    /// </summary>
    public static bool IsRightCtrl { get; internal set; }
    /// <summary>
    /// 是否按下Ctrl键
    /// </summary>
    public static bool IsCtrl => IsLeftCtrl || IsRightCtrl;
    /// <summary>
    /// 是否按下左Shift键
    /// </summary>
    public static bool IsLeftShift { get; internal set; }
    /// <summary>
    /// 是否按下右Shift键
    /// </summary>
    public static bool IsRightShift { get; internal set; }

    /// <summary>
    /// 是否按下Shift键
    /// </summary>
    public static bool IsShift => IsLeftShift || IsRightShift;
    /// <summary>
    /// 是否是系统键
    /// </summary>
    public static bool IsWin { get; internal set; }
    /// <summary>
    /// 是否按下应用键
    /// </summary>
    public static bool IsApps { get; internal set; }

    /// <summary>
    /// 输出字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"Code:{Code} ScanCode:{ScanCode} IsExtend:{IsExtend} IsUp:{IsUp} IsDown:{IsDown} IsLeftAlt:{IsLeftAlt} IsRightAlt:{IsRightAlt} IsAlt:{IsAlt} IsLeftCtrl:{IsLeftCtrl} IsRightCtrl:{IsRightCtrl} IsCtrl:{IsCtrl} IsLeftShift:{IsLeftShift} IsRightShift:{IsRightShift} IsShift:{IsShift} IsWin:{IsWin} IsApps:{IsApps}";
    }
}

/// <summary>
/// 按键事件类型
/// </summary>
public enum KeyEventType
{
    /// <summary>
    /// 按下一个键 
    /// </summary>
    KEYDOWN = WindowsMessage.WM_KEYDOWN,
    /// <summary>
    /// 释放一个键
    /// </summary>
    KEYUP = WindowsMessage.WM_KEYUP,
    /// <summary>
    /// 当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口 
    /// </summary>
    SYSKEYDOWN = WindowsMessage.WM_SYSKEYDOWN,
    /// <summary>
    /// 当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口 
    /// </summary>
    SYSKEYUP = WindowsMessage.WM_SYSKEYUP
}