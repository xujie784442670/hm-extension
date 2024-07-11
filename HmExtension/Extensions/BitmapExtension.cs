using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using HmExtension.Commons.Extensions;
using ZXing;

namespace HmExtension.Extensions;
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
        bitmap.Save(stream, ImageFormat.Png);
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

    /// <summary>
    /// 解析二维码
    /// </summary>
    /// <param name="barcodeBitmap"></param>
    /// <returns></returns>
    public static string DecodeQrCode(this Bitmap barcodeBitmap)
    {
        BarcodeReader reader = new BarcodeReader();
        reader.Options.CharacterSet = "UTF-8";
        Result result = reader.Decode(barcodeBitmap);
        return result == null ? "" : result.Text;
    }
}