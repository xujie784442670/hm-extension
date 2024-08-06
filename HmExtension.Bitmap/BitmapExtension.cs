using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using HmExtension.Commons.Extensions;

namespace HmExtension.Drawing;

/// <summary>
/// 图片扩展类(v1.0.0.3)
/// </summary>
public static class BitmapExtension
{
    /// <summary>
    /// 正方型裁剪
    /// 以图片中心为轴心，截取正方型，然后等比缩放
    /// 用于头像处理
    /// </summary>
    /// <param name="fromFile">原图Stream对象</param>
    /// <param name="side">指定的边长（正方型）</param>
    /// <param name="quality">质量（范围0-100）</param>
    public static Image CutForSquare(this Image initImage, int side, int quality)
    {
        //原图宽高均小于模版，不作处理，直接保存
        if ((initImage.Width <= side) && (initImage.Height <= side))
        {
            return initImage;
        }
        else
        {
            //原始图片的宽、高
            int initWidth = initImage.Width;
            int initHeight = initImage.Height;

            //非正方型先裁剪为正方型
            if (initWidth != initHeight)
            {
                //截图对象
                Image pickedImage;
                Graphics pickedG;

                //宽大于高的横图
                if (initWidth > initHeight)
                {
                    //对象实例化
                    pickedImage = new System.Drawing.Bitmap(initHeight, initHeight);
                    pickedG = Graphics.FromImage(pickedImage);
                    //设置质量
                    pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = SmoothingMode.HighQuality;
                    //定位
                    Rectangle fromR = new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight);
                    Rectangle toR = new Rectangle(0, 0, initHeight, initHeight);
                    //画图
                    pickedG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);
                    //重置宽
                    initWidth = initHeight;
                }
                //高大于宽的竖图
                else
                {
                    //对象实例化
                    pickedImage = new System.Drawing.Bitmap(initWidth, initWidth);
                    pickedG = Graphics.FromImage(pickedImage);
                    //设置质量
                    pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = SmoothingMode.HighQuality;
                    //定位
                    Rectangle fromR = new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                    Rectangle toR = new Rectangle(0, 0, initWidth, initWidth);
                    //画图
                    pickedG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);
                    //重置高
                    initHeight = initWidth;
                }

                //将截图对象赋给原图
                initImage = (Image)pickedImage.Clone();
                //释放截图资源
                pickedG.Dispose();
                pickedImage.Dispose();
            }

            //缩略图对象
            Image resultImage = new System.Drawing.Bitmap(side, side);
            Graphics resultG = Graphics.FromImage(resultImage);
            //设置质量
            resultG.InterpolationMode = InterpolationMode.HighQualityBicubic;
            resultG.SmoothingMode = SmoothingMode.HighQuality;
            //用指定背景色清空画布
            resultG.Clear(Color.White);
            //绘制缩略图
            resultG.DrawImage(initImage, new Rectangle(0, 0, side, side), new Rectangle(0, 0, initWidth, initHeight),
                GraphicsUnit.Pixel);

            //关键质量控制
            //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
            ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo i in icis)
            {
                if ((i.MimeType == "image/jpeg") || (i.MimeType == "image/bmp") || (i.MimeType == "image/png") ||
                    (i.MimeType == "image/gif"))
                    ici = i;
            }

            EncoderParameters ep = new EncoderParameters(1);
            ep.Param[0] = new EncoderParameter(Encoder.Quality, quality);
            ep.Dispose();
            //释放缩略图资源
            resultG.Dispose();
            //释放原始图片资源
            initImage.Dispose();
            return resultImage;
        }
    }

    /// <summary>
    /// 绘制矩形
    /// </summary>
    /// <param name="image">源图片</param>
    /// <param name="rect">矩形位置大小信息</param>
    /// <param name="color">画笔颜色(默认红色)</param>
    /// <param name="width">画笔宽度</param>
    /// <param name="isFull">是否填充</param>
    /// <returns></returns>
    public static Image DrawRect(this Image image, Rectangle rect, Color? color = null, int width = 1,
        bool isFull = false)
    {
        using var g = Graphics.FromImage(image);
        using var pen = new Pen(color ?? Color.Red, width);
        if (isFull)
        {
            g.FillRectangle(new SolidBrush(color ?? Color.Red), rect);
        }
        else
        {
            g.DrawRectangle(pen, rect);
        }
        return image;
    }
    /// <summary>
    /// 绘制文字
    /// </summary>
    /// <param name="image">源图片</param>
    /// <param name="text">文本内容</param>
    /// <param name="point">起始位置</param>
    /// <param name="color">画笔颜色</param>
    /// <param name="font">字体(默认宋体,12px)</param>
    /// <returns></returns>
    public static Image DrawText(this Image image, string text, Point point, Color? color = null, Font? font = null)
    {
        using var g = Graphics.FromImage(image);
        using var brush = new SolidBrush(color ?? Color.Red);
        g.DrawString(text, font ?? new Font("宋体", 12), brush, point);
        return image;
    }

    /// <summary>
    /// 将图片转换为字节数组
    /// </summary>
    /// <param name="bitmap">图片对象</param>
    /// <returns>字节数组</returns>
    public static byte[] ToByte(this System.Drawing.Bitmap bitmap)
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
    public static string ToDataUrl(this System.Drawing.Bitmap bitmap)
    {
        return $"data:image/png;base64,{bitmap.ToByte().ToBase64()}";
    }
}