using System.Drawing;
using System.IO;

namespace HmExtension.Standard.Extensions;

/// <summary>
/// 字节数组扩展类
/// </summary>
public static class ByteArrayExtension
{
    /// <summary>
    /// 将字节数组转换为Bitmap
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <returns>Bitmap</returns>
    public static Bitmap ToBitmap(this byte[] value)
    {
        using var stream = new MemoryStream(value);
        return new Bitmap(stream);
    }
}