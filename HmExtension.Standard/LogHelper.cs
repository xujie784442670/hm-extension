using System;
using System.IO;
using System.Text;

namespace HmExtension.Standard;

/// <summary>
/// 日志帮助类
/// </summary>
public static class LogHelper
{
    public static bool Is_debug = true;
    private static object Lock_log = new object();

    /// <summary>
    /// 写日志文件
    /// </summary>
    /// <param name="msg">要写入的 内容
    /// <param name="tag_type">要写入的 标签
    public static void Log(this string msg, string tag_type = "debug")
    {
        if (Is_debug == false) return;//是否开启日志
        string logPath = AppDomain.CurrentDomain.BaseDirectory + @"log\";
        if (!Directory.Exists(logPath))
        {
            Directory.CreateDirectory(logPath);
        }
        string lin_msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss-ffff ") + tag_type + " :" + msg + "\r\n";
        lock (Lock_log)
        {
            File.AppendAllText(logPath + "log.txt", lin_msg, Encoding.UTF8);
        }
    }
}