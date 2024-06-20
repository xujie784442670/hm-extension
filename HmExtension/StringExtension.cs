using System;
using System.Globalization;
using System.Text;

namespace HmExtension;

/// <summary>
/// 字符串扩展类
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// 将字符串转换为驼峰命名(大驼峰)
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="isUpper">是否大驼峰,默认为大驼峰,如果为false则返回小驼峰</param>
    /// <param name="separator">分隔符,如果为空,将所有非字母字符视为分隔符</param>
    /// <returns>新字符串</returns>
    public static string ToCamelCase(this string value, bool isUpper = true, string separator = "")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        if (string.IsNullOrWhiteSpace(separator))
        {
            separator = " ";
            var chars = value.ToLower().ToCharArray();
            value = "";
            foreach (var t in chars)
            {
                // 检查是否是非字母
                if (!char.IsLetter(t))
                {
                    value += " ";
                }
                else
                {
                    value += t;
                }
            }
        }

        var strings = value.Split(separator.ToCharArray());
        value = "";
        foreach (var str in strings)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                continue;
            }

            value += str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        if (isUpper == false)
        {
            // 小驼峰
            value = value.Substring(0, 1).ToLower() + value.Substring(1);
        }

        return value;
    }


    /// <summary>
    /// 将字符串输出到控制台,支持格式化
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="prefix">前缀</param>
    /// <param name="suffix">后缀</param>
    /// <param name="args">格式化参数,如果传递了该参数,则自动将字符串视为格式化字符串</param>
    public static void Print(this string value, string prefix = "", string suffix = "", params object[] args)
    {
        value = args.Length > 0 ? string.Format(value, args) : value;
        System.Console.Write($"{prefix}{value}{suffix}");
    }

    /// <summary>
    /// 将字符串输出到控制台,支持格式化
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="args">格式化参数,如果传递了该参数,则自动将字符串视为格式化字符串</param>
    public static void Print(this string value, params object[] args)
    {
        Print(value, "", "", args);
    }

    /// <summary>
    /// 将字符串输出到控制台,支持格式化,并在末尾添加换行符
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="args">格式化参数,如果传递了该参数,则自动将字符串视为格式化字符串</param>
    public static void Println(this string value, params object[] args)
    {
        Print(value + Environment.NewLine, args);
    }

    /// <summary>
    /// 将字符串输出到控制台,支持格式化,并在末尾添加换行符
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="prefix">前缀</param>
    /// <param name="suffix">后缀</param>
    /// <param name="args">格式化参数,如果传递了该参数,则自动将字符串视为格式化字符串</param>
    public static void Println(this string value, string prefix, string suffix, params object[] args)
    {
        Print(value + Environment.NewLine, prefix, suffix, args);
    }

    /// <summary>
    /// 将字符串转换为Base64编码
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>Base64字符串</returns>
    public static string ToBase64(this string value)
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    /// 将字符串转换为字节数组
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="encoder">转换编码,默认为
    /// <see cref="Encoding.Default">Encoding.Default</see></param>
    /// <returns>字节数组</returns>
    public static byte[] ToBytes(this string value, Encoding encoder = null)
    {
        encoder ??= Encoding.Default;
        return encoder.GetBytes(value);
    }

    /// <summary>
    /// 将16进制字符串转换为字节数组
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>字节数组</returns>
    public static byte[] FromHex(this string value)
    {
        // 将字符串中所有的非16进制字符替换为空
        value = value.Replace(@"[^0-9a-fA-F]", "");
        var bytes = new byte[value.Length / 2];
        for (var i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
        }

        return bytes;
    }

    /// <summary>
    /// 判断字符串是否为空字符串
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>true or false</returns>
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// 判断字符串是否为空字符串或仅由空白字符组成
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>true or false</returns>
    public static bool IsEmptyOrWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// 使用正则表达式替换字符串
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="pattern">正则表达式,默认为所有字符</param>
    /// <param name="replacement">替换的字符串,默认为字符串</param>
    /// <returns>替换后的字符串</returns>
    public static string ReplaceRegex(this string value, string pattern = @".*", string replacement = "")
    {
        if (value is null || string.IsNullOrWhiteSpace(value)) return value;
        // 使用正则表达式替换
        return System.Text.RegularExpressions.Regex.Replace(value, pattern, replacement);
    }

    /// <summary>
    /// 将Base64编码的字符串转换为普通字符串
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>解密后的字符串</returns>
    public static string FromBase64(this string value)
    {
        return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value));
    }

    /// <summary>
    /// 计算字符串的MD5值
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>MD5字符串</returns>
    public static string ToMd5(this string value)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();
        var bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
        var sb = new System.Text.StringBuilder();
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// 反转字符串
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>反转后的字符串</returns>
    public static string Reverse(this string value)
    {
        var chars = value.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    /// <summary>
    /// 将字符串转换为整数
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>整数</returns>
    public static int ToInt(this string value, NumberStyles style = NumberStyles.Integer)
    {
        return int.Parse(value, style);
    }

    /// <summary>
    /// 将字符串转换为长整数
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>长整数</returns>
    public static long ToLong(this string value, NumberStyles style = NumberStyles.Integer)
    {
        return long.Parse(value, style);
    }

    /// <summary>
    /// 将字符串转换为short
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>short</returns>
    public static short ToShort(this string value, NumberStyles style = NumberStyles.Integer)
    {
        return short.Parse(value, style);
    }

    /// <summary>
    /// 将字符串转换为float
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>float</returns>
    public static float ToFloat(this string value,
        NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
    {
        return float.Parse(value, style);
    }

    /// <summary>
    /// 将字符串转换为double
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>double</returns>
    public static double ToDouble(this string value,
        NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
    {
        return double.Parse(value, style);
    }

    /// <summary>
    /// 将字符串转换为byte
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>byte</returns>
    public static byte ToByte(this string value, NumberStyles style = NumberStyles.Integer)
    {
        return byte.Parse(value, style);
    }

    /// <summary>
    /// 将字符串转换为无符号整数
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>ushort</returns>
    public static ushort ToUShort(this string value, NumberStyles style = NumberStyles.Integer)
    {
        return ushort.Parse(value, style);
    }

    /// <summary>
    /// 将字符串转换为无符号长整数
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>uint</returns>
    public static uint ToUInt(this string value, NumberStyles style = NumberStyles.Integer)
    {
        return uint.Parse(value, style);
    }

    /// <summary>
    /// 将字符串转换为无符号长整数
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="style"> 数字格式</param>
    /// <returns>ulong</returns>
    public static ulong ToULong(this string value, NumberStyles style = NumberStyles.Integer)
    {
        return ulong.Parse(value, style);
    }
    /// <summary>
    /// 格式化字符串
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="args">格式化参数</param>
    /// <seealso cref="string.Format(string, object[])"/>
    /// <returns>格式化字符串</returns>
    public static string Format(this string value, params object[] args)
    {
        return string.Format(value, args);
    }
}