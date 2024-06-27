using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Keys = HmExtension.Standard.Commons.Keys;

namespace HmExtension.Standard.utils;

/// <summary>
/// 热键辅助类
/// </summary>
public class HotKeyHelper
{
    /// <summary>
    /// hook 注册热键
    /// </summary>
    /// <param name="hWnd">要定义热键的窗口的句柄</param>
    /// <param name="id">定义热键ID （不能与其它ID重复）</param>
    /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
    /// <param name="vk">组合 热键的内容</param>
    /// <returns></returns>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

    /// <summary>
    /// 卸载注册的热键
    /// </summary>
    /// <param name="hWnd">//要取消热键的窗口的句柄
    /// <param name="id">//要取消热键的ID
    /// <returns></returns>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
    [Flags()]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8,

    }

}