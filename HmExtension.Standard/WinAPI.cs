using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace HmExtension.Standard;

/// <summary>
/// Windows API
/// </summary>
public class WinAPI
{
    /// <summary>
    /// 窗口消息
    /// </summary>
    public struct TagMSG
    {
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr hwnd;
        /// <summary>
        /// 消息
        /// </summary>
        public int message;
        /// <summary>
        /// 关于消息的附加信息。 确切含义取决于 消息 成员的值。
        /// </summary>
        public IntPtr wParam;
        /// <summary>
        /// 关于消息的附加信息。 确切含义取决于 消息 成员的值。
        /// </summary>
        public IntPtr lParam;
        /// <summary>
        /// 消息的发布时间。
        /// </summary>
        public int time;
        /// <summary>
        /// 发布消息时的光标位置（以屏幕坐标表示）
        /// </summary>
        public Point pt;
    }
    #region WinodwsAPI

    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

    private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
    /// <summary>
    /// 窗口消息
    /// </summary>
    /// <param name="lpMsg">指向 MSG 结构的指针，该结构从线程的消息队列接收消息信息。</param>
    /// <param name="hWnd">要检索其消息的窗口的句柄。 窗口必须属于当前线程。
    ///     如果 hWnd 为 NULL，GetMessage 将检索属于当前线程的任何窗口的消息，以及当前线程的消息队列中 hwnd 值为 NULL 的任何消息， () 看到 MSG 结构。 因此，如果 hWnd 为 NULL，则同时处理窗口消息和线程消息。
    ///     如果 hWnd 为 -1，则 GetMessage 仅检索当前线程的消息队列中 hwnd 值为 NULL 的消息，即当 hWnd 参数为 NULL) 或 PostThreadMessage 时，PostMessage (发布的线程消息。
    /// </param>
    /// <param name="wMsgFilterMin">要检索的最低消息值的整数值。 使用 WM_KEYFIRST (0x0100) 指定第一条键盘消息， 或使用WM_MOUSEFIRST (0x0200) 指定第一条鼠标消息。
    /// 
    /// 在此处和 wMsgFilterMax 中使用WM_INPUT仅指定WM_INPUT消息。
    /// 
    /// 如果 wMsgFilterMin 和 wMsgFilterMax 均为零， 则 GetMessage 将返回所有可用消息 (即不) 执行范围筛选</param>
    /// <param name="wMsgFilterMax">要检索的最高消息值的整数值。 使用 WM_KEYLAST 指定最后一条键盘消息， WM_MOUSELAST 指定最后一条鼠标消息。
    /// 
    /// 在此处和 wMsgFilterMin 中使用WM_INPUT，仅指定WM_INPUT消息。
    /// 
    /// 如果 wMsgFilterMin 和 wMsgFilterMax 均为零， 则 GetMessage 将返回所有可用消息 (即不) 执行范围筛选。
    /// </param>
    /// <returns>
    /// 如果函数检索 WM_QUIT以外的消息，则返回值为非零值。
    /// 如果函数检索 WM_QUIT 消息，则返回值为零。
    /// 如果出现错误，则返回值为 -1。 例如，如果 hWnd 是无效的窗口句柄或 lpMsg 是无效的指针，则该函数将失败。 要获得更多的错误信息，请调用 GetLastError。</returns>
    [DllImport("user32.dll", EntryPoint = "GetMessage")]
    public static extern bool GetMessage(out TagMSG lpMsg, IntPtr? hWnd, int wMsgFilterMin, int wMsgFilterMax);

