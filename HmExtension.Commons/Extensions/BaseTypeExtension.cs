using System;

namespace HmExtension.Commons.Extensions;
/// <summary>
/// 基础类型扩展类
/// </summary>
public static class BaseTypeExtension
{
    /// <summary>
    /// 将short转换为字节数组
    /// <example>
    /// <code>
    /// short s = 10;
    /// byte[] bytes = s.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">shot</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this short value)
    {
        return BitConverter.GetBytes(value);
    }
    /// <summary>
    /// 将short转换为16进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToHexString(this short value)
    {
        return Convert.ToString(value, 16).PadLeft(4,'0');
    }
    /// <summary>
    /// 将short转换为二进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToBinaryString(this short value)
    {
        return Convert.ToString(value, 2).PadLeft(16,'0');
    }
    /// <summary>
    /// 将int转换为字节数组
    /// <example>
    /// <code>
    /// int i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">int</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this int value)
    {
        return BitConverter.GetBytes(value);
    }
    /// <summary>
    /// 将int转换为16进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToHexString(this int value)
    {
        return Convert.ToString(value, 16).PadLeft(8,'0');
    }
    /// <summary>
    /// 将int转换为二进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToBinaryString(this int value)
    {
        return Convert.ToString(value, 2).PadLeft(32,'0');
    }
    /// <summary>
    /// 将long转换为字节数组
    /// <example>
    /// <code>
    /// long i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">long</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this long value)
    {
        return BitConverter.GetBytes(value);
    }
    /// <summary>
    /// 将long转换为16进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToHexString(this long value)
    {
        return Convert.ToString(value, 16).PadLeft(16,'0');
    }
    /// <summary>
    /// 将long转换为二进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToBinaryString(this long value)
    {
        return Convert.ToString(value, 2).PadLeft(64,'0');
    }
    /// <summary>
    /// 将float转换为字节数组
    /// <example>
    /// <code>
    /// float i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">float</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this float value)
    {
        return BitConverter.GetBytes(value);
    }
   
    /// <summary>
    /// 将double转换为字节数组
    /// <example>
    /// <code>
    /// double i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">double</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this double value)
    {
        return BitConverter.GetBytes(value);
    }
    /// <summary>
    /// 将char转换为字节数组
    /// <example>
    /// <code>
    /// char i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">char</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this char value)
    {
        return BitConverter.GetBytes(value);
    }

    /// <summary>
    /// 将bool转换为字节数组
    /// <example>
    /// <code>
    /// bool i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">bool</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this bool value)
    {
        return BitConverter.GetBytes(value);
    }
    /// <summary>
    /// 将ushort转换为字节数组
    /// <example>
    /// <code>
    /// ushort i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">ushort</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this ushort value)
    {
        return BitConverter.GetBytes(value);
    }
    /// <summary>
    /// 将ushort转换为16进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToHexString(this ushort value)
    {
        return Convert.ToString(value, 16).PadLeft(4,'0');
    }
    /// <summary>
    /// 将ushort转换为二进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToBinaryString(this ushort value)
    {
        return Convert.ToString(value, 2).PadLeft(16,'0');
    }
    /// <summary>
    /// 将uint转换为字节数组
    /// <example>
    /// <code>
    /// uint i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">uint</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this uint value)
    {
        return BitConverter.GetBytes(value);
    }
    /// <summary>
    /// 将uint转换为16进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToHexString(this uint value)
    {
        return Convert.ToString(value, 16).PadLeft(8,'0');
    }
    /// <summary>
    /// 将uint转换为二进制字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToBinaryString(this uint value)
    {
        return Convert.ToString(value, 2).PadLeft(32,'0');
    }
    /// <summary>
    /// 将ulong转换为字节数组
    /// <example>
    /// <code>
    /// ulong i = 10;
    /// byte[] bytes = i.ToByte();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">ulong</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this ulong value)
    {
        return BitConverter.GetBytes(value);
    }
}