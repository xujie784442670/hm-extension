using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace HmExtension.Standard;

/// <summary>
/// 进程工具类
/// </summary>
public class ProcessHelper
{
    /// <summary>
    /// 根据进程名结束进程 不需要后缀 多个 相同进程名 会被一起结束
    /// </summary>
    /// <param name="prossName"></param>
    public static void KillProcessByName(string prossName)
    {
        string newName = prossName.Replace(".exe", "");
        try
        {
            Process[] pp = Process.GetProcessesByName(newName);
            foreach (var p in pp)
            {
                if (p.ProcessName == newName)
                {
                    p.Kill();
                }
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// 根据进程名和进程PID，关闭指定进程 - 进程名不需要带后缀
    /// </summary>
    /// <param name="prossName">进程名</param>
    /// <param name="closePid">需要关闭的进程PID</param>
    public static void CloseProcessByProcessId(string prossName, int closePid)
    {
        Process[] pp = Process.GetProcessesByName(prossName);
        foreach (var p in pp)
        {
            if (p.Id == closePid)
            {
                p.Kill();
            }
        }
    }

    /// <summary>
    /// 根据进程名获得进程Process对象的集合 - 不需要带上进程后缀
    /// </summary>
    /// <param name="prossName">进程名</param>
    /// <param name="pro">进程Process 对象集合</param>
    /// <returns>找不到返回 false</returns>
    public static bool GetProcessPorExByProssName(string prossName, ref List<Process> pro)
    {
        Process[] pp = Process.GetProcessesByName(prossName);
        foreach (var p in pp)
        {
            if (p.ProcessName == prossName)
            {
                pro.Add(p);
            }
        }

        return pro.Any();
    }

    [DllImport("user32", EntryPoint = "GetWindowThreadProcessId")]
    private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int pid);

    /// <summary>
    /// 根据窗口句柄获得进程PID和线程PID
    /// </summary>
    /// <param name="hwnd">句柄</param>
    /// <param name="pid">返回 进程PID</param>
    /// <returns>方法的返回值，线程PID，进程PID和线程PID这两个概念不同</returns>
    public static int GetPidByHwnd(int hwnd, out int pid)
    {
        pid = 0;
        return GetWindowThreadProcessId((IntPtr)hwnd, out pid);
    }

    /// <summary>
    /// 根据窗口标题获得进程Process对象
    /// </summary>
    /// <param name="title">窗口标题</param>
    /// <param name="pro">进程Process 对象</param>
    /// <returns>找不到返回 false</returns>
    public static bool GetProcessPorByTitle(string title, out Process pro)
    {
        pro = null;
        Process[] arrayProcess = Process.GetProcesses();
        foreach (Process p in arrayProcess)
        {
            if (p.MainWindowTitle.IndexOf(title, StringComparison.Ordinal) != -1)
            {
                pro = p;
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// 根据窗口标题查找窗口进程PID-返回List
    /// </summary>
    /// <param name="title">窗口标题</param>
    /// <returns>List</returns>
    public static List<int> FindPidExByTitle(string title)
    {
        List<int> list = new List<int>();
        Process[] arrayProcess = Process.GetProcesses();
        foreach (Process p in arrayProcess)
        {
            if (p.MainWindowTitle.IndexOf(title, StringComparison.Ordinal) != -1)
            {
                list.Add(p.Id);
            }
        }

        return list;
    }


    /// <summary>
    /// 根据进程名获得进程PID - 不需要带上进程后缀
    /// </summary>
    /// <param name="prossName">进城名</param>
    /// <returns>进城PID 找不到返回 0</returns>
    public static int GetProcessPid(string prossName)
    {
        Process[] pp = Process.GetProcessesByName(prossName);
        foreach (var p in pp)
        {
            if (p.ProcessName == prossName)
            {
                return p.Id;
            }
        }

        return 0;
    }


    /// <summary>
    /// 运行一个指定文件或者程序
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>失败返回false</returns>
    public static bool RunApp(string path)
    {
        try
        {
            Process pro = new Process();
            pro.StartInfo.FileName = @path;
            pro.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return true;
    }


    /// <summary>
    /// 运行一个指定文件或者程序可以带上参数
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="flag">附带参数</param>
    /// <returns>失败返回false</returns>
    private static bool RunAppByFlag(string path, string flag)
    {
        try
        {
            Process pro = new Process();
            pro.StartInfo.FileName = @path;
            pro.StartInfo.Arguments = flag;
            pro.Start();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return true;
    }


    /// <summary>
    /// 通过句柄获得进程路径
    /// </summary>
    /// <param name="hwnd">句柄</param>
    /// <returns>返回 进程路径 找不到返回""</returns>
    public static string GetAppRunPath_ByHandle(int hwnd)
    {
        Process[] ps = Process.GetProcesses();
        foreach (Process p in ps)
        {
            if ((int)p.MainWindowHandle == hwnd)
            {
                return p.MainModule?.FileName;
            }
        }

        return default;
    }


    /// <summary>
    /// 通过进程名获得进程路径 不需要后缀
    /// </summary>
    /// <param name="hwnd">句柄</param>
    /// <param name="prossName">进程名</param>
    /// <returns>返回 进程路径 找不到返回""</returns>
    public static string GetAppRunPath_ByName(string prossName)
    {
        Process[] ps = Process.GetProcesses();
        foreach (Process p in ps)
        {
            if (p.ProcessName == prossName)
            {
                return p.MainModule?.FileName;
            }
        }

        return "";
    }
}