    // [DllImport("kernel32.dll")]
    // public static extern IntPtr GetModuleHandle(string lpModuleName);
    //
    // [DllImport("user32.dll", SetLastError = true)]
    // static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);
    //
    // [DllImport("user32.dll")]
    // static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
    private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("user32.dll ", EntryPoint = "SendMessageA")]
    public static extern int SendMessageA(IntPtr hwnd, uint wMsg, int wParam, string lParam);

    [DllImport("user32.dll ", EntryPoint = "SendMessage")]
    public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

    [DllImport("user32.dll", EntryPoint = "GetParent")]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
    public static extern bool GetCursorPos(out Point pt);

    [DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern IntPtr WindowFromPoint(Point pt);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetWindowRect(IntPtr hwnd, ref Rectangle rc);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetClientRect(IntPtr hwnd, ref Rectangle rc);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
    public static extern int ScreenToClient(IntPtr hWnd, ref Rectangle rect);

    [DllImport("user32.dll")]
    public static extern bool EnumChildWindows(IntPtr hWndParent, ChildWindowsProc lpEnumFunc, int lParam);

    //==============================
    /// <summary>
    /// 激活窗口
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="fAltTab"></param>
    [DllImport("user32.dll")]
    public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

    /// <summary>
    /// 取进程ID
    /// </summary>
    /// <param name="hWndParent">窗口句柄</param>
    /// <param name="intPtr"> 进程id</param>
    /// <returns>拥有窗口的线程的标识符</returns>
    [DllImport("user32.dll")]
    public static extern int GetWindowThreadProcessId(IntPtr hWndParent, ref IntPtr lpdwProcessId);

    ///<summary>
    /// 该函数设置由不同线程产生的窗口的显示状态
    /// </summary>
    /// <param name="hWnd">窗口句柄</param>
    /// <param name="cmdShow">s</param>
    /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
    [DllImport("User32.dll")]
    public static extern bool ShowWindowAsync(IntPtr hWnd, ShowEnum cmdShow);

    /// <summary>
    ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
    ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
    /// </summary>
    /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
    /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
    [DllImport("User32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);


    [DllImport("user32.dll")]
    private static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
    [DllImport("user32.dll")]
    private static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);
    [DllImport("user32.dll")]
    private static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);

    /// <summary>
    /// 窗口置顶 或设置大小
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="hWndInsertAfter"></param>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    /// <param name="uFlags"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    #endregion



    #region 封装API方法

    private delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);

    /// <summary>
    /// 激活窗口 并显示到最前面
    /// </summary>
    /// <param name="hwnd"></param>
    public static void SetWindosActive(IntPtr hwnd)
    {
        ShowWindowAsync(hwnd, ShowEnum.SW_NORMAL);//显示
        SetForegroundWindow(hwnd);//当到最前端
    }
    /// <summary>
    /// 窗口置顶
    /// </summary>
    /// <param name="hWnd">窗口句柄</param>
    /// <param name="is_activ">是否置顶 为false 取消置顶</param>
    /// <returns></returns>
    public static int SetWindowsTop(IntPtr hWnd, bool is_activ)
    {
        int is_top = -1;
        if (!is_activ) is_top = -2;

        return SetWindowPos(hWnd, is_top, 0, 0, 0, 0, 1 | 2);
    }

    public delegate bool ChildWindowsProc(IntPtr hwnd, int lParam);





    /// <summary>
    /// 枚举窗口 返回窗口句柄 数组
    /// </summary>
    /// <param name="phwnd"></param>
    /// <returns></returns>
    public IntPtr[] GetAllChildControlsHandle(IntPtr phwnd)
    {
        List<IntPtr> child = new List<IntPtr>();
        EnumChildWindows(phwnd, delegate (IntPtr hWnd, int lParam)
        {
            child.Add(hWnd);
            return true;
        }, 0);
        return child.ToArray();
    }




    /// <summary>
    /// 根据标题查找窗体句柄
    /// </summary>
    /// <param name="title">标题内容</param>
    /// <returns></returns>
    public IntPtr FindWindow(string title)
    {
        Process[] ps = Process.GetProcesses();
        foreach (Process p in ps)
        {
            if (p.MainWindowTitle.IndexOf(title) != -1)
            {
                return p.MainWindowHandle;
            }
        }
        return IntPtr.Zero;
    }
    /// <summary>
    /// 查找句柄
    /// </summary>
    /// <param name="IpClassName">类名</param>
    /// <returns></returns>
    public static IntPtr GetHandle(string IpClassName)
    {
        return FindWindow(IpClassName, null);
    }

    /// <summary>
    /// 找到句柄
    /// </summary>
    /// <param name="p">坐标</param>
    /// <returns></returns>
    public static IntPtr GetHandle(Point p)
    {
        return WindowFromPoint(p);
    }

    /// <summary>
    /// 获取鼠标位置的坐标
    /// </summary>
    /// <returns></returns>
    public static Point GetCursorPosPoint()
    {
        Point p = new Point();
        if (GetCursorPos(out p))
        {
            return p;
        }
        return default(Point);
    }

    /// <summary>
    /// 得到鼠标指向的窗口句柄
    /// </summary>
    /// <returns>找不到则返回-1</returns>
    public static int GetMoustPointWindwsHwnd()
    {
        try
        {
            return (int)GetHandle(GetCursorPosPoint());
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return -1;
    }


    /// <summary>
    /// 查找子窗口句柄
    /// </summary>
    /// <param name="hwndParent">父窗口句柄</param>
    /// <param name="hwndChildAfter">前一个同目录级同名窗口句柄</param>
    /// <param name="lpszClass">类名</param>
    /// <returns></returns>
    public static IntPtr FindWindowE(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass)
    {
        return FindWindowEx(hwndParent, hwndChildAfter, lpszClass, null);
    }

    /// <summary>
    /// 查找全部子窗口句柄
    /// </summary>
    /// <param name="hwndParent">父窗口句柄</param>
    /// <param name="className">类名</param>
    /// <returns></returns>
    public static List<IntPtr> FindWindowExList(IntPtr hwndParent, string className)
    {
        List<IntPtr> resultList = new List<IntPtr>();
        for (IntPtr hwndClient = FindWindowE(hwndParent, IntPtr.Zero, className); hwndClient != IntPtr.Zero; hwndClient = FindWindowE(hwndParent, hwndClient, className))
        {
            resultList.Add(hwndClient);
        }

        return resultList;
    }


    public static void InputStr(IntPtr myIntPtr, string Input)
    {
        byte[] ch = (ASCIIEncoding.ASCII.GetBytes(Input));
        for (int i = 0; i < ch.Length; i++)
        {
            SendMessageA(myIntPtr, (uint)0X102, int.Parse(ch[i].ToString()), "0");
        }
    }
    /// <summary>
    /// 给窗口发送文本内容
    /// </summary>
    /// <param name="hWnd">句柄</param>
    /// <param name="lParam">要发送的内容</param>
    public static void SendMessageA(IntPtr hWnd, string lParam)
    {

        SendMessageA(hWnd, WindowsMessage.WM_SETTEXT, 0, lParam);
    }


    /// <summary>
    /// 鼠标点击
    /// </summary>
    /// <param name="hWnd">句柄</param>
    /// <param name="lParam">要发送的内容</param>
    public static void MouseClick(IntPtr hWnd, int _x, int _y)
    {
        SendMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, 0);
    }


    /// <summary>
    /// 获得窗口内容或标题
    /// </summary>
    /// <param name="hWnd">句柄</param>
    /// <returns></returns>
    public static string GetWindowText(IntPtr hWnd)
    {
        StringBuilder result = new StringBuilder(128);
        GetWindowText(hWnd, result, result.Capacity);
        return result.ToString();
    }


    /// <summary>
    /// 窗口在屏幕位置
    /// </summary>
    /// <param name="hWnd">句柄</param>
    /// <returns></returns>
    public static Rectangle GetWindowRect(IntPtr hWnd)
    {
        Rectangle result = default(Rectangle);
        GetWindowRect(hWnd, ref result);
        result.Width = result.Width - result.X;
        result.Height = result.Height - result.Y;

        return result;
    }

    /// <summary>
    /// 窗口相对屏幕位置转换成父窗口位置
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="rect"></param>
    /// <returns></returns>
    public static Rectangle ScreenToClient(IntPtr hWnd, Rectangle rect)
    {
        Rectangle result = rect;
        ScreenToClient(hWnd, ref result);
        return result;
    }

    /// <summary>
    /// 窗口大小
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    public static Rectangle GetClientRect(IntPtr hWnd)
    {
        Rectangle result = default(Rectangle);
        GetClientRect(hWnd, ref result);
        return result;
    }


    ///=====================================================================  
    //判断窗口是否存在
    [DllImport("user32", EntryPoint = "IsWindow")]
    private static extern bool IsWindow(IntPtr hWnd);

    /// <summary>
    /// 判断窗口是否存在
    /// </summary>
    /// <param name="Hwnd">窗口句柄</param>
    /// <returns>存在返回 true 不存在返回 false</returns>
    public static bool IsWindow(int Hwnd)
    {
        if (IsWindow((IntPtr)Hwnd))
        {
            return true;
        }
        return false;
    }


    //修改指定窗口标题
    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern int SetWindowText(IntPtr hWnd, string text);

    /// <summary>
    /// 设置窗口标题
    /// </summary>
    /// <param name="Hwnd">窗口句柄</param>
    /// <param name="newtext">新标题</param>
    public static void SetWindowText(int Hwnd, string newtext)
    {
        SetWindowText((IntPtr)Hwnd, newtext);
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern bool IsZoomed(IntPtr hWnd);

    //获得顶层窗口
    [DllImport("user32", EntryPoint = "GetForegroundWindow")]
    private static extern IntPtr GetForegroundwindow();

    /// <summary>
    /// 获得顶层窗口
    /// </summary>
    /// <returns>返回 窗口句柄</returns>
    public static int GetForeGroundWindow()
    {
        return (int)GetForegroundwindow();
    }

    /// <summary>
    /// 得到窗口状态
    /// </summary>
    /// <param name="Hwnd">窗口句柄</param>
    /// <param name="flag">
    /// 操作方式
    /// 1：判断窗口是否最小化
    /// 2：判断窗口是否最大化
    /// 3：判断窗口是否激活
    /// </param>
    /// <returns>满足条件返回 true</returns>
    public static bool GetWindowState(int Hwnd, int flag)
    {
        switch (flag)
        {
            case 1:
                return IsIconic((IntPtr)Hwnd);
            case 2:
                return IsZoomed((IntPtr)Hwnd);
            case 3:
                if (Hwnd != GetForeGroundWindow())
                {
                    return false;
                }
                break;
        }
        return true;

    }




    /// <summary>
    /// 得到窗口上一级窗口的句柄
    /// </summary>
    /// <param name="ChildHwnd">子窗口句柄</param>
    /// <returns> 返回 窗口句柄 找不到返回 0</returns>
    public static int GetChild_Host(int ChildHwnd)
    {
        return (int)GetParent((IntPtr)ChildHwnd);
    }



    /// </summary>
    /// 得到指定窗口类名
    /// <summary>
    /// <param name="hWnd">句柄</param>
    /// <returns>找不到返回""</returns>
    public static string GetClassName(int hWnd)
    {
        return GetClassName((IntPtr)hWnd);
    }

    /// </summary>
    /// 得到指定窗口类名
    /// <summary>
    /// <param name="hWnd">句柄</param>
    /// <returns>找不到返回""</returns>
    public static string GetClassName(IntPtr hWnd)
    {
        StringBuilder lpClassName = new StringBuilder(128);
        if (GetClassName(hWnd, lpClassName, lpClassName.Capacity) == 0)
        {
            return "";
        }
        return lpClassName.ToString();
    }


    #endregion



    /// <summary>
    /// 得到指定坐标的窗口句柄
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <returns>找不到返回 0</returns>
    public static int FindPointWindow(int x, int y)
    {
        Point p = new Point(x, y);
        IntPtr formHandle = WindowFromPoint(p);//得到窗口句柄
        return (int)formHandle;
    }

    /// </summary>
    /// 根据标题和类名找句柄
    /// <summary>
    /// <param name="IpClassName">窗口类名 如果为"" 则只根据标题查找</param>
    /// <param name="IpClassName">窗口标题 如果为"" 则只根据类名查找</param>
    /// <returns>找不到则返回0</returns>
    public static int FindWindowHwnd(string IpClassName, string IpTitleName)
    {
        if (IpTitleName == "" && IpClassName != "")
        {
            return (int)FindWindow(IpClassName, null);
        }
        else if (IpClassName == "" && IpTitleName != "")
        {
            return (int)FindWindow(null, IpTitleName);
        }
        else if (IpClassName != "" && IpTitleName != "")
        {
            return (int)FindWindow(IpClassName, IpTitleName);
        }
        return 0;
    }




    public struct WindowInfo
    {
        public IntPtr hWnd;
        public string szWindowName;
        public string szClassName;
    }
    /// <summary>
    /// 遍历方法 - 返回一个List<WindowInfo> 集合
    /// </summary>
    /// <returns></returns>
    public List<WindowInfo> GetAllDesktopWindows()
    {
        List<WindowInfo> wndList = new List<WindowInfo>();

        //enum all desktop windows
        EnumWindows(delegate (IntPtr hWnd, int lParam)
        {
            WindowInfo wnd = new WindowInfo();
            StringBuilder sb = new StringBuilder(256);
            wnd.hWnd = hWnd;
            GetWindowTextW(hWnd, sb, sb.Capacity);
            wnd.szWindowName = sb.ToString();
            GetClassNameW(hWnd, sb, sb.Capacity);
            wnd.szClassName = sb.ToString();
            wndList.Add(wnd);
            return true;
        }, 0);

        return wndList;
    }
    /// <summary>
    /// 塞选方法 - 返回满足条件的句柄集合
    /// </summary>
    /// <param name="textName">窗口标题 - 为空不匹配标题( 模糊匹配，不区分大小写)</param>
    /// <param name="textClass">窗口类名 - 为空不匹配类名( 模糊匹配，不区分大小写)</param>
    /// <returns></returns>
    public List<int> EnumWindow(string textName, string textClass)
    {
        CompareInfo Compare = CultureInfo.InvariantCulture.CompareInfo;
        List<int> gethwnd = new List<int>();
        //先取得所有句柄
        List<WindowInfo> Listwindows = GetAllDesktopWindows();
        //
        foreach (WindowInfo wdf in Listwindows)
        {
            if (textName != "" && textClass != "")
            {
                if (Compare.IndexOf(wdf.szClassName.ToUpper(), textClass.ToUpper()) > -1 && Compare.IndexOf(wdf.szWindowName.ToUpper(), textName.ToUpper()) > -1)
                {
                    gethwnd.Add((int)wdf.hWnd);
                }
            }
            else if (textName == "" && textClass == "")
            {
                gethwnd.Add((int)wdf.hWnd);
                //都返回
            }
            else if (textName == "")
            {
                //只匹配类名
                if (Compare.IndexOf(wdf.szClassName.ToUpper(), textClass.ToUpper()) > -1)
                {
                    gethwnd.Add((int)wdf.hWnd);
                }
            }
            else if (textClass == "")
            {
                //只匹配标题
                if (Compare.IndexOf(wdf.szWindowName.ToUpper(), textName.ToUpper()) > -1)
                {
                    gethwnd.Add((int)wdf.hWnd);
                }
            }
        }
        return gethwnd;
    }

    /// <summary>
    /// 根据窗口标题模糊查找窗口句柄
    /// </summary>
    /// <param name="title">窗口标题</param>
    /// <returns>返回 窗口句柄 找不到返回 0</returns>
    public int FindWindow_ByTitle(string title)
    {
        //按照窗口标题来寻找窗口句柄
        Process[] ps = Process.GetProcesses();
        string WindowHwnd = "";
        foreach (Process p in ps)
        {
            if (p.MainWindowTitle.IndexOf(title) != -1)
            {
                WindowHwnd = p.MainWindowHandle.ToString();
                return int.Parse(WindowHwnd);
            }
        }
        return 0;
    }



    /// <summary>
    /// 根据窗口标题模糊查找符合条件的所有窗口句柄
    /// </summary>
    /// <param name="title">窗口标题关键字</param>
    /// <returns>返回 窗口句柄 多个句柄以"|" 隔开，找不到返回""</returns>
    public string FindWindowEx_ByTitle(string title)
    {
        //按照窗口标题来寻找窗口句柄
        Process[] ps = Process.GetProcesses();
        string WindowHwnd = "";
        foreach (Process p in ps)
        {
            if (p.MainWindowTitle.IndexOf(title) != -1)
            {
                if (WindowHwnd == "")
                {
                    WindowHwnd = p.MainWindowHandle.ToString();
                }
                else
                {
                    WindowHwnd = WindowHwnd + "|" + p.MainWindowHandle.ToString();
                }
            }
        }
        if (WindowHwnd == "")
        {
            return "";
        }
        return WindowHwnd;
    }


    /// <summary>
    /// 根据进程名获得窗口句柄 - 不需要带上进程后缀
    /// </summary>
    /// <param name="ProssName">进程名</param>
    /// <returns>窗口句柄 找不到返回 0</returns>
    private int FindWindow_ByProcessName(string ProssName)
    {
        Process[] pp = Process.GetProcessesByName(ProssName);
        for (int i = 0; i < pp.Length; i++)
        {
            if (pp[i].ProcessName == ProssName)
            {
                return (int)pp[i].MainWindowHandle;
            }
        }
        return 0;
    }



    /// <summary>
    /// 根据进程名获得窗口句柄 - 不需要带上进程后缀
    /// </summary>
    /// <param name="ProssName">进程名</param>
    /// <returns>窗口句柄 多个用"|"隔开 找不到返回 ""</returns>
    private string FindWindowEx_ByProcessName(string ProssName)
    {
        string Hwnd = "";
        Process[] pp = Process.GetProcessesByName(ProssName);
        for (int i = 0; i < pp.Length; i++)
        {
            if (pp[i].ProcessName == ProssName)
            {
                if (Hwnd == "")
                {
                    Hwnd = pp[i].MainWindowHandle.ToString();
                }
                else
                {
                    Hwnd = Hwnd + "|" + pp[i].MainWindowHandle.ToString();
                }
            }
        }
        return Hwnd;
    }






    /// <summary>
    /// 不改变尺寸移动窗口到指定位置
    /// </summary>
    /// <param name="Hwnd">窗口句柄</param>
    /// <param name="X">目的地左上角X</param>
    /// <param name="Y">目的地左上角Y</param>
    /// <returns>移动成功返回 true</returns>
    public bool MoveWindow(int Hwnd, int X, int Y)
    {
        Rectangle rect = new Rectangle();
        GetWindowRect((IntPtr)Hwnd, ref rect);
        return MoveWindow((IntPtr)Hwnd, rect.Left, rect.Top, rect.Right, rect.Bottom, true);
    }

    /// <summary>
    /// 改变尺寸移动窗口到指定位置
    /// </summary>
    /// <param name="Hwnd">窗口句柄</param>
    /// <param name="X">目的地左上角X</param>
    /// <param name="Y">目的地左上角Y</param>
    /// <param name="Width">新宽度</param>
    /// <param name="Height">新高度</param>
    /// <returns>移动成功返回 true</returns>
    public bool MoveWindow(int Hwnd, int X, int Y, int Width, int Height)
    {
        return MoveWindow((IntPtr)Hwnd, X, Y, Width, Height, true);
    }









}//end class






