using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using HmExtension.Commons.Extensions;

namespace HmExtension.Standard.Extensions;

/// <summary>
/// 字符串扩展类
/// </summary>
public static class StringExtension
{
    /// <summary>  
    /// 将一个字节数组转换为位图  
    /// </summary>  
    /// <param name="pixValue">显示字节数组</param>  
    /// <param name="width">图像宽度</param>  
    /// <param name="height">图像高度</param>  
    /// <returns>位图</returns>  
    private static Bitmap PixToBitmap(byte[] pixValue, int width, int height)
    {
        //// 申请目标位图的变量，并将其内存区域锁定
        var m_currBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        var m_rect = new Rectangle(0, 0, width, height);
        var m_bitmapData = m_currBitmap.LockBits(m_rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

        IntPtr iptr = m_bitmapData.Scan0; // 获取bmpData的内存起始位置  

        //// 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中  
        System.Runtime.InteropServices.Marshal.Copy(pixValue, 0, iptr, pixValue.Length);
        m_currBitmap.UnlockBits(m_bitmapData);
        //// 算法到此结束，返回结果  

        return m_currBitmap;
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