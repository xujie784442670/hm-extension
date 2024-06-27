using System;
using System.Runtime.InteropServices;
using static HmExtension.Standard.WinApi;

namespace HmExtension.Standard.WindowApi;

public abstract class AbstractHook<T> : IDisposable where T: Enum
{
    private int hookId;

    public HookType HookType { get; private set; }

    private Action<int, IntPtr> hookProc;

    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
        public int x;
        public int y;
    }

    public int Hook(HookType hookType, Action<int, IntPtr> lpfn, IntPtr hInstance, int threadId)
    {
        HookType = hookType;
        hookProc = lpfn;
        hookId = SetWindowsHookEx(hookType, HookHandler, hInstance, threadId);
        return hookId;
    }

    private protected int HookHandler(int ncode, IntPtr wparam, IntPtr lparam)
    {
        if (ncode >= 0)
        {
            hookProc?.Invoke(wparam.ToInt32(), lparam);
        }
        return WinApi.CallNextHookEx(hookId, ncode, wparam, lparam);
    }

    public void UnHook()
    {
        WinApi.UnhookWindowsHookEx(hookId);
    }


    public void Dispose()
    {
        UnHook();
    }

    ~AbstractHook()
    {
        Dispose();
    }

    public abstract int Hook(T type=default);
}