public enum ShowEnum
{
    SW_Close = 0,
    SW_NORMAL = 1,
    SW_MINIMIZE = 2,
    SW_MAXIMIZE = 3,
    SW_SHOWNOACTIVATE = 4,
    SW_SHOW = 5,
    SW_RESTORE = 9,//还原
    SW_SHOWDEFAULT = 10
}

public class WindowsMessage
{
    public const int WM_NULL = 0x0000; // 
    public const int WM_CREATE = 0x0001; //应用程序创建一个窗口 
    public const int WM_DESTROY = 0x0002; //一个窗口被销毁 
    public const int WM_MOVE = 0x0003; //移动一个窗口 
    public const int WM_SIZE = 0x0005; //改变一个窗口的大小 
    public const int WM_ACTIVATE = 0x0006; //一个窗口被激活或失去激活状态； 
    public const int WM_SETFOCUS = 0x0007; //获得焦点后 
    public const int WM_KILLFOCUS = 0x0008; //失去焦点 
    public const int WM_ENABLE = 0x000A; //改变enable状态 
    public const int WM_SETREDRAW = 0x000B; //设置窗口是否能重画 
    public const int WM_SETTEXT = 0x000C; //应用程序发送此消息来设置一个窗口的文本 
    public const int WM_GETTEXT = 0x000D; //应用程序发送此消息来复制对应窗口的文本到缓冲区 
    public const int WM_GETTEXTLENGTH = 0x000E; //得到与一个窗口有关的文本的长度（不包含空字符） 
    public const int WM_PAINT = 0x000F; //要求一个窗口重画自己 
    public const int WM_CLOSE = 0x0010; //当一个窗口或应用程序要关闭时发送一个信号 
    public const int WM_QUERYENDSESSION = 0x0011; //当用户选择结束对话框或程序自己调用ExitWindows函数 
    public const int WM_QUIT = 0x0012; //用来结束程序运行或当程序调用postquitmessage函数 
    public const int WM_QUERYOPEN = 0x0013; //当用户窗口恢复以前的大小位置时，把此消息发送给某个图标 
    public const int WM_ERASEBKGND = 0x0014; //当窗口背景必须被擦除时（例在窗口改变大小时） 
    public const int WM_SYSCOLORCHANGE = 0x0015; //当系统颜色改变时，发送此消息给所有顶级窗口 
    public const int WM_ENDSESSION = 0x0016; // 当系统进程发出WM_QUERYENDSESSION消息后，此消息发送给应用程序，通知它对话是否结束 
    public const int WM_SYSTEMERROR = 0x0017; // 
    public const int WM_SHOWWINDOW = 0x0018; //当隐藏或显示窗口是发送此消息给这个窗口 
    public const int WM_ACTIVATEAPP = 0x001C; //发此消息给应用程序哪个窗口是激活的，哪个是非激活的； 
    public const int WM_FONTCHANGE = 0x001D; //当系统的字体资源库变化时发送此消息给所有顶级窗口 
    public const int WM_TIMECHANGE = 0x001E; //当系统的时间变化时发送此消息给所有顶级窗口 
    public const int WM_CANCELMODE = 0x001F; //发送此消息来取消某种正在进行的摸态（操作） 
    public const int WM_SETCURSOR = 0x0020; //如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口
    public const int WM_MOUSEACTIVATE = 0x0021; //当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口 
    public const int WM_CHILDACTIVATE = 0x0022; //发送此消息给MDI子窗口当用户点击此窗口的标题栏，或当窗口被激活，移动，改变大小 
    public const int WM_QUEUESYNC = 0x0023; //此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息 
    public const int WM_GETMINMAXINFO = 0x0024; //此消息发送给窗口当它将要改变大小或位置； 
    public const int WM_PAINTICON = 0x0026; //发送给最小化窗口当它图标将要被重画 
    public const int WM_ICONERASEBKGND = 0x0027; //此消息发送给某个最小化窗口，仅当它在画图标前它的背景必须被重画 
    public const int WM_NEXTDLGCTL = 0x0028; //发送此消息给一个对话框程序去更改焦点位置 
    public const int WM_SPOOLERSTATUS = 0x002A; //每当打印管理列队增加或减少一条作业时发出此消息 
    public const int WM_DRAWITEM = 0x002B; //当button，combobox，listbox，menu的可视外观改变时发送此消息给这些空件的所有者 
    public const int WM_MEASUREITEM = 0x002C; //当button, combo box, list box, list view control, or menu item 被创建时发送此消息给控件的所有者 
    public const int WM_DELETEITEM = 0x002D; // 当the list box 或combo box 被销毁或当某些项被删除通过LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT 消息 
    public const int WM_VKEYTOITEM = 0x002E; //此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息 
    public const int WM_CHARTOITEM = 0x002F; //此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息 
    public const int WM_SETFONT = 0x0030; //当绘制文本时程序发送此消息得到控件要用的颜色 
    public const int WM_GETFONT = 0x0031; //应用程序发送此消息得到当前控件绘制文本的字体 
    public const int WM_SETHOTKEY = 0x0032; //应用程序发送此消息让一个窗口与一个热键相关连 
    public const int WM_GETHOTKEY = 0x0033; //应用程序发送此消息来判断热键与某个窗口是否有关联 
    public const int WM_QUERYDRAGICON = 0x0037; //此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序能 
                                                //返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标 
    public const int WM_COMPAREITEM = 0x0039; //发送此消息来判定combobox或listbox新增加的项的相对位置 
    public const int WM_GETOBJECT = 0x003D; //WM_COMPACTING = 0x0041; //显示内存已经很少了 
    public const int WM_WINDOWPOSCHANGING = 0x0046; //发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数 
    public const int WM_WINDOWPOSCHANGED = 0x0047; //发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数 
    public const int WM_POWER = 0x0048; //（适用于16位的windows）当系统将要进入暂停状态时发送此消息 
    public const int WM_COPYDATA = 0x004A; //当一个应用程序传递数据给另一个应用程序时发送此消息 
    public const int WM_CANCELJOURNAL = 0x004B; //当某个用户取消程序日志激活状态，提交此消息给程序 
    public const int WM_NOTIFY = 0x004E; //当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口 
    public const int WM_INPUTLANGCHANGEREQUEST = 0x0050; //当用户选择某种输入语言，或输入语言的热键改变 
    public const int WM_INPUTLANGCHANGE = 0x0051; //当平台现场已经被改变后发送此消息给受影响的最顶级窗口 
    public const int WM_TCARD = 0x0052; //当程序已经初始化windows帮助例程时发送此消息给应用程序 
    public const int WM_HELP = 0x0053; //此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单，否则就 
                                       //发送给有焦点的窗口，如果当前都没有焦点，就把此消息发送给当前激活的窗口 
    public const int WM_USERCHANGED = 0x0054; //当用户已经登入或退出后发送此消息给所有的窗口，当用户登入或退出时系统更新用户的具体 
                                              //设置信息，在用户更新设置时系统马上发送此消息； 
    public const int WM_NOTIFYformAT = 0x0055; //公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构 
                                               //在WM_NOTIFY消息，使用此控件能使某个控件与它的父控件之间进行相互通信 
    public const int WM_CONTEXTMENU = 0x007B; //当用户某个窗口中点击了一下右键就发送此消息给这个窗口 
    public const int WM_styleCHANGING = 0x007C; //当调用SETWINDOWLONG函数将要改变一个或多个窗口的风格时发送此消息给那个窗口 
    public const int WM_styleCHANGED = 0x007D; //当调用SETWINDOWLONG函数一个或多个窗口的风格后发送此消息给那个窗口 
    public const int WM_DISPLAYCHANGE = 0x007E; //当显示器的分辨率改变后发送此消息给所有的窗口 
    public const int WM_GETICON = 0x007F; //此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄； 
    public const int WM_SETICON = 0x0080; //程序发送此消息让一个新的大图标或小图标与某个窗口关联； 
    public const int WM_NCCREATE = 0x0081; //当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送； 
    public const int WM_NCDESTROY = 0x0082; //此消息通知某个窗口，非客户区正在销毁 
    public const int WM_NCCALCSIZE = 0x0083; //当某个窗口的客户区域必须被核算时发送此消息 
    public const int WM_NCHITTEST = 0x0084; //移动鼠标，按住或释放鼠标时发生 
    public const int WM_NCPAINT = 0x0085; //程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时； 
    public const int WM_NCACTIVATE = 0x0086; //此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态； 
    public const int WM_GETDLGCODE = 0x0087; //发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件 
                                             //通过响应WM_GETDLGCODE消息，应用程序可以把他当成一个特殊的输入控件并能处理它 
    public const int WM_NCMOUSEMOVE = 0x00A0; //当光标在一个窗口的非客户区内移动时发送此消息给这个窗口//非客户区为：窗体的标题栏及窗的边框体 
    public const int WM_NCLBUTTONDOWN = 0x00A1; // 
                                                //当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息 
    public const int WM_NCLBUTTONUP = 0x00A2; //当用户释放鼠标左键同时光标某个窗口在非客户区十发送此消息； 
    public const int WM_NCLBUTTONDBLCLK = 0x00A3; //当用户双击鼠标左键同时光标某个窗口在非客户区十发送此消息 
    public const int WM_NCRBUTTONDOWN = 0x00A4; //当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息 
    public const int WM_NCRBUTTONUP = 0x00A5; //当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息 
    public const int WM_NCRBUTTONDBLCLK = 0x00A6; //当用户双击鼠标右键同时光标某个窗口在非客户区十发送此消息 
    public const int WM_NCMBUTTONDOWN = 0x00A7; //当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息 
    public const int WM_NCMBUTTONUP = 0x00A8; //当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息 
    public const int WM_NCMBUTTONDBLCLK = 0x00A9; //当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息 
    public const int WM_KEYFIRST = 0x0100; // 
    public const int WM_KEYDOWN = 0x0100; //按下一个键 
    public const int WM_KEYUP = 0x0101; //释放一个键 
    public const int WM_CHAR = 0x0102; //按下某键，并已发出WM_KEYDOWN，WM_KEYUP消息 
    public const int WM_DEADCHAR = 0x0103; //当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口 
    public const int WM_SYSKEYDOWN = 0x0104; //当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口； 
    public const int WM_SYSKEYUP = 0x0105; //当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口 
    public const int WM_SYSCHAR = 0x0106; //当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口 
    public const int WM_SYSDEADCHAR = 0x0107; //当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口 
    public const int WM_KEYLAST = 0x0108; // 
    public const int WM_INITDIALOG = 0x0110; //在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务 
    public const int WM_COMMAND = 0x0111; //当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译 
    public const int WM_SYSCOMMAND = 0x0112; //当用户选择窗口菜单的一条命令或当用户选择最大化或最小化时那个窗口会收到此消息 
    public const int WM_TIMER = 0x0113; //发生了定时器事件 
    public const int WM_HSCROLL = 0x0114; //当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件 
    public const int WM_VSCROLL = 0x0115; //当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件WM_INITMENU = 0x0116; // 
                                          //当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许 
                                          //程序在显示前更改菜单 
    public const int WM_INITMENUPOPUP = 0x0117; //当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要 
                                                // 改变全部 
    public const int WM_MENUSELECT = 0x011F; //当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口） 
    public const int WM_MENUCHAR = 0x0120; //当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者； 
    public const int WM_ENTERIDLE = 0x0121; //当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理完一条或几条先前的消息后没有消息它的列队中等待 
    public const int WM_MENURBUTTONUP = 0x0122; // 
    public const int WM_MENUDRAG = 0x0123; // 
    public const int WM_MENUGETOBJECT = 0x0124; // 
    public const int WM_UNINITMENUPOPUP = 0x0125; // 
    public const int WM_MENUCOMMAND = 0x0126; // 
    public const int WM_CHANGEUISTATE = 0x0127; // 
    public const int WM_UPDATEUISTATE = 0x0128; // 
    public const int WM_QUERYUISTATE = 0x0129; // 
    public const int WM_CTLCOLORMSGBOX = 0x0132; //在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以 
                                                 //通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色 
    public const int WM_CTLCOLOREDIT = 0x0133; //当一个编辑型控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
                                               //通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色 
    public const int WM_CTLCOLORLISTBOX = 0x0134; //当一个列表框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
                                                  //通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色 
    public const int WM_CTLCOLORBTN = 0x0135; //当一个按钮控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
                                              //通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色 
    public const int WM_CTLCOLORDLG = 0x0136; //当一个对话框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
                                              //通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色 
    public const int WM_CTLCOLORSCROLLBAR = 0x0137; //当一个滚动条控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
                                                    //通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色 
    public const int WM_CTLCOLORSTATIC = 0x0138; //当一个静态控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
                                                 //通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色 
    public const int WM_MOUSEFIRST = 0x0200; // 
    public const int WM_MOUSEMOVE = 0x0200; //移动鼠标 
    public const int WM_LBUTTONDOWN = 0x0201; //按下鼠标左键 
    public const int WM_LBUTTONUP = 0x0202; //释放鼠标左键 
    public const int WM_LBUTTONDBLCLK = 0x0203; //双击鼠标左键 
    public const int WM_RBUTTONDOWN = 0x0204; //按下鼠标右键 
    public const int WM_RBUTTONUP = 0x0205; //释放鼠标右键 
    public const int WM_RBUTTONDBLCLK = 0x0206; //双击鼠标右键 
    public const int WM_MBUTTONDOWN = 0x0207; //按下鼠标中键 
    public const int WM_MBUTTONUP = 0x0208; //释放鼠标中键 
    public const int WM_MBUTTONDBLCLK = 0x0209; //双击鼠标中键 
    public const int WM_MOUSEWHEEL = 0x020A; //当鼠标轮子转动时发送此消息个当前有焦点的控件 
    public const int WM_MOUSELAST = 0x020A; // 
    public const int WM_PARENTNOTIFY = 0x0210; //当MDI子窗口被创建或被销毁，或用户按了一下鼠标键而光标在子窗口上时发送此消息给它的父窗口 
    public const int WM_ENTERMENULOOP = 0x0211; //发送此消息通知应用程序的主窗口that已经进入了菜单循环模式 
    public const int WM_EXITMENULOOP = 0x0212; //发送此消息通知应用程序的主窗口that已退出了菜单循环模式 
    public const int WM_NEXTMENU = 0x0213; // 
    public const int WM_SIZING = 532; //当用户正在调整窗口大小时发送此消息给窗口；通过此消息应用程序可以监视窗口大小和位置
                                      //也可以修改他们 
    public const int WM_CAPTURECHANGED = 533; //发送此消息给窗口当它失去捕获的鼠标时； 
    public const int WM_MOVING = 534; //当用户在移动窗口时发送此消息，通过此消息应用程序可以监视窗口大小和位置 
                                      //也可以修改他们； 
    public const int WM_POWERBROADCAST = 536; //此消息发送给应用程序来通知它有关电源管理事件； 
    public const int WM_DEVICECHANGE = 537; //当设备的硬件配置改变时发送此消息给应用程序或设备驱动程序 
    public const int WM_IME_STARTCOMPOSITION = 0x010D; // 
    public const int WM_IME_ENDCOMPOSITION = 0x010E; // 
    public const int WM_IME_COMPOSITION = 0x010F; // 
    public const int WM_IME_KEYLAST = 0x010F; // 
    public const int WM_IME_SETCONTEXT = 0x0281; // 
    public const int WM_IME_NOTIFY = 0x0282; // 
    public const int WM_IME_CONTROL = 0x0283; // 
    public const int WM_IME_COMPOSITIONFULL = 0x0284; // 
    public const int WM_IME_SELECT = 0x0285; // 
    public const int WM_IME_CHAR = 0x0286; // 
    public const int WM_IME_REQUEST = 0x0288; // 
    public const int WM_IME_KEYDOWN = 0x0290; // 
    public const int WM_IME_KEYUP = 0x0291; // 
    public const int WM_MDICREATE = 0x0220; //应用程序发送此消息给多文档的客户窗口来创建一个MDI 子窗口 
    public const int WM_MDIDESTROY = 0x0221; //应用程序发送此消息给多文档的客户窗口来关闭一个MDI 子窗口 
    public const int WM_MDIACTIVATE = 0x0222; //应用程序发送此消息给多文档的客户窗口通知客户窗口激活另一个MDI子窗口，当客户窗口收到 
                                              //此消息后，它发出WM_MDIACTIVE消息给MDI子窗口（未激活）激活它； 
    public const int WM_MDIRESTORE = 0x0223; //程序发送此消息给MDI客户窗口让子窗口从最大最小化恢复到原来大小 
    public const int WM_MDINEXT = 0x0224; //程序发送此消息给MDI客户窗口激活下一个或前一个窗口 
    public const int WM_MDIMAXIMIZE = 0x0225; //程序发送此消息给MDI客户窗口来最大化一个MDI子窗口； 
    public const int WM_MDITILE = 0x0226; //程序发送此消息给MDI客户窗口以平铺方式重新排列所有MDI子窗口 
    public const int WM_MDICASCADE = 0x0227; //程序发送此消息给MDI客户窗口以层叠方式重新排列所有MDI子窗口 
    public const int WM_MDIICONARRANGE = 0x0228; //程序发送此消息给MDI客户窗口重新排列所有最小化的MDI子窗口 
    public const int WM_MDIGETACTIVE = 0x0229; //程序发送此消息给MDI客户窗口来找到激活的子窗口的句柄 
    public const int WM_MDISETMENU = 0x0230; //程序发送此消息给MDI客户窗口用MDI菜单代替子窗口的菜单 
    public const int WM_ENTERSIZEMOVE = 0x0231; // 
    public const int WM_EXITSIZEMOVE = 0x0232; // 
    public const int WM_DROPFILES = 0x0233; // 
    public const int WM_MDIREFRESHMENU = 0x0234; // 
    public const int WM_MOUSEHOVER = 0x02A1; // 
    public const int WM_MOUSELEAVE = 0x02A3; // 
    public const int WM_CUT = 0x0300; //程序发送此消息给一个编辑框或combobox来删除当前选择的文本 
    public const int WM_COPY = 0x0301; //程序发送此消息给一个编辑框或combobox来复制当前选择的文本到剪贴板 
    public const int WM_PASTE = 0x0302; //程序发送此消息给editcontrol或combobox从剪贴板中得到数据 
    public const int WM_CLEAR = 0x0303; //程序发送此消息给editcontrol或combobox清除当前选择的内容； 
    public const int WM_UNDO = 0x0304; //程序发送此消息给editcontrol或combobox撤消最后一次操作 
    public const int WM_RENDERformAT = 0x0305; // 
    public const int WM_RENDERALLformATS = 0x0306; // 
    public const int WM_DESTROYCLIPBOARD = 0x0307; //当调用ENPTYCLIPBOARD函数时发送此消息给剪贴板的所有者 
    public const int WM_DRAWCLIPBOARD = 0x0308; //当剪贴板的内容变化时发送此消息给剪贴板观察链的第一个窗口；它允许用剪贴板观察窗口来 
                                                //显示剪贴板的新内容； 
    public const int WM_PAINTCLIPBOARD = 0x0309; //当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区需要重画； 
    public const int WM_VSCROLLCLIPBOARD = 0x030A; // 
    public const int WM_SIZECLIPBOARD = 0x030B; //当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区域的大小已经改变是此消息通过剪贴板观察窗口发送给剪贴板的所有者； 
    public const int WM_ASKCBformATNAME = 0x030C; //通过剪贴板观察窗口发送此消息给剪贴板的所有者来请求一个CF_OWNERDISPLAY格式的剪贴板的名字 
    public const int WM_CHANGECBCHAIN = 0x030D; //当一个窗口从剪贴板观察链中移去时发送此消息给剪贴板观察链的第一个窗口； 
    public const int WM_HSCROLLCLIPBOARD = 0x030E; // 
                                                   //此消息通过一个剪贴板观察窗口发送给剪贴板的所有者；它发生在当剪贴板包含CFOWNERDISPALY格式的数据并且有个事件在剪贴板观察窗的水平滚动条上；所有者应滚动剪贴板图象并更新滚动条的值； 
    public const int WM_QUERYNEWPALETTE = 0x030F; //此消息发送给将要收到焦点的窗口，此消息能使窗口在收到焦点时同时有机会实现他的逻辑调色板 
    public const int WM_PALETTEISCHANGING = 0x0310; //当一个应用程序正要实现它的逻辑调色板时发此消息通知所有的应用程序 
    public const int WM_PALETTECHANGED = 0x0311; //此消息在一个拥有焦点的窗口实现它的逻辑调色板后发送此消息给所有顶级并重叠的窗口，以此 
                                                 //来改变系统调色板 
    public const int WM_HOTKEY = 0x0312; //当用户按下由REGISTERHOTKEY函数注册的热键时提交此消息 
    public const int WM_PRINT = 791; //应用程序发送此消息仅当WINDOWS或其它应用程序发出一个请求要求绘制一个应用程序的一部分； 
    public const int WM_PRINTCLIENT = 792; // 
    public const int WM_HANDHELDFIRST = 856; // 
    public const int WM_HANDHELDLAST = 863; // 
    public const int WM_PENWINFIRST = 0x0380; // 
    public const int WM_PENWINLAST = 0x038F; // 
    public const int WM_COALESCE_FIRST = 0x0390; // 
    public const int WM_COALESCE_LAST = 0x039F; // 
    public const int WM_DDE_FIRST = 0x03E0; // 
    public const int WM_DDE_INITIATE = WM_DDE_FIRST + 0; //一个DDE客户程序提交此消息开始一个与服务器程序的会话来响应那个指定的程序和主题名； 
    public const int WM_DDE_TERMINATE = WM_DDE_FIRST + 1; //一个DDE应用程序（无论是客户还是服务器）提交此消息来终止一个会话； 
    public const int WM_DDE_ADVISE = WM_DDE_FIRST + 2; //一个DDE客户程序提交此消息给一个DDE服务程序来请求服务器每当数据项改变时更新它 
    public const int WM_DDE_UNADVISE = WM_DDE_FIRST + 3; //一个DDE客户程序通过此消息通知一个DDE服务程序不更新指定的项或一个特殊的剪贴板格式的项 
    public const int WM_DDE_ACK = WM_DDE_FIRST + 4; //此消息通知一个DDE（动态数据交换）程序已收到并正在处理WM_DDE_POKE, WM_DDE_EXECUTE, WM_DDE_DATA, WM_DDE_ADVISE, WM_DDE_UNADVISE, or WM_DDE_INITIAT消息 
    public const int WM_DDE_DATA = WM_DDE_FIRST + 5; //一个DDE服务程序提交此消息给DDE客户程序来传递个一数据项给客户或通知客户的一条可用数据项 
    public const int WM_DDE_REQUEST = WM_DDE_FIRST + 6; //一个DDE客户程序提交此消息给一个DDE服务程序来请求一个数据项的值； 
    public const int WM_DDE_POKE = WM_DDE_FIRST + 7; //一个DDE客户程序提交此消息给一个DDE服务程序，客户使用此消息来请求服务器接收一个未经同意的数据项；服务器通过答复WM_DDE_ACK消息提示是否它接收这个数据项； 
    public const int WM_DDE_EXECUTE = WM_DDE_FIRST + 8; //一个DDE客户程序提交此消息给一个DDE服务程序来发送一个字符串给服务器让它象串行命令一样被处理，服务器通过提交WM_DDE_ACK消息来作回应； 
    public const int WM_DDE_LAST = WM_DDE_FIRST + 8; // 
    public const int WM_APP = 0x8000; // 
    public const int WM_USER = 0x0400; //此消息能帮助应用程序自定义私有消息；


