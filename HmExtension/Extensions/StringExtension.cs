#nullable enable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using HmExtension.Commons.Extensions;
using ZXing;
using ZXing.QrCode.Internal;

namespace HmExtension.Extensions;

/// <summary>
/// 字符串扩展类
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// 将字符串转换为QRCode二维码
    /// <example>
    /// <code>
    /// // 将字符串转换为二维码
    /// Bitmap bitmap = "http://www.baidu.com".ToQRCode();
    /// // 生成一个绿色的二维码,背景色为白色
    /// Bitmap bitmap = "http://www.baidu.com".ToQRCode(darkColor:Color.Green, lightColor:Color.White);
    /// // 生成一个带有LOGO的二维码
    /// Bitmap bitmap = "http://www.baidu.com".ToQRCode(icon: new Bitmap("logo.png"));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <param name="size">设置图片长宽,默认200</param>
    /// <param name="level">容错等级
    ///     <list type="bullet">
    ///         <item>
    ///             <term>ErrorCorrectionLevel.L: </term>
    ///             <description>大约 7% 的错误更正能力</description>
    ///         </item>
    ///         <item>
    ///             <term>ErrorCorrectionLevel.M: </term>
    ///             <description>大约 15% 的错误更正能力。</description>
    ///         </item>
    ///         <item>
    ///             <term>ErrorCorrectionLevel.Q: </term>
    ///             <description>大约 25% 的错误更正能力。</description>
    ///         </item>
    ///         <item>
    ///             <term>ErrorCorrectionLevel.H: </term>
    ///             <description>大约 30% 的错误更正能力。</description>
    ///         </item>
    ///     </list>
    /// </param>
    /// <param name="drawQuietZones">静止区，位于二维码某一边的空白边界,用来阻止读者获取与正在浏览的二维码无关的信息 即是否绘画二维码的空白边框区域 默认为true</param>
    /// <param name="drawQuietZonesSize">静止区尺寸,默认为<see cref="QuietZoneModules.Two"/></param>
    /// <param name="icon">二维码水印图标 默认为NULL ，加上这个二维码中间会显示一个图标</param>
    /// <param name="iconSizePercent">水印图标的大小比例 ，可根据自己的喜好设置 </param>
    /// <param name="iconBorderWidth">水印图标的边框</param>
    /// <param name="iconBackgroundColor">水印图标的背景色,默认为Color.White  白色</param>
    /// <param name="encoding">字符串编码集</param>
    /// <returns>二维码图片</returns>
    public static Bitmap ToQrCode(this string value,
        int size = 200,
        ErrorCorrectionLevel? level = default,
        Bitmap? icon = null,
        int iconSizePercent = 15,
        int iconBorderWidth = 0,
        Color? iconBackgroundColor = null,
        bool drawQuietZones = true,
        int drawQuietZonesSize = 10,
        Encoding? encoding = default)
    {
        BarcodeWriter writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, encoding?.EncodingName ?? Encoding.Default.EncodingName); //编码问题
        writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION,level ?? ErrorCorrectionLevel.Q);
        writer.Options.Height = writer.Options.Width = size;
        writer.Options.Margin = drawQuietZones ? drawQuietZonesSize:0; //设置边框
        ZXing.Common.BitMatrix bm = writer.Encode(value);
        Bitmap map = writer.Write(bm);
        if (icon != null)
        {
            //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //计算插入图片的大小和位置
            int middleW = iconSizePercent * rectangle[2] / 100 - iconBorderWidth * 2;
            int middleH = iconSizePercent * rectangle[3] / 100 - iconBorderWidth * 2;
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;
            //将img转换成bmp格式，否则后面无法创建Graphics对象
            Bitmap bmpimg = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0);
            }
            //将二维码插入图片
            Graphics myGraphic = Graphics.FromImage(bmpimg);
            //白底
            myGraphic.FillRectangle(new SolidBrush(iconBackgroundColor ?? Color.White), middleL, middleT, middleW + iconBorderWidth * 2, middleH+ iconBorderWidth * 2);
            myGraphic.DrawImage(icon, new Rectangle(middleL+iconBorderWidth, middleT + iconBorderWidth, middleW, middleH),new Rectangle(0,0,icon.Width,icon.Height),GraphicsUnit.Pixel);
            map = bmpimg;
        }
        return map;
    }

    /// <summary>
    /// 将DataUrl字符串转换为图片
    /// </summary>
    /// <param name="value">当前字符串</param>
    /// <returns>图片</returns>
    /// <exception cref="ArgumentException">如果字符串不是DataURL则抛出该异常</exception>
    public static Bitmap FromBitmap(this string value)
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