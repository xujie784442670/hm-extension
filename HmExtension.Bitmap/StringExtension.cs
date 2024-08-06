#nullable enable
using System;
using HmExtension.Commons.Extensions;

namespace HmExtension.Drawing;

/// <summary>
/// 字符串扩展类
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// 将DataUrl字符串转换为图片
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>图片</returns>
    /// <exception cref="ArgumentException">如果字符串不是DataURL则抛出该异常</exception>
    public static System.Drawing.Bitmap FromBitmap(this string value)
    {
        // 检查字符串是否是DataUrl字符串
        if (!value.IsDataUrl()) throw new ArgumentException("不是DataUrl字符串");
        // 获取图片的Base64字符串
        var base64 = value.Split(',')[1];
        // 将Base64字符串转换为字节数组
        var bytes = base64.FromBase64ToBytes();
        // 将字节数组转换为图片
        return bytes.ToBitmap();
    }
}