    //============================================
    public const int MEM_COMMIT = 0x1000;
    public const int OPEN_PROCESS_ALL = 0x1f0fff;
    public const int PAGE_READWRITE = 4;
    public const int PROCESS_CREATE_THREAD = 2;
    public const int PROCESS_HEAP_ENTRY_BUSY = 4;
    public const int PROCESS_VM_OPERATION = 8;
    public const int PROCESS_VM_READ = 0x100;
    public const int PROCESS_VM_WRITE = 0x20;

    public const int WS_SHOWNORMAL = 1;
    public const uint INFINITE = 0xFFFFFFFF;
    public const int MEM_RELEASE = 0x8000;

    public const int SB_LINEDOWN = 0x1;

    public const int MK_LBUTTON = 0x1;
    public const uint GW_HWNDNEXT = 0x02;
    public const uint BM_CLICK = 0xF5;
    public const uint EM_REPLACESEL = 0xC2;

    public const int OF_READWRITE = 2;
    public const int OF_SHARE_DENY_NONE = 0x40;
    public readonly IntPtr HFILE_ERROR = new IntPtr(-1);





    ///////////////////////////////////////////////////////////////////// 
    //通知消息(Notification message)是指这样一种消息，一个窗口内的子控件发生了一些事情，需要通 
    //知父窗口。通知消息只适用于标准的窗口控件如按钮、列表框、组合框、编辑框，以及Windows 95公 
    //共控件如树状视图、列表视图等。例如，单击或双击一个控件、在控件中选择部分文本、操作控件的 
    //滚动条都会产生通知消息。

