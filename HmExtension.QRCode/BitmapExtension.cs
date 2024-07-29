using System.Drawing;
using ZXing;

namespace HmExtension.QRCode;
/// <summary>
/// 图片扩展类(v1.0.0.3)
/// </summary>
public static class BitmapExtension
{
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