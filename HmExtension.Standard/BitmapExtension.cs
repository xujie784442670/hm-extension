using System.Drawing;
using System.IO;

namespace HmExtension.Standard;
/// <summary>
/// 图片扩展类(v1.0.0.3)
/// </summary>
public static class BitmapExtension
{

    /// <summary>
    /// 将图片转换为字节数组
    /// </summary>
    /// <param name="bitmap">图片对象</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this Bitmap bitmap)
    {
        using var stream = new MemoryStream();
        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        return stream.ToArray();
    }
    /// <summary>
    /// 将图片转换为DataUrl
    /// </summary>
    /// <param name="bitmap">图片对象</param>
    /// <returns>字节数组</returns>
    public static string ToDataUrl(this Bitmap bitmap)
    {
        return $"data:image/png;base64,{bitmap.ToByte().ToBase64()}";
    }
}