    //按扭 
    //public const int BN_CLICKED //用户单击了按钮 
    //public const int BN_DISABLE //按钮被禁止 
    //public const int BN_DOUBLECLICKED //用户双击了按钮 
    //public const int BN_HILITE //用户加亮了按钮 
    //public const int BN_PAINT //按钮应当重画 
    //public const int BN_UNHILITE //加亮应当去掉
    ////组合框 
    //public const int CBN_CLOSEUP //组合框的列表框被关闭 
    //public const int CBN_DBLCLK //用户双击了一个字符串 
    //public const int CBN_DROPDOWN //组合框的列表框被拉出 
    //public const int CBN_EDITCHANGE //用户修改了编辑框中的文本 
    //public const int CBN_EDITUPDATE //编辑框内的文本即将更新 
    //public const int CBN_ERRSPACE //组合框内存不足 
    //public const int CBN_KILLFOCUS //组合框失去输入焦点 
    //public const int CBN_SELCHANGE //在组合框中选择了一项 
    //public const int CBN_SELENDCANCEL //用户的选择应当被取消 
    //public const int CBN_SELENDOK //用户的选择是合法的 
    //public const int CBN_SETFOCUS //组合框获得输入焦点
    ////编辑框 
    //public const int EN_CHANGE //编辑框中的文本己更新 
    //public const int EN_ERRSPACE //编辑框内存不足 
    //public const int EN_HSCROLL //用户点击了水平滚动条 
    //public const int EN_KILLFOCUS //编辑框正在失去输入焦点 
    //public const int EN_MAXTEXT //插入的内容被截断 
    //public const int EN_SETFOCUS //编辑框获得输入焦点 
    //public const int EN_UPDATE //编辑框中的文本将要更新 
    //public const int EN_VSCROLL //用户点击了垂直滚动条消息含义

    //    //列表框 
    //public const int LBN_DBLCLK //用户双击了一项 
    //public const int LBN_ERRSPACE //列表框内存不够 
    //public const int LBN_KILLFOCUS //列表框正在失去输入焦点 
    //public const int LBN_SELCANCEL //选择被取消 
    //public const int LBN_SELCHANGE //选择了另一项 
    //public const int LBN_SETFOCUS //列表框获得输入焦点
}

public enum HookType : int
{
    WH_JOURNALRECORD = 0,
    WH_JOURNALPLAYBACK = 1,
    WH_KEYBOARD = 2,
    WH_GETMESSAGE = 3,
    WH_CALLWNDPROC = 4,
    WH_CBT = 5,
    WH_SYSMSGFILTER = 6,
    WH_MOUSE = 7,
    WH_HARDWARE = 8,
    WH_DEBUG = 9,
    WH_SHELL = 10,
    WH_FOREGROUNDIDLE = 11,
    WH_CALLWNDPROCRET = 12,
    WH_KEYBOARD_LL = 13,
    WH_MOUSE_LL = 14
}
