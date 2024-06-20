using System;

namespace HmExtension;
/// <summary>
/// 对象扩展类
/// </summary>
public static class ObjectExtension
{
    /// <summary>
    /// 将对象打印到控制台
    /// </summary>
    /// <param name="value">当前对象</param>
    /// <param name="prefix">前缀字符串</param>
    /// <param name="suffix">后缀字符串</param>
    public static void Print(this object value, string prefix = "", string suffix = "")
    {
        Console.Write(prefix + value + suffix);
    }    
    /// <summary>
    /// 将对象打印到控制台
    /// </summary>
    /// <param name="value">当前对象</param>
    public static void Print(this object value)
    {
        Print(value,"","");
    }

    /// <summary>
    /// 将对象打印到控制台,并在末尾添加换行符
    /// </summary>
    /// <param name="value">当前对象</param>
    /// <param name="prefix">前缀字符串</param>
    /// <param name="suffix">后缀字符串</param>
    public static void Println(this object value, string prefix = "", string suffix = "")
    {
        Print(value, prefix, $"{suffix}{Environment.NewLine}");
    }
    /// <summary>
    /// 将对象打印到控制台,并在末尾添加换行符
    /// </summary>
    /// <param name="value">当前对象</param>
    public static void Println(this object value)
    {
        Println(value,"","");
    }
}