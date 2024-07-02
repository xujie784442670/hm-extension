using System;
using System.IO;
using System.Text;

namespace HmExtension.Standard.utils;

/// <summary>
/// 日志帮助类
/// </summary>
public static class LogHelper
{
    /// <summary>
    /// 是否是调试模式
    /// </summary>
    public static bool IsDebug = true;
    private static readonly object LockLog = new object();

    /// <summary>
    /// 写日志文件
    /// </summary>
    /// <param name="msg">要写入的 内容</param>
    /// <param name="tagType">要写入的 标签</param>
    public static void Log(this string msg, string tagType = "debug")
    {
        if (IsDebug == false) return;//是否开启日志
        string logPath = AppDomain.CurrentDomain.BaseDirectory + @"log\";
        if (!Directory.Exists(logPath))
        {
            Directory.CreateDirectory(logPath);
        }
        string linMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss-ffff ") + tagType + " :" + msg + "\r\n";
        lock (LockLog)
        {
            File.AppendAllText(logPath + "log.txt", linMsg, Encoding.UTF8);
        }
    }
}