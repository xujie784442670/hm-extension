using System;
using HmExtension.Commons.Model;

namespace HmExtension.Commons.utils;

/// <summary>
/// 随机帮助类
/// </summary>
public class RandomHelper
{
    public static Random Random { get; } = new Random();

    public const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";

    public const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public const string Letters = LowercaseLetters + UppercaseLetters;

    public const string Numbers = "0123456789";

    public const string LettersAndNumbers = Letters + Numbers;

    /// <summary>
    /// 获取一个随机数,取值范围为[minValue,maxValue)
    /// </summary>
    /// <param name="minValue">
    ///     最小值
    /// </param>
    /// <param name="maxValue">最大值</param>
    /// <returns></returns>
    public static int Next(int minValue, int maxValue)
    {
        return Random.Next(minValue, maxValue);
    }

    /// <summary>
    /// 获取一个随机数,取值范围为[0,maxValue)
    /// </summary>
    /// <param name="maxValue">最大值</param>
    /// <returns></returns>
    public static int Next(int maxValue)
    {
        return Random.Next(maxValue);
    }

    /// <summary>
    /// 获取一个随机数,取值范围为[0,int.MaxValue)
    /// </summary>
    /// <returns></returns>
    public static int Next()
    {
        return Random.Next();
    }

    /// <summary>
    /// 获取一个随机数,取值范围为[0,1)
    /// </summary>
    /// <returns></returns>
    public static double NextDouble()
    {
        return Random.NextDouble();
    }

    /// <summary>
    /// 生成随机字节数组
    /// </summary>
    /// <param name="buffer"></param>
    public static void NextBytes(byte[] buffer)
    {
        Random.NextBytes(buffer);
    }

    /// <summary>
    /// 生成随机字节数组
    /// </summary>
    /// <param name="buffer"></param>
    public static void NextBytes(Span<byte> buffer)
    {
        var bytes = new byte[buffer.Length];
        Random.NextBytes(bytes);
        bytes.CopyTo(buffer);
    }

    /// <summary>
    /// 获取一个随机布尔值
    /// </summary>
    /// <returns></returns>
    public static bool NextBool()
    {
        return Random.Next(2) == 1;
    }

    /// <summary>
    /// 获取一个随机枚举值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T NextEnum<T>()
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Next(values.Length));
    }

    /// <summary>
    /// 获取一个随机枚举值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T NextEnum<T>(params T[] values)
    {
        return values[Random.Next(values.Length)];
    }

    /// <summary>
    /// 获取一个随机颜色
    /// </summary>
    /// <param name="isAlpha">是否随机透明度</param>
    /// <returns></returns>
    public static Color NextColor(bool isAlpha)
    {
        return Color.FromArgb((byte)Next(256), (byte)Next(256), (byte)Next(256), (byte)(isAlpha ? Next(256) : 255));
    }

    /// <summary>
    /// 随机获取一个指定长度的字符串
    /// </summary>
    /// <param name="length">字符串长度,如果为0,则会随机生成一个[1,100)的长度</param>
    /// <param name="chars">字符串列表</param>
    /// <returns></returns>
    public static string NextString(int length=0, string chars = LettersAndNumbers)
    {
        if (length <= 0)
        {
            length = Next(1,100);
        }
        var buffer = new char[length];
        for (var i = 0; i < length; i++)
        {
            buffer[i] = chars[Next(chars.Length)];
        }

        return new string(buffer);
    }

}