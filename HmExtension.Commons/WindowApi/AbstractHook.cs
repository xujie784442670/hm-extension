using System;
using System.Runtime.InteropServices;
using static HmExtension.Commons.WinApi;

namespace HmExtension.Commons.WindowApi;

/// <summary>
/// 钩子基类类型
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AbstractHook<T> : IDisposable where T: Enum
{
    private int _hookId;

    /// <summary>
    /// 钩子类型
    /// </summary>
    public HookType HookType { get; private set; }

    private Action<int, IntPtr> _hookProc;

    /// <summary>
    /// 点结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
        /// <summary>
        /// x坐标
        /// </summary>
        public int x;
        /// <summary>
        /// y坐标
        /// </summary>
        public int y;
    }
    /// <summary>
    /// 钩子
    /// </summary>
    /// <param name="hookType">钩子类型</param>
    /// <param name="lpfn">处理函数委托</param>
    /// <param name="hInstance"> 实例句柄</param>
    /// <param name="threadId">线程ID</param>
    /// <returns></returns>
    public int Hook(HookType hookType, Action<int, IntPtr> lpfn, IntPtr hInstance, int threadId)
    {
        HookType = hookType;
        _hookProc = lpfn;
        _hookId = SetWindowsHookEx(hookType, HookHandler, hInstance, threadId);
        return _hookId;
    }
    /// <summary>
    /// 钩子处理函数
    /// </summary>
    /// <param name="ncode"></param>
    /// <param name="wparam"></param>
    /// <param name="lparam"></param>
    /// <returns></returns>
    private protected int HookHandler(int ncode, IntPtr wparam, IntPtr lparam)
    {
        if (ncode >= 0)
        {
            _hookProc?.Invoke(wparam.ToInt32(), lparam);
        }
        return WinApi.CallNextHookEx(_hookId, ncode, wparam, lparam);
    }
    /// <summary>
    /// 卸载钩子
    /// </summary>
    public void UnHook()
    {
        WinApi.UnhookWindowsHookEx(_hookId);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        UnHook();
    }
    /// <summary>
    /// 析构函数
    /// </summary>
    ~AbstractHook()
    {
        Dispose();
    }
    /// <summary>
    /// 启动钩子
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public abstract int Hook(T type=default);
}