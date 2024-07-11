using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace HmExtension.Commons.Extensions;

/// <summary>
/// 字符串扩展类
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// 将字符串转换为驼峰命名(大驼峰)
    /// <example>
    /// <code>
    /// "user name".ToCamelCase(); // UserName
    /// "user name".ToCamelCase(false); // userName
    /// "user-name".ToCamelCase(false, "-"); // userName
    /// "user_name".ToCamelCase(false, "_"); // userName
    /// "last_update time".ToCamelCase(); // LastUpdateTime
    /// "last_update time".ToCamelCase(false); // lastUpdateTime
    /// "last_update time".ToCamelCase(false, " "); // last_updateTime
    /// "last_update time".ToCamelCase(false, "_"); // last_update time
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// "Hello World".Print(); // 控制台输出: Hello World
    /// "张三".Print("姓名: "); // 控制台输出: 姓名: 张三
    /// "25".Print("年龄: ", "岁"); // 控制台输出: 年龄: 25岁
    /// "{0}".Print("今天温度: ", "℃", 25); // 控制台输出: 今天温度: 25℃
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// "Hello World".Print(); // 控制台输出: Hello World
    /// "张三".Print("姓名: "); // 控制台输出: 姓名: 张三
    /// "你好: {0}".Print("张三"); // 控制台输出: 你好: 张三
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="args">格式化参数,如果传递了该参数,则自动将字符串视为格式化字符串</param>
    public static void Print(this string value, params object[] args)
    {
        Print(value, "", "", args);
    }

    /// <summary>
    /// 将字符串输出到控制台,支持格式化,并在末尾添加换行符
    /// <example>
    /// <code>
    /// "Hello World".Println(); // 控制台输出: Hello World\r\n
    /// "张三".Println("姓名: "); // 控制台输出: 姓名: 张三\r\n
    /// "你好: {0}".Println("张三"); // 控制台输出: 你好: 张三\r\n
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="args">格式化参数,如果传递了该参数,则自动将字符串视为格式化字符串</param>
    public static void Println(this string value, params object[] args)
    {
        Print(value + Environment.NewLine, args);
    }

    /// <summary>
    /// 将字符串输出到控制台,支持格式化,并在末尾添加换行符
    /// <example>
    /// <code>
    /// "Hello World".Println(); // 控制台输出: Hello World\r\n
    /// "张三".Println("姓名: "); // 控制台输出: 姓名: 张三\r\n
    /// "25".Println("年龄: ", "岁"); // 控制台输出: 年龄: 25岁\r\n
    /// "{0}".Println("今天温度: ", "℃", 25); // 控制台输出: 今天温度: 25℃\r\n
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// "Hello World".ToBase64(); // SGVsbG8gV29ybGQ=
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>Base64字符串</returns>
    public static string ToBase64(this string value)
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    /// 将字符串转换为字节数组
    /// <example>
    /// <code>
    /// "Hello World".ToBytes(); // {72, 101, 108, 108, 111, 32, 87, 111, 114, 108, 100}
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// "48656C6C6F20576F726C64".FromHex(); // {72, 101, 108, 108, 111, 32, 87, 111, 114, 108, 100}
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// "".IsEmpty(); // true
    /// "Hello World".IsEmpty(); // false
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>true or false</returns>
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// 判断字符串是否为空字符串或仅由空白字符组成
    /// <example>
    /// <code>
    /// "".IsEmptyOrWhiteSpace(); // true
    /// "Hello World".IsEmptyOrWhiteSpace(); // false
    /// "   ".IsEmptyOrWhiteSpace(); // true
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>true or false</returns>
    public static bool IsEmptyOrWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// 使用正则表达式替换字符串
    /// <example>
    /// <code>
    /// "Hello World".ReplaceRegex(); // 输出: ""
    /// "123abc456".ReplaceRegex(@"\d", "#"); // 输出: "###abc###"
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// "SGVsbG8gV29ybGQ=".FromBase64(); // Hello World
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>解密后的字符串</returns>
    public static string FromBase64(this string value)
    {
        return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value));
    }

    /// <summary>
    /// 计算字符串的MD5值
    /// <example>
    /// <code>
    /// "123456".ToMd5(); // e10adc3949ba59abbe56e057f20f883e
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// "123456".Reverse(); // 654321
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// int i = "123".ToInt(); // 123
    /// int i = "a1b2c3".ToInt(<see cref="NumberStyles.HexNumber">NumberStyles.HexNumber</see>); // 0x1a2b3
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// long i = "123".ToLong(); // 123
    /// long i = "a1b2c3".ToLong(<see cref="NumberStyles.HexNumber">NumberStyles.HexNumber</see>); // 0x1a2b3
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// short i = "123".ToShort(); // 123
    /// short i = "a1b2c3".ToShort(<see cref="NumberStyles.HexNumber">NumberStyles.HexNumber</see>); // 0x1a2b3
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// float i = "123.45".ToFloat(); // 123.45
    /// float i = "1.2345e2".ToFloat(); // 123.45
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// double i = "123.45".ToDouble(); // 123.45
    /// double i = "1.2345e2".ToDouble(); // 123.45
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// byte[] arr = "123".ToByte();
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// ushort i = "123".ToUShort();
    /// ushort i = "a1b2c3".ToUShort(<see cref="NumberStyles.HexNumber">NumberStyles.HexNumber</see>);
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// uint i = "123".ToUInt();
    /// uint i = "a1b2c3".ToUInt(<see cref="NumberStyles.HexNumber">NumberStyles.HexNumber</see>);
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// ulong i = "123".ToULong();
    /// ulong i = "a1b2c3".ToULong(<see cref="NumberStyles.HexNumber">NumberStyles.HexNumber</see>);
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// "Hello {0}".Format("World"); // Hello World
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="args">格式化参数</param>
    /// <seealso cref="string.Format(string, object[])"/>
    /// <returns>格式化字符串</returns>
    public static string Format(this string value, params object[] args)
    {
        return string.Format(value, args);
    }


    /// <summary>
    /// 将JSON字符串转换为对象
    /// <example >
    /// <code>
    /// class Student{
    ///     public string Name { get; set; }
    ///     public int Age { get; set; }
    /// }
    /// 
    /// var stu = "{\"Name\":\"张三\",\"Age\":20}".FromJson&lt;Student&gt;();
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">当前字符串</param>
    /// <param name="converters">JSON转换器</param>
    /// <returns>对象</returns>
    /// <seealso cref="Newtonsoft.Json.JsonConvert.DeserializeObject(string)"/>
    /// <seealso cref="Newtonsoft.Json.JsonConverter"/>
    public static T FromJson<T>(this string value, params JsonConverter[] converters)
    {
        return JsonConvert.DeserializeObject<T>(value, converters);
    }


    /// <summary>
    /// 检查字符串是否是DataUrl字符串
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>true or false</returns>
    public static bool IsDataUrl(this string value)
    {
        return value.StartsWith("data:image");
    }
    /// <summary>
    /// 将Base64字符串转换为字节数组
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>字节数组</returns>
    public static byte[] FromBase64ToBytes(this string value)
    {
        return Convert.FromBase64String(value);
    }